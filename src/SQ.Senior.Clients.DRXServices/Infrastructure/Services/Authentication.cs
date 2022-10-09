using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using SQ.Senior.Clients.DrxServices.Configurations;
using SQ.Senior.Clients.DrxServices.Infrastructure.Constants;
using SQ.Senior.Clients.DrxServices.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SQ.Senior.Clients.DrxServices.Infrastructure.Services {

    public class Authentication : IAuthentication {
        private readonly HttpClient _httpClient;
        private readonly DrxConfiguration _drxConfiguration;
        private DrxToken drxToken = null;
        private DrxSession drxSession = null;

        //TODO: get user id from http context
        private readonly string UserId = "0";
        public Authentication(HttpClient httpClient, IOptionsSnapshot<DrxConfiguration> drxConfigurationOptions) {
            _httpClient = httpClient;
            _drxConfiguration = drxConfigurationOptions.Value ?? throw new ArgumentNullException(nameof(drxConfigurationOptions));
        }

        public async Task<DrxToken> GetDrxTokenAsync() {
            if (drxToken != null && !string.IsNullOrWhiteSpace(drxToken.AccessToken) && DateTime.UtcNow < drxToken.AccessTokenExpiry)
                return drxToken;

            try {
                var contentType = Endpoints.UrlEncodedContent;
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Endpoints.Basic, _drxConfiguration.APIKey);
                var data = new StringBuilder();
                data.Append(Endpoints.GrantType);
                byte[] byteData = Encoding.UTF8.GetBytes(data.ToString());
                var payload = new StringContent(data.ToString(), Encoding.UTF8, contentType);
                var tokenresponse = await _httpClient.PostAsync($"{_drxConfiguration.LinkAuthentication}{_drxConfiguration.TokenEndpoint}", payload);
                if (tokenresponse is null)
                    return null;

                if (tokenresponse.StatusCode == HttpStatusCode.OK)
                    return JsonConvert.DeserializeObject<DrxToken>(await tokenresponse.Content.ReadAsStringAsync());

                return null;
            } catch (Exception ex) {
                ApiLogService.LogException(ex, UserId);
                return null;
            }
        }
        public async Task<DrxSession> GetDrxSessionAsync(List<SelectedPrescriptions> prescriptions, ApplicantCompleteInfo applicant, int healthStatus, DrxToken drxToken, Action<string> setDrxXml = null) {
            if (drxToken is null && string.IsNullOrWhiteSpace(drxToken.AccessToken) && DateTime.UtcNow > drxToken.AccessTokenExpiry)
                drxToken = await GetDrxTokenAsync();
            try {
                if (drxToken is null)
                    return null;

                if (!string.IsNullOrWhiteSpace(drxToken.SessionId) || DateTime.UtcNow <= drxToken.SessionIdExpiry)
                    return drxSession;

                var drxSessionXml = GenerateDrxSession(prescriptions, applicant, healthStatus);

                if (setDrxXml != null)
                    setDrxXml(drxSessionXml);

                var contentType = Endpoints.XmlContent;
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Endpoints.Bearer, drxToken.AccessToken);
                var sessionResponse = await _httpClient.PostAsync($"{_drxConfiguration.BaseUri}{_drxConfiguration.LinkSessionCreate}", new StringContent(drxSessionXml, Encoding.UTF8, contentType));
                if (sessionResponse is null)
                    return null;
                if (sessionResponse.StatusCode == HttpStatusCode.OK) {
                    using (var xmlData = new StringReader(await sessionResponse.Content.ReadAsStringAsync())) {
                        var sessionSet = new DataSet();
                        sessionSet.ReadXml(xmlData);
                        if (sessionSet.Tables.Count > 0 && sessionSet.Tables.Contains("Session") && sessionSet.Tables[0] != null) {
                            return new DrxSession {
                                SessionId = Convert.ToString(sessionSet.Tables[0].Rows[0]["SessionID"]),
                                Expires = Convert.ToDateTime(sessionSet.Tables[0].Rows[0]["Expires"]),
                            };
                        }
                    }
                }
                return null;

            } catch (Exception ex) {
                ApiLogService.LogException(ex, UserId);
                return null;
            }
        }
        public string GenerateDrxSession(List<SelectedPrescriptions> prescriptions, ApplicantCompleteInfo applicant, int healthStatus) {
            var drxSessionXml = new StringBuilder("<ArrayOfSession><Session>");
            var profileXml = new StringBuilder("<Profile>");
            var subsidyXml = new StringBuilder();
            var dosageXml = GeneratePrescriptionXml(prescriptions);
            drxSessionXml.Append(dosageXml);
            if (applicant != null && applicant.BirthDate != null && Convert.ToDateTime(applicant.BirthDate) != DateTime.MinValue)
                drxSessionXml.Append($"<Birthdate>{applicant.BirthDate}</Birthdate>");

            if (healthStatus >= 3)
                drxSessionXml.Append($"<HealthStatus>{healthStatus}</HealthStatus>");

            profileXml
                .Append($"<FirstName>{applicant.FirstName}</FirstName>")
                .Append($"<LastName>{applicant.LastName}</LastName>")
                .Append($"<EmailAddress>{applicant.EmailAddress}</EmailAddress>")
            .Append("</Profile>");

            if (applicant.SubsidyLevel != -1) {
                subsidyXml.Append($"<SubsidyLevel>{applicant.SubsidyLevel}</SubsidyLevel>");
                if (applicant.SubsidyLevel == 3)
                    subsidyXml.Append($"<SubsidyPercent>{applicant.SubsidyPercent}</SubsidyPercent>");
            }
            drxSessionXml.Append(profileXml);
            drxSessionXml.Append(subsidyXml);
            drxSessionXml.Append("</Session></ArrayOfSession>");
            return drxSessionXml.ToString();
        }

        private string GeneratePrescriptionXml(List<SelectedPrescriptions> prescriptions) {
            var dosageXml = new StringBuilder("<Dosage>");
            if (prescriptions == null)
                return dosageXml.Append("</Dosage>").ToString();

            foreach (var item in prescriptions) {
                if (!string.IsNullOrEmpty(item.DosageId) && item.DosageId.Equals("N/A") && item.SelectedPrescriptionInfo.Dosages.Count() > 0) {
                    var dosage = item.SelectedPrescriptionInfo.Dosages.FirstOrDefault(x => x.DosageId == item.DosageId);
                    if (dosage != null) {
                        dosageXml.Append($"<DaysOfSupply>{dosage.CommonDaysOfSupply}</DaysOfSupply>");
                        if (!string.IsNullOrEmpty(item.Package) && item.Package.Equals("N/A")) {
                            var selectedPackage = dosage.Packages.FirstOrDefault(x => x.ReferenceNDC == item.Package);
                            if (selectedPackage != null) {
                                var amount = (item.AmountPerThirtyDays * selectedPackage.TotalPackageQuantity).ToString(CultureInfo.InvariantCulture);
                                dosageXml.Append($"<MetricQuantity>{amount}</MetricQuantity>");
                            }
                            dosageXml.Append($"<NDC>{item.Package}</NDC>");
                        } else {
                            dosageXml.Append($"<MetricQuantity>{item.AmountPerThirtyDays}</MetricQuantity>");
                            dosageXml.Append($"<NDC>{dosage.ReferenceNDC}</NDC>");
                        }
                    }
                }
            }

            return dosageXml.Append("</Dosage>").ToString();
        }
    }
}
