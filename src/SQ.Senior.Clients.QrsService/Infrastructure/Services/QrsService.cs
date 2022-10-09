using SQ.Senior.Clients.QrsService.Infrastructure.Constants;
using SQ.Senior.Clients.QrsService.Infrastructure.Helpers;
using SQ.Senior.Clients.QrsService.Infrastructure.IQRSServices;
using SQ.Senior.Clients.QrsService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SQ.Senior.Clients.QrsService.Infrastructure.Services {
    public class QrsService : IQrsService {
        private readonly IQrsApiClient _qrsApiClient;

        public QrsService(IQrsApiClient qrsApiClient) {
            _qrsApiClient = qrsApiClient ?? throw new ArgumentNullException(nameof(qrsApiClient));
        }

        public async Task<ApplicantInteractionResponse> AddApplicantsAsync(QRSApplicants applicant, BrandType brandType, string userId) {
            var requestResponse = new ApplicantInteractionResponse();
            bool isNew = true;
            try {
                QRSApplicants qrsApplicant = null;
                if (applicant.QrsApplicantId > 0) {
                    var existingApplicant = await GetApplicant(applicant.QrsApplicantId, userId);
                    if (existingApplicant != null) {
                        isNew = false;
                        existingApplicant.ExtraHelpLevel = applicant.ExtraHelpLevel;
                        existingApplicant.Email = string.IsNullOrEmpty(existingApplicant.Email) ? applicant.Email : existingApplicant.Email;
                        existingApplicant.DateModified = DateTime.UtcNow;
                        existingApplicant.DateDeleted = null;
                        qrsApplicant = await _qrsApiClient.PatchRequestAsync(Endpoints.Applicants, applicant.QrsApplicantId, existingApplicant, userId);
                    }
                }
                if (isNew) {
                    applicant.DateCreated = DateTime.UtcNow;
                    qrsApplicant = await _qrsApiClient.PostRequestAsync(Endpoints.Applicants, applicant, userId);
                    if (qrsApplicant != null && qrsApplicant.Id > 0) {
                        requestResponse.ApplicantId = qrsApplicant.Id;
                        requestResponse.InteractionId = AddInteraction(qrsApplicant.Id, brandType, userId);
                    }
                }
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                requestResponse.ApplicantId = requestResponse.InteractionId = 0;
            }
            return requestResponse;
        }

        public long AddInteraction(long applicantId, BrandType brandType, string userId) {
            var interactionRequest = new Interaction {
                ApplicantId = applicantId,
                Source = QRSHelper.GetSourceType(brandType),
                DateCreated = DateTime.UtcNow
            };
            try {
                return _qrsApiClient.PostRequestAsync(Endpoints.Interactions, interactionRequest, userId).Result.Id;
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return 0;
            }
        }

        public async Task<List<ContactType>> GetContactTypes(string userId) {
            try {
                return await _qrsApiClient.GetAsync<List<ContactType>>(Endpoints.ContactTypes, userId);
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return new List<ContactType>();
            }
        }


        public async Task<int> AddZipFipCode(QRSContact contact, long interactionId, string userId) {
            string endPoint = string.Format(Endpoints.Contact, interactionId);
            int qrsContactId = 0;
            QRSContact qrsContact = null;
            bool isNew = true;
            try {
                if (contact.QrsContactId > 0) {
                    var existingContact = await GetContact((int)contact.QrsContactId, userId);
                    if (existingContact != null) {
                        isNew = false;
                        if ((string.IsNullOrEmpty(existingContact.FipsCode) || !existingContact.FipsCode.Equals(contact.FipsCode))) {
                            var modifiedContact = existingContact;
                            modifiedContact.FipsCode = contact.FipsCode;
                            modifiedContact.DateModified = DateTime.UtcNow;
                            modifiedContact.DateDeleted = null;
                            qrsContact = await _qrsApiClient.PatchRequestAsync<QRSContact>(Endpoints.ApplicantContact, (int)contact.QrsContactId, modifiedContact, userId);
                        } else {
                            qrsContact = existingContact;
                        }
                    }
                }
                if (isNew) {
                    contact.DateCreated = DateTime.UtcNow;
                    qrsContact = await _qrsApiClient.PostRequestAsync(endPoint, contact, userId);
                }

                if (qrsContact != null && qrsContact.Id > 0) {
                    qrsContactId = qrsContact.Id;
                }
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                qrsContactId = 0;
            }
            return qrsContactId;
        }

        public async Task AddContactsAsync(QRSContact contact, string contactType, long interactionId, string userId, bool isAQE = false) {
            try {
                if (isAQE) {
                    string endPointAQE = string.Format(Endpoints.Contact, interactionId);
                    contact.InteractionId = interactionId;
                    contact.DateCreated = DateTime.UtcNow;
                    contact.DateModified = DateTime.UtcNow;
                    await _qrsApiClient.PostRequestAsync(endPointAQE, contact, userId);
                } else {
                    string endPoint = string.Format(Endpoints.Contact, interactionId);
                    var requestModel = new QRSContact();
                    var contactTypes = await GetContactTypes(userId);
                    requestModel.InteractionId = interactionId;
                    requestModel.ContactTypeId = contactTypes.FirstOrDefault(x => x.ContactTypeText.ToString().Equals(contactType)).Id;
                    switch (contactType) {
                        case CommunicationTypes.EmailType:
                            requestModel.EmailAddress = contact.EmailAddress;
                            break;
                        case CommunicationTypes.ZipCodeType:
                            requestModel.ZipCode = contact.ZipCode;
                            break;
                        case CommunicationTypes.FipsCodeType:
                            requestModel.FipsCode = contact.FipsCode;
                            break;
                        case CommunicationTypes.PhoneType:
                            requestModel.PhoneNumber = contact.PhoneNumber;
                            break;
                        case CommunicationTypes.AddressType:
                            requestModel.AddressLine1 = contact.AddressLine1;
                            break;
                    }
                    if (contactType.Equals(CommunicationTypes.FipsCodeType)) {
                        var contactRecordList = await _qrsApiClient.GetAsync<List<QRSContact>>(string.Format(Endpoints.Contact, interactionId), userId);
                        if (contactRecordList.Any()) {
                            var zipCodeRecord = contactRecordList.FirstOrDefault(x => x.ZipCode.Equals(contact.ZipCode));
                            if (zipCodeRecord != null && zipCodeRecord.Id > 0) {
                                zipCodeRecord.FipsCode = requestModel.FipsCode;
                                zipCodeRecord.DateModified = DateTime.UtcNow;
                                zipCodeRecord.DateDeleted = null;
                                await _qrsApiClient.PatchRequestAsync(endPoint, zipCodeRecord.Id, zipCodeRecord, userId);
                                return;
                            }
                        }
                    }
                    requestModel.DateCreated = DateTime.UtcNow;
                    await _qrsApiClient.PostRequestAsync(endPoint, requestModel, userId);
                }
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
            }
        }

        public async Task AddContactsAsyncGeneric(QRSContact contact, long interactionId, string userId) {
            try {
                string endPoint = string.Format(Endpoints.Contact, interactionId);
                QRSContact requestModel;
                var contactTypes = await GetContactTypes(userId);
                // getting all object properties.
                PropertyInfo[] objectProperties = contact.GetType().GetProperties();
                // iterate through each property.
                foreach (var property in objectProperties) {
                    bool isPatchRequest = false;
                    // get property value to check if null or not. if null then we don't perform any action.
                    var propertyValue = property.GetValue(contact, null);
                    string contactType = string.Empty;
                    if (QRSHelper.ValidateValue(propertyValue)) {
                        contactType = QRSHelper.GetCommunicationType(property.Name);
                        if (!string.IsNullOrEmpty(contactType)) {
                            requestModel = new QRSContact {
                                // get contact-type-id based on contact-type we get in previous step.
                                ContactTypeId = contactTypes.FirstOrDefault(x => x.ContactTypeText.ToString().Equals(contactType)).Id
                            };
                            // in case of these 2 types, get previous QRS record for same Interaction from db and update property value in that model and patch the request.
                            if (contactType.Equals(CommunicationTypes.AddressType)) {
                                var contactRecordList = await _qrsApiClient.GetAsync<List<QRSContact>>(endPoint, userId);
                                if (contactRecordList != null && contactRecordList.Any()) {
                                    var qrsContactRecord = contactRecordList.FirstOrDefault(x => x.ContactTypeId == requestModel.ContactTypeId);
                                    if (qrsContactRecord != null) {
                                        requestModel = qrsContactRecord;
                                        isPatchRequest = true;
                                    }
                                }
                            }
                            // now we need to set the property value in our request model.
                            // we don't know which property it is.therefore we set it dynamically here.
                            typeof(QRSContact).GetProperty(property.Name).SetValue(requestModel, propertyValue);
                            if (isPatchRequest) {
                                requestModel.DateModified = DateTime.UtcNow;
                                requestModel.DateDeleted = null;
                                await _qrsApiClient.PatchRequestAsync(endPoint, requestModel.Id, requestModel, userId);
                            } else {
                                requestModel.DateCreated = DateTime.UtcNow;
                                requestModel.InteractionId = interactionId;
                                await _qrsApiClient.PostRequestAsync(endPoint, requestModel, userId);
                            }
                        }
                    }
                }
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
            }
        }

        public async Task<QRSApplicants> GetApplicant(int qrsApplicantId, string userId) {
            string endPoint = Endpoints.Applicants + "/" + qrsApplicantId;
            try {
                return await _qrsApiClient.GetAsync<QRSApplicants>(endPoint, userId);
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }

        public async Task<QRSContact> GetContact(int qrsContactId, string userId) {
            string endPoint = Endpoints.ApplicantContact + "/" + qrsContactId;
            try {
                return await _qrsApiClient.GetAsync<QRSContact>(endPoint, userId);
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }

        public async Task<QRSProvider> GetProvider(long qrsProviderId, string userId) {
            string endPoint = Endpoints.ApplicantProvider + "/" + qrsProviderId;
            try {
                return await _qrsApiClient.GetAsync<QRSProvider>(endPoint, userId);
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }


        public async Task<QRSPharmacy> GetPharmacy(int qrsPharmacyId, string userId) {
            string endPoint = Endpoints.ApplicantPharmacy + "/" + qrsPharmacyId;
            try {
                return await _qrsApiClient.GetAsync<QRSPharmacy>(endPoint, userId);
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }

        public async Task<QRSPlan> GetPlan(long qrsPlanId, string userId) {
            var endPoint = $"{ Endpoints.ApplicantPlan}/{ qrsPlanId}";
            try {
                return await _qrsApiClient.GetAsync<QRSPlan>(endPoint, userId);
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }


        public async Task<int> AddPharmacyAsync(QRSPharmacy pharmacy, long interactionId, string userId) {
            string endPoint = string.Format(Endpoints.Pharmacy, interactionId);
            int qrsPharmacyId = 0;
            QRSPharmacy qrsPharmacy = null;
            bool isNew = true;
            try {
                if (pharmacy.QrsPharmacyId > 0) {
                    var existingPharmacy = await GetPharmacy((int)pharmacy.QrsPharmacyId, userId);
                    if (existingPharmacy != null) {
                        isNew = false;
                        var modifiedPharmacy = pharmacy;
                        modifiedPharmacy.Id = existingPharmacy.Id;
                        modifiedPharmacy.IsDeleted = false;
                        modifiedPharmacy.DateModified = DateTime.UtcNow;
                        modifiedPharmacy.DateDeleted = null;
                        qrsPharmacy = await _qrsApiClient.PatchRequestAsync(Endpoints.ApplicantPharmacy, (int)pharmacy.QrsPharmacyId, modifiedPharmacy, userId);
                    }
                }
                if (isNew) {
                    pharmacy.DateCreated = DateTime.UtcNow;
                    qrsPharmacy = await _qrsApiClient.PostRequestAsync(endPoint, pharmacy, userId);
                }

                if (qrsPharmacy != null && qrsPharmacy.Id > 0) {
                    qrsPharmacyId = qrsPharmacy.Id;
                }
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                qrsPharmacyId = 0;
            }
            return qrsPharmacyId;
        }

        public async Task<int> AddPlanAsync(QRSPlan plan, long interactionId, string userId) {
            string endPoint = string.Format(Endpoints.Plans, interactionId);
            int qrsPlanId = 0;
            QRSPlan qrsPlan = null;
            bool isNew = true;
            try {
                if (plan.QrsPlanId > 0) {
                    var existingPlan = await GetPlan((int)plan.QrsPlanId, userId);
                    if (existingPlan != null) {
                        isNew = false;
                        var modifiedPlan = existingPlan;
                        modifiedPlan.Id = existingPlan.Id;
                        modifiedPlan.IsSelected = true;
                        modifiedPlan.EffectiveYear = plan.EffectiveYear;
                        modifiedPlan.EstAnnualPrescriptionCost = plan.EstAnnualPrescriptionCost;
                        modifiedPlan.EstAnnualMedicalCost = plan.EstAnnualMedicalCost;
                        modifiedPlan.DateModified = DateTime.UtcNow;
                        modifiedPlan.DateDeleted = DateTime.UtcNow;
                        qrsPlan = await _qrsApiClient.PatchRequestAsync(Endpoints.ApplicantPlan, (int)plan.QrsPlanId, modifiedPlan, userId);
                    }
                }
                if (isNew) {
                    plan.DateCreated = DateTime.UtcNow;
                    qrsPlan = await _qrsApiClient.PostRequestAsync(endPoint, plan, userId);
                }

                if (qrsPlan != null && qrsPlan.Id > 0) {
                    qrsPlanId = qrsPlan.Id;
                }
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                qrsPlanId = 0;
            }
            return qrsPlanId;
        }

        public async Task<int> AddEffectivePlanAsync(int planId, long? qrsApplicantId, string userId) {
            string endPoint = string.Format(Endpoints.EffectivePlan, qrsApplicantId, planId);
            try {
                await _qrsApiClient.PostRequestAsync(endPoint, new QRSPlan(), userId);
                return 1;
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return 0;
            }
        }
        public async Task DeleteQrsDataAsync(string endPoint, long? recordId, string userId) {
            try {
                if (recordId != null)
                    await _qrsApiClient.DeleteRequestAsync(endPoint, (int)recordId, userId);
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
            }
        }
        public async Task<QRSApplicantsTokens> GetApplicantByTokenAsync(string endPoint, string token, string userId) {
            try {
                QRSApplicantsTokens qrsApplicantsTokens = new QRSApplicantsTokens { Token = token };
                return await _qrsApiClient.PostRequestAsync(endPoint, qrsApplicantsTokens, userId);
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }

        public async Task<QRSApplicants> GetApplicantByApplicantIdAsync(string endPoint, string applicantId, string userId) {
            try {
                return await _qrsApiClient.GetByIdAsync<QRSApplicants>(endPoint, Convert.ToInt32(applicantId), userId);
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }

        public async Task<QRSApplicants> GetConsolidatedDataByApplicantIdAsync(string endPoint, string applicantId, string userId) {
            var url = $"applicants/{applicantId}/consolidate";
            try {
                var data = await _qrsApiClient.GetAsync<QRSApplicants>(url, userId);
                return data;
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }
    }
}
