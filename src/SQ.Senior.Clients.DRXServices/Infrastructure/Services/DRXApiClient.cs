using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SQ.Senior.Clients.DrxServices.Configurations;
using SQ.Senior.Clients.DrxServices.Infrastructure.Constants;
using SQ.Senior.Clients.DrxServices.Infrastructure.Enums;
using SQ.Senior.Clients.DrxServices.Infrastructure.Helpers;
using SQ.Senior.Clients.DrxServices.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SQ.Senior.Clients.DrxServices.Infrastructure.Services {

    public class DrxApiClient : IDrxApiClient {
        private readonly HttpClient _httpClient;
        private readonly DrxConfiguration _drxConfiguration;
        private readonly PhoneNumbers _phoneNumbers;
        //TODO: user user id from token
        private readonly string UserId = "0";
        private readonly IAuthentication _authentication;
        public DrxApiClient(HttpClient httpClient, IOptionsSnapshot<DrxConfiguration> drxConfigurationOptions,
            IOptionsSnapshot<PhoneNumbers> phoneNumberOptions, IAuthentication authentication) {
            _httpClient = httpClient;
            _drxConfiguration = drxConfigurationOptions.Value;
            _phoneNumbers = phoneNumberOptions.Value;
            _authentication = authentication;
        }

        public async Task<string> AddSessionPharmacy(string drxSession, string drxAccessToken, string pharmacyJson) {
            var url = new StringBuilder(_drxConfiguration.LinkAddRemovePharmacySession)
                        .Replace("[SESSION_ID]", drxSession)
                        .ToString();
            try {
                var contentType = "application/json";
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", drxAccessToken);
                var payload = new StringContent(pharmacyJson, Encoding.UTF8, contentType);
                HttpResponseMessage sessionPharmacyResponse = await _httpClient.PostAsync(url, payload);
                if (sessionPharmacyResponse == null)
                    return string.Empty;
                if (sessionPharmacyResponse.StatusCode == HttpStatusCode.OK) {
                    var sessionPharmacyContent = await sessionPharmacyResponse.Content.ReadAsStringAsync();
                    return sessionPharmacyContent;
                }
                return string.Empty;
            } catch (HttpRequestException ex) {
                ApiLogService.LogException(ex, UserId);
                throw new HttpRequestException(nameof(_httpClient));
            }
        }

        public async Task<string> DeleteSessionPharmacy(string drxSession, string drxAccessToken) {
            var url = new StringBuilder(_drxConfiguration.LinkAddRemovePharmacySession)
                        .Replace("[SESSION_ID]", drxSession)
                        .ToString();
            try {
                var contentType = "application/json";
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", drxAccessToken);
                HttpResponseMessage deleteSessionPharmacyResponse = await _httpClient.DeleteAsync(url);
                if (deleteSessionPharmacyResponse == null)
                    return null;
                if (deleteSessionPharmacyResponse.StatusCode == HttpStatusCode.OK) {
                    var deleteSessionPharmacyResult = await deleteSessionPharmacyResponse.Content.ReadAsStringAsync();
                    return deleteSessionPharmacyResult;
                }
                return string.Empty;
            } catch (HttpRequestException ex) {
                ApiLogService.LogException(ex, UserId);
                throw new HttpRequestException(nameof(_httpClient));
            }
        }

        public async Task<List<Prescription>> SearchPrescriptionsAsync(string prescriptionKeyword) {
            try {

                var drxToken = await _authentication.GetDrxTokenAsync();
                if (drxToken is null)
                    return null;

                var prescriptionAutoCompleteLink = new StringBuilder(_drxConfiguration.LinkPrescriptionsAutocomplete)
                        .Replace(Endpoints.PrescriptionPrefix, prescriptionKeyword)
                        .ToString();

                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Endpoints.JsonContent));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Endpoints.Bearer, drxToken.AccessToken);
                HttpResponseMessage prescriptionAutoCompleteResponse = await _httpClient.GetAsync(prescriptionAutoCompleteLink);
                if (prescriptionAutoCompleteResponse == null)
                    return null;
                if (prescriptionAutoCompleteResponse.StatusCode == HttpStatusCode.OK) {
                    var prescriptionAutoCompleteResult = await prescriptionAutoCompleteResponse.Content.ReadAsStringAsync();
                    var prescriptions = JsonConvert.DeserializeObject<List<Prescription>>(prescriptionAutoCompleteResult);
                    return prescriptions;
                }
                return null;
            } catch (HttpRequestException ex) {
                ApiLogService.LogException(ex, UserId);
                throw new HttpRequestException(nameof(_httpClient));
            }
        }

        public async Task<PrescriptionInfo> GetPrescriptionInfo(int id, string drxAccessToken) {
            try {
                var url = new StringBuilder(_drxConfiguration.LinkPrescriptionsInfo)
                        .Replace("[DRUG_ID]", id.ToString())
                        .ToString();

                var contentType = "application/json";
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", drxAccessToken);
                HttpResponseMessage prescriptionInfoResponse = await _httpClient.GetAsync(url);
                if (prescriptionInfoResponse == null)
                    return null;
                if (prescriptionInfoResponse.StatusCode == HttpStatusCode.OK) {
                    var prescriptionInfoResponseResult = await prescriptionInfoResponse.Content.ReadAsStringAsync();
                    var prescriptionInfo = JsonConvert.DeserializeObject<PrescriptionInfo>(prescriptionInfoResponseResult);
                    return prescriptionInfo;
                }
                return null;
            } catch (HttpRequestException ex) {
                ApiLogService.LogException(ex, UserId);
                throw new HttpRequestException(nameof(_httpClient));
            }
        }

        public PrescriptionInfo GetPrescriptionInfo(string prescriptionInfoAsXml) {
            try {
                if (!string.IsNullOrEmpty(prescriptionInfoAsXml)) {
                    using (StringReader responseAsString = new StringReader(prescriptionInfoAsXml)) {
                        XmlSerializer serializer =
                            new XmlSerializer(typeof(PrescriptionInfo));
                        PrescriptionInfo prescriptionInfo = (PrescriptionInfo)serializer.Deserialize(responseAsString);
                        return prescriptionInfo;
                    }
                }
            } catch (Exception ex) {
                ApiLogService.LogException(ex, UserId);
            }
            return null;
        }

        public async Task<List<List<TierInfo>>> GetFormularyPharmacy(string planId, string sessionId, string zip, string fips, string effectiveDate, string drxAccessToken) {
            try {
                var url = new StringBuilder(_drxConfiguration.LinkFormularyTierPharmacy)
                        .Replace("[SESSION_ID]", sessionId)
                        .Replace("[PLANID]", planId)
                        .Replace("[ZIP]", zip)
                        .Replace("[FIPS]", fips)
                        .ToString();

                var contentType = "application/json";
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", drxAccessToken);
                HttpResponseMessage formularyPharmacyResponse = await _httpClient.GetAsync(url);
                if (formularyPharmacyResponse == null)
                    return null;
                if (formularyPharmacyResponse.StatusCode == HttpStatusCode.OK) {
                    var formularyPharmacyResult = await formularyPharmacyResponse.Content.ReadAsStringAsync();
                    var tierInfo = JsonConvert.DeserializeObject<List<List<TierInfo>>>(formularyPharmacyResult);
                    return tierInfo;
                }
                return null;
            } catch (HttpRequestException ex) {
                ApiLogService.LogException(ex, UserId);
                throw new HttpRequestException(nameof(_httpClient));
            }
        }

        public FormularyTierFilters GetFormularyTypeDetail(int formularyTypeDetailId) {
            FormularyTierFilters tierDetail = new FormularyTierFilters();
            if (formularyTypeDetailId == 0) {
                tierDetail.DaysOfSupply = 30;
                tierDetail.IsMailOrder = false;
                tierDetail.IsPreferredPharmacy = true;
            } else if (formularyTypeDetailId == 1) {
                tierDetail.DaysOfSupply = 30;
                tierDetail.IsMailOrder = false;
                tierDetail.IsPreferredPharmacy = false;
            } else if (formularyTypeDetailId == 2) {
                tierDetail.DaysOfSupply = 90;
                tierDetail.IsMailOrder = true;
                tierDetail.IsPreferredPharmacy = true;
            } else if (formularyTypeDetailId == 3) {
                tierDetail.DaysOfSupply = 90;
                tierDetail.IsMailOrder = true;
                tierDetail.IsPreferredPharmacy = false;
            }
            return tierDetail;
        }

        public async Task<List<Pharmacy>> SearchDrxPharmaciesAsync(string zipCode, string drxAccessToken, string pharmacyDistance, string pharmacyName) {

            var pharmacies = new List<Pharmacy>();
            if (string.IsNullOrWhiteSpace(zipCode))
                return pharmacies;

            pharmacies.AddRange(await GetPharmaciesAsync(zipCode, Records.Zero.ToString(), drxAccessToken));

            if (pharmacies.Count > Convert.ToInt32(Records.FortyNine))
                pharmacies.AddRange(await GetPharmaciesAsync(zipCode, Records.Fifty.ToString(), drxAccessToken));

            pharmacies = string.IsNullOrWhiteSpace(pharmacyDistance) ? pharmacies :
                    pharmacies.Where(x => x.Distance <= Convert.ToDouble(pharmacyDistance)).ToList();

            pharmacies = string.IsNullOrWhiteSpace(pharmacyName) ? pharmacies :
                                pharmacies.Where(x => DrxHelper.RemoveSpecialCharacters(x.Name).ToLower().Contains(pharmacyName.ToLower())).ToList();
            return pharmacies;
        }

        public async Task<List<Pharmacy>> GetPharmacyAutoComplete(string zipCode, string planId, string drxAccessToken) {
            try {
                var url = new StringBuilder(_drxConfiguration.LinkFormularyTierPharmacy)
                        .Replace("[ZIP]", zipCode)
                        .ToString();
                var contentType = "application/json";
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", drxAccessToken);
                HttpResponseMessage pharmacyResponse = await _httpClient.GetAsync(url);
                if (pharmacyResponse == null)
                    return null;
                if (pharmacyResponse.StatusCode == HttpStatusCode.OK) {
                    var pharmacyResult = await pharmacyResponse.Content.ReadAsStringAsync();
                    var pharmacies = JsonConvert.DeserializeObject<List<Pharmacy>>(pharmacyResult);
                    return pharmacies;
                }
                return null;
            } catch (HttpRequestException ex) {
                ApiLogService.LogException(ex, UserId);
                throw new HttpRequestException(nameof(_httpClient));
            }
        }

        public async Task<Plan> GetPlanDetailExtended(string planId, string sessionId, string zip, string fips, string effectiveDate, string drxAccessToken) {
            try {
                var url = new StringBuilder(_drxConfiguration.LinkPlanDetailSearch)
                            .Replace("[SESSION_ID]", sessionId)
                            .Replace("[PLAN_ID]", planId)
                            .Replace("[ZIP]", zip)
                            .Replace("[FIPS]", fips)
                            .Replace("[EFFECTIVE_DATE]", effectiveDate)
                            .ToString();

                var contentType = "application/json";
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", drxAccessToken);
                HttpResponseMessage planResponse = await _httpClient.GetAsync(url);
                if (planResponse == null)
                    return null;
                if (planResponse.StatusCode == HttpStatusCode.OK) {
                    var planResult = await planResponse.Content.ReadAsStringAsync();
                    var plan = JsonConvert.DeserializeObject<Plan>(planResult);
                    return plan;
                }
                return null;
            } catch (HttpRequestException ex) {
                ApiLogService.LogException(ex, UserId);
                throw new HttpRequestException(nameof(_httpClient));
            }
        }

        public async Task<MedicarePlanDetail> GetPlanDetail(string planId, string sessionId, string zip, string fips, string effectiveDate, string drxAccessToken) {
            try {
                var url = new StringBuilder(_drxConfiguration.LinkPlanDetailSearch)
                            .Replace("[SESSION_ID]", sessionId)
                            .Replace("[PLAN_ID]", planId)
                            .Replace("[ZIP]", zip)
                            .Replace("[FIPS]", fips)
                            .Replace("[EFFECTIVE_DATE]", effectiveDate)
                            .Replace("[SHOWPHARMACY]", "true")
                            .ToString();

                var contentType = "application/json";
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", drxAccessToken);
                HttpResponseMessage medicarePlanDetailResponse = await _httpClient.GetAsync(url);
                if (medicarePlanDetailResponse == null)
                    return null;
                if (medicarePlanDetailResponse.StatusCode == HttpStatusCode.OK) {
                    var medicarePlanDetailResult = await medicarePlanDetailResponse.Content.ReadAsStringAsync();
                    var medicarePlanDetail = JsonConvert.DeserializeObject<MedicarePlanDetail>(medicarePlanDetailResult);
                    return medicarePlanDetail;
                }
                return null;
            } catch (HttpRequestException ex) {
                ApiLogService.LogException(ex, UserId);
                throw new HttpRequestException(nameof(_httpClient));
            }
        }

        public async Task<string> GetPlans(string sessionId, string zip, string fips, string planYear, string drxAccessToken) {
            try {
                var urlGetPlans = new StringBuilder(_drxConfiguration.LinkPlanSearch)
                            .Replace(Endpoints.SessionId, sessionId)
                            .Replace(Endpoints.Zip, zip)
                            .Replace(Endpoints.Fips, fips)
                            .Replace(Endpoints.PlanYear, planYear)
                            .ToString();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Endpoints.Bearer, drxAccessToken);
                var request = new HttpRequestMessage(HttpMethod.Get, $"{_drxConfiguration.BaseUri}{urlGetPlans}");
                request.Headers.Add(HttpRequestHeader.Accept.ToString(), Endpoints.XmlContent);
                var planResponse = await _httpClient.SendAsync(request);

                if (planResponse is null)
                    return string.Empty;

                if (planResponse.StatusCode == HttpStatusCode.OK) {
                    return await planResponse.Content.ReadAsStringAsync();
                }
                return string.Empty;
            } catch (HttpRequestException ex) {
                ApiLogService.LogException(ex, UserId);
                throw new HttpRequestException(nameof(_httpClient));
            }
        }

        public async Task<string> PlanEnroll(ApplicationUser user, AQEApplicantPlan plan, string sessionId, string drxAccessToken, string fips, string zip, string userId, string qrsApplicantId, string token) {
            try {
                var url = new StringBuilder(_drxConfiguration.LinkPlanEnrollment)
                            .Replace("[Sessionid]", sessionId)
                            .Replace("[PlanID]", plan.DRXPlanId)
                            .ToString();

                var contentType = "application/xml";
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", drxAccessToken);
                string planEnrollXml = await GenerateEnrollXml(user, fips, zip, userId, qrsApplicantId, token, "CONSUMER");
                var payload = new StringContent(planEnrollXml, Encoding.UTF8, contentType);
                HttpResponseMessage planEnrollResponse = await _httpClient.PostAsync(url, payload);
                if (planEnrollResponse == null)
                    return null;
                if (planEnrollResponse.StatusCode == HttpStatusCode.OK) {
                    var planEnrollResult = await planEnrollResponse.Content.ReadAsStringAsync();
                    return planEnrollResult;
                }
                return null;
            } catch (HttpRequestException ex) {
                ApiLogService.LogException(ex, UserId);
                throw new HttpRequestException(nameof(_httpClient));
            }
        }
        private async Task<List<Pharmacy>> GetPharmaciesAsync(string zipCode, string skip, string drxAccessToken) {
            try {
                var searchPharmacyUrl = new StringBuilder(_drxConfiguration.LinkPharmacySearch)
                        .Replace(Endpoints.Zip, zipCode)
                        .Replace(Endpoints.Skip, skip)
                        .ToString();

                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Endpoints.JsonContent));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Endpoints.Bearer, drxAccessToken);
                var pharmacyResponse = await _httpClient.GetAsync(searchPharmacyUrl);
                if (pharmacyResponse is null)
                    return null;
                if (pharmacyResponse.StatusCode == HttpStatusCode.OK) {
                    var pharmacies = JsonConvert.DeserializeObject<PharmacyResult>(await pharmacyResponse.Content.ReadAsStringAsync());
                    return pharmacies.PharmacyList.ToList();
                }
                return null;
            } catch (HttpRequestException ex) {
                ApiLogService.LogException(ex, UserId);
                throw new HttpRequestException(nameof(_httpClient));
            }
        }
        private async Task<string> GenerateEnrollXml(ApplicationUser applicant, string fips, string zip, string userId, string qrsApplicantId, string token, string carrier = "CONSUMER", bool isMedicareHelpLine = false) {
            var enrollXml = new StringBuilder();
            try {
                var contactPhoneNumber = isMedicareHelpLine
                    ? _phoneNumbers.MedicareHelplineDRxPhoneNumber
                    : _phoneNumbers.DefaultPhoneNumber;

                var selectQuoteTtyXml =
                    "<EnrollDisplay>" +
                        "<KeyMessage>AgentMessage2</KeyMessage>" +
                        "<ValueMessage>TTY:1-977-486-2048</ValueMessage>" +
                    "</EnrollDisplay>";

                var ttyXml = isMedicareHelpLine ? string.Empty : selectQuoteTtyXml;
                var Enrollee = new StringBuilder("<Enrollee>");

                if (applicant != null) {
                    Enrollee.Append($"<CountyFIPS>{fips}</CountyFIPS>");
                    if (applicant.DateOfBirth != null && applicant.DateOfBirth.Value != DateTime.MinValue)
                        Enrollee.Append($"<DateOfBirth>{applicant.DateOfBirth.Value:yyyy-MM-dd}</DateOfBirth>");
                    if (!string.IsNullOrEmpty(applicant.Email))
                        Enrollee.Append($"<Email>{applicant.Email}</Email>");
                    if (!string.IsNullOrEmpty(applicant.FirstName))
                        Enrollee.Append($"<FirstName>{applicant.FirstName}</FirstName>");
                    if (!string.IsNullOrEmpty(applicant.LastName))
                        Enrollee.Append($"<LastName>{applicant.LastName}</LastName>");
                    if (!string.IsNullOrEmpty(applicant.Phone))
                        Enrollee.Append($"<PhoneNumber>{applicant.Phone.Replace("-", "").Replace("_", "")}</PhoneNumber>");
                    Enrollee.Append($"<Zip>{zip.Trim()}</Zip>");
                    Enrollee.Append("</Enrollee>");
                } else {
                    //TODO: this task will be done in the SEQ-783
                    if (!string.IsNullOrEmpty(token)) {
                        //var applicant_token = await _applicantService.GetApplicantByTokenAsync(token, userId);
                        // var applicantConsolidated = await _applicantService.GetConsolidatedDataByApplicantIdAsync(applicant_token.ApplicantId, userId);
                        //var ConsolidatedContacts = applicantConsolidated.Contacts
                        //    .GroupBy(u => u.ContactTypeId)
                        //    .Select(grp => grp.OrderByDescending(d => d.DateCreated)
                        //    .FirstOrDefault()).ToList();
                        //var applicantPhoneNumber = ConsolidatedContacts.FirstOrDefault(c => c.ContactTypeId == 2);

                        Enrollee.Append($"<CountyFIPS>{fips}</CountyFIPS>");
                        //if (applicantConsolidated.DOB != null && Convert.ToDateTime(applicantConsolidated.DOB) != DateTime.MinValue)
                        //    Enrollee.Append($"<DateOfBirth>{Convert.ToDateTime(applicantConsolidated.DOB):yyyy-MM-dd}</DateOfBirth>");
                        //if (!string.IsNullOrEmpty(applicantConsolidated.Email))
                        //    Enrollee.Append($"<Email>{applicantConsolidated.Email}</Email>");
                        //if (!string.IsNullOrEmpty(applicantConsolidated.FirstName))
                        //    Enrollee.Append($"<FirstName>{applicantConsolidated.FirstName}</FirstName>");
                        //if (!string.IsNullOrEmpty(applicantConsolidated.LastName))
                        //    Enrollee.Append($"<LastName>{applicantConsolidated.LastName}</LastName>");
                        //if (applicantPhoneNumber != null && !string.IsNullOrEmpty(applicantPhoneNumber.PhoneNumber))
                        //    Enrollee.Append($"<PhoneNumber>{applicantPhoneNumber.PhoneNumber.Replace("-", "").Replace("_", "")}</PhoneNumber>");
                        Enrollee.Append($"<Zip>{zip.Trim()}</Zip>");
                    } else {
                        Enrollee.Append($"<CountyFIPS>{fips}</CountyFIPS>");
                        Enrollee.Append($"<Zip>{zip.Trim()}</Zip>");
                    }
                }
                var enrollData = $@"<EnrollmentSettings>
                    <Custom1>{qrsApplicantId}</Custom1>
                    <Custom2{userId}</Custom2>
                    <Custom5>CQE</Custom5>
                    <CustomInternalXml>
                        <StoringData>
                            <AgencyID>{_drxConfiguration.AgencyId}</AgencyID>
                            <AgencyName>{_drxConfiguration.AgencyName}</AgencyName>
                        </StoringData>
                    </CustomInternalXml>
                    <EnrollDisplay>
                        <EnrollDisplay>
                            <KeyMessage>AgentMessage</KeyMessage>
                            <ValueMessage>Contact Customer Care</ValueMessage>
                        </EnrollDisplay>
                        <EnrollDisplay>
                            <KeyMessage>AgentMessage1</KeyMessage>
                                <ValueMessage>{contactPhoneNumber}</ValueMessage>
                            </EnrollDisplay >{ttyXml}
                        <EnrollDisplay>
                            <KeyMessage>AgentMessage3</KeyMessage>
                            <ValueMessage>Hours: M-F 7am-8pm CT, Sat 9am-4pm CT, Sun 10am-2pm CT</ValueMessage>
                        </EnrollDisplay>
                </EnrollDisplay>{Enrollee}
                <Enroller><AID>{_drxConfiguration.AffiliateId}</AID>
                <ChannelID>{_drxConfiguration.ChannelId}</ChannelID>
                <PartnerID>{_drxConfiguration.PartnerId}</PartnerID>
                <UserType>{carrier}</UserType>
                </Enroller>
                <Language>en-US</Language>
                </EnrollmentSettings>";
                enrollXml.Append(enrollData);
            } catch (Exception ex) {
                ApiLogService.LogException(ex, UserId);
            }
            return enrollXml.ToString();
        }        
    }
}
