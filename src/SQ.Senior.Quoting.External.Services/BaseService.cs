using SQ.Senior.Clients.QrsService.Models;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using SQ.Senior.Quoting.External.Services.Helpers;
using SQ.Senior.Quoting.External.Services.IServices;
using SQ.Senior.Quoting.External.Services.ViewModels.Pharmacy;
using SQ.Senior.Quoting.External.Services.ViewModels.Plan;
using SQ.Senior.Quoting.External.Services.ViewModels.Prescription;
using SQ.Senior.Quoting.External.Services.ViewModels.Providers;
using System;

namespace SQ.Senior.Quoting.External.Services {
    public class BaseService {
        internal IUserService UserService;

        public BaseService(IUserService userService) {
            UserService = userService;
        }

        // TODO: NO NO, so this has been commented out!Move any Session variable setting and getting to the web host and inject an object or variable into the service layer.HttpContext is a WEB specific object. Move this to a helper in the web proj or something else.
        //public async Task<long> GetCurrentInteractionIdAsync() {
        //    long interactionId = 0;
        //    try {
        //        if (HttpContext.Current.Session[SessionKeys.InteractionId] != null) {
        //            interactionId = (long)HttpContext.Current.Session[SessionKeys.InteractionId];
        //        } else {
        //            if (HttpContext.Current.Session[SessionKeys.UserId] != null && HttpContext.Current.Session[SessionKeys.InteractionId] == null) {
        //                string userId = HttpContext.Current.Session[SessionKeys.UserId].ToString();
        //                await _userService.ManageUserInteractionSessionAsync(userId);
        //            }
        //        }
        //    } catch (Exception ex) {
        //        LogService.Logging.WriteError(ex);
        //        return interactionId;
        //    }
        //    return interactionId;
        //}


        public QRSProvider ProviderMapping(ApplicantProvider provider) {
            return new QRSProvider {
                InteractionId = 0,
                ProviderType = provider.ProviderType,
                NpiNumber = provider.NationalProviderIdentifier,
                Speciality = provider.Specialty,
                Name = provider.Name,
                AddressLine1 = provider.Address1,
                AddressLine2 = provider.Address2,
                City = provider.City,
                State = provider.State,
                Phone = provider.Phone,
                IsActive = provider.IsActive,
                QrsProviderId = provider.QrsProviderId
            };
        }

        public QRSPharmacy PharmacyMapping(Pharmacy pharmacy) {
            return new QRSPharmacy {
                PharmacyDrxId = pharmacy.PharmacyDrxId,
                PharmacyName = pharmacy.PharmacyName,
                Address1 = pharmacy.Address1,
                Address2 = pharmacy.Address2,
                City = pharmacy.City,
                Country = string.Empty,
                State = string.Empty,
                StateId = 0,
                ZipCode = pharmacy.ZipCode,
                StateCode = pharmacy.StateCode,
                Phone = pharmacy.PharmacyPhone,
                Latitude = pharmacy.Latitude,
                Longitude = pharmacy.Longitude,
                Distance = pharmacy.Distance,
                PharmacyNPI = pharmacy.PharmacyNpi,
                Has24hrService = pharmacy.Has24hrService,
                HasCompounding = pharmacy.HasCompounding,
                HasDelivery = pharmacy.HasDelivery,
                HasDriveup = pharmacy.HasDriveup,
                HasDurableEquipment = pharmacy.HasDurableEquipment,
                HasEPrescriptions = pharmacy.HasEPrescriptions,
                HasHandicapAccess = pharmacy.HasHandicapAccess,
                IsHomeInfusion = pharmacy.IsHomeInfusion,
                IsLongTermCare = pharmacy.IsLongTermCare,
                JsonResponse = pharmacy.JsonResponse,
                IsDeleted = pharmacy.IsDeleted,
                QrsPharmacyId = pharmacy.QrsPharmacyId
            };
        }

        public QRSPrescription PrescriptionMapping(AgentVersionApplicantPrescription prescription) {
            return new QRSPrescription {
                PrescriptionId = prescription.PrescriptionId,
                Prescription = prescription.PrescriptionLabel,
                PrescriptionName = prescription.PrescriptionName,
                Package = prescription.Package,
                PackageName = prescription.PackageName,
                DosageId = prescription.DosageId,
                AmountPerThirtyDays = prescription.AmountPerThirtyDays,
                Dated = prescription.Dated,
                PrescriptionInfoAsXml = prescription.SelectedPrescriptionInfoAsXml,
                NationalDrugCode = prescription.NationalDrugCode,
                QrsPrescriptionId = prescription.QrsAgentPrescriptionId
            };
        }

        public QRSPlan PlanMapping(AQEApplicantPlan plan) {
            return new QRSPlan {
                ContactId = plan.ContractId,
                PlanId = plan.PlanId,
                SegmentId = plan.SegmentId,
                DrxSessionId = plan.DrxSessionId,
                DrxPlanId = plan.DrxPlanId,
                PlanName = plan.PlanName,
                PlanAmount = plan.PlanAmount,
                EffectiveYear = plan.EffectiveYear,
                IsSelected = plan.Selected,
                CarrierName = plan.CarrierName,
                PrescriptionDeductible = plan.PrescriptionDeductible,
                PrescriptionPremium = plan.PrescriptionPremium,
                EstAnnualPrescriptionCost = plan.EstimatedAnnualPrescriptionCost,
                EstAnnualMedicalCost = plan.EstimatedAnnualMedicalCost,
                MaxOutOfPocket = plan.MaximumOutOfPocketCost,
                MedicalDeductible = plan.MedicalDeductible,
                MedicalPremium = plan.MedicalPremium,
                PlanRating = plan.PlanRating,
                PlanSubType = plan.PlanSubType,
                PlanType = plan.PlanType,
                LogoName = plan.LogoName,
                QrsPlanId = plan.QrsPlanId
            };
        }

        public QRSProviderFilterCriteria SearchProvidersMapping(SearchProvidersRequest searchRequest) {
            var qrsProvider = new QRSProviderFilterCriteria {
                EntityTypeCode = 1,
                NationalProviderIdentifier = 0,
                ProviderFirstName = searchRequest.FirstName ?? string.Empty,
                ProviderLastName = searchRequest.LastName ?? string.Empty,
                City = searchRequest.City ?? string.Empty,
                ZipCode = searchRequest.ZipCode ?? string.Empty,
                State = searchRequest.State ?? string.Empty
            };

            return qrsProvider;
        }
        public ApplicantProvider ApplicantProviderMapping(ProviderDetail providerDetail, (string phone, string specialty, Contact contact) providerInfo) {

            if (providerDetail is null) {
                return null;
            }

            var applicantProvider = new ApplicantProvider {
                NationalProviderIdentifier = ConvertHelper.GetIntValue(providerDetail?.NationalProviderIdentifier ?? 0),
                Name = $"{providerDetail?.LastName ?? string.Empty}, {providerDetail?.FirstName ?? string.Empty}",
                Address1 = providerInfo.contact?.Address1 ?? string.Empty,
                Address2 = providerInfo.contact?.Address2 ?? string.Empty,
                City = providerInfo.contact?.City ?? string.Empty,
                State = providerInfo.contact?.State ?? string.Empty,
                ZipCode = providerInfo.contact?.ZipCode ?? string.Empty,
                Phone = providerInfo.phone ?? string.Empty,
                Specialty = providerInfo.specialty ?? string.Empty
            };

            return applicantProvider;
        }
        public ApplicantTracking ApplicantTrackingPurlMapping(PurlParams purlParams, string userId, string purlType, long? qrsApplicantId, long? interactionId) {
            var applicantTracking = new ApplicantTracking {
                ZIPCode = purlParams.ZipCode,
                FipsCode = purlParams.FipsCode,
                CreatedDate = DateTime.UtcNow,
                UserId = userId,
                EntrySource = purlType,
                EntryKeyword = purlParams.Keyword,
                QrsApplicantId = qrsApplicantId,
                InteractionId = interactionId,
                ScreenSize = ConvertHelper.GetIntValue(purlParams.ScreenSize),
                DeviceType = purlParams.DeviceType,
                AccountId = ConvertHelper.GetIntValue(purlParams.AccountId),
                IndividualId = ConvertHelper.GetIntValue(purlParams.IndividualId)
            };
            return applicantTracking;
        }
        public Clients.DrxServices.Models.SelectedPrescriptions DrxPresciptionMapping(PrescriptionResponse prescription, Clients.DrxServices.Models.PrescriptionInfo prescriptionInfo) {

            if (prescription is null || string.IsNullOrWhiteSpace(prescription.PrescriptionName))
                return null;
            return new Clients.DrxServices.Models.SelectedPrescriptions {
                Id = prescription.Id,
                AmountPerThirtyDays = prescription.AmountPerThirtyDays,
                Confirmed = true,
                DosageId = prescription.DosageId,
                Package = prescription.Package,
                NationalDrugCode = prescription.NationalDrugCode,
                PrescriptionName = prescription.PrescriptionName,
                SelectedPrescriptionInfoAsXml = prescription.SelectedPrescriptionInfoAsXml,
                SelectedPrescriptionInfo = PlanHelpers.GetPrescriptionInfo(prescription.SelectedPrescriptionInfoAsXml)
            };
        }
        public PrescriptionResponse PrescriptionResponseMapping(PrescriptionRequest prescription) {
            if (prescription is null)
                return null;

            return new PrescriptionResponse {
                Id = prescription.Id,
                PrescriptionId = prescription.PrescriptionId,
                AmountPerThirtyDays = prescription.AmountPerThirtyDays,
                DosageId = prescription.DosageId,
                Package = prescription.Package,
                PrescriptionName = prescription.PrescriptionName,
                PackageName = prescription.PackageName,
                PrescriptionLabel = prescription.PrescriptionLabel
            };
        }

        public PharmacyResponse GetPharmacyResponseMapping(Pharmacy pharmacy) {

            if (pharmacy is null) {
                return null;
            }
            var getPharmacyResponse = new PharmacyResponse {
                Id = ConvertHelper.GetStringValue(pharmacy.Id),
                PharmacyDrxId = pharmacy.PharmacyDrxId,
                PharmacyName = pharmacy.PharmacyName,
                Address1 = pharmacy.Address1,
                Address2 = pharmacy.Address2,
                PharmacyPhone = pharmacy.PharmacyPhone,
                QrsPharmacyId = pharmacy.QrsPharmacyId,
                Distance = pharmacy.Distance
            };
            return getPharmacyResponse;
        }
        public Pharmacy GetPharmacyMapping(SavePharmacyRequest savePharmacyRequest) {

            if (savePharmacyRequest is null) {
                return null;
            }

            var getPharmacy = new Pharmacy {
                PharmacyDrxId = savePharmacyRequest.PharmacyDrxId,
                PharmacyName = savePharmacyRequest.PharmacyName,
                Address1 = savePharmacyRequest.Address1,
                Address2 = savePharmacyRequest.Address2,
                City = savePharmacyRequest.City,
                ZipCode = savePharmacyRequest.ZipCode,
                StateCode = savePharmacyRequest.StateCode,
                PharmacyPhone = savePharmacyRequest.PharmacyPhone,
                Latitude = savePharmacyRequest.Latitude,
                Longitude = savePharmacyRequest.Longitude,
                Distance = savePharmacyRequest.Distance,
                JsonResponse = savePharmacyRequest.JsonResponse,
                PharmacyNpi = savePharmacyRequest.PharmacyNpi,
                Has24hrService = savePharmacyRequest.Has24hrService,
                HasCompounding = savePharmacyRequest.HasCompounding,
                HasDelivery = savePharmacyRequest.HasDelivery,
                HasDriveup = savePharmacyRequest.HasDriveup,
                HasDurableEquipment = savePharmacyRequest.HasDurableEquipment,
                HasEPrescriptions = savePharmacyRequest.HasEPrescriptions,
                HasHandicapAccess = savePharmacyRequest.HasHandicapAccess,
                IsHomeInfusion = savePharmacyRequest.IsHomeInfusion,
                IsLongTermCare = savePharmacyRequest.IsLongTermCare
            };
            return getPharmacy;
        }
        public PrescriptionResponse AgentVersionApplicantPrescriptionMapping(AgentVersionApplicantPrescription prescription) {
            if (prescription is null) {
                return null;
            }

            return new PrescriptionResponse {
                Id = prescription.Id,
                PrescriptionId = prescription.PrescriptionId,
                AmountPerThirtyDays = prescription.AmountPerThirtyDays,
                DosageId = prescription.DosageId,
                Package = prescription.Package,
                PrescriptionName = prescription.PrescriptionName,
                PackageName = prescription.PackageName,
                PrescriptionLabel = prescription.PrescriptionLabel,

            };
        }
        public AgentVersionApplicantPrescription RequestPrescriptionToAgentVersionPrescriptionMapping(PrescriptionRequest prescription) {
            if (prescription is null) {
                return null;
            }

            return new AgentVersionApplicantPrescription {
                Id = prescription.Id,
                PrescriptionId = prescription.PrescriptionId,
                AmountPerThirtyDays = prescription.AmountPerThirtyDays,
                DosageId = prescription.DosageId,
                Package = prescription.Package,
                PrescriptionName = prescription.PrescriptionName,
                PackageName = prescription.PackageName,
                PrescriptionLabel = prescription.PrescriptionLabel,
                Dated = DateTime.UtcNow

            };
        }
        public ProviderResponse GetProviderResponseMapping(ApplicantProvider applicantProvider) {

            if (applicantProvider is null) {
                return null;
            }
            var getProviderResponse = new ProviderResponse {
                Id = applicantProvider.Id,
                ProviderType = applicantProvider.ProviderType,
                NpiNumber = applicantProvider.NationalProviderIdentifier,
                Name = applicantProvider.Name,
                Address1 = applicantProvider.Address1,
                City = applicantProvider.City,
                State = applicantProvider.State,
                ZipCode = applicantProvider.ZipCode,
                Phone = applicantProvider.Phone,
                Active = applicantProvider.IsActive,
                Specialty = applicantProvider.Specialty
            };
            return getProviderResponse;
        }
    }
}
