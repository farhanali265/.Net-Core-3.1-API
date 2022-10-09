using SQ.Senior.Clients.QrsService.Infrastructure.Constants;
using SQ.Senior.Clients.QrsService.Infrastructure.IQRSServices;
using SQ.Senior.Clients.QrsService.Models;
using System;
using System.Threading.Tasks;

namespace SQ.Senior.Clients.QrsService.Infrastructure.Services {
    public class QrsProviderService : IQrsProviderService {
        private readonly INpiApiClient _npiApiClient;
        private readonly IQrsApiClient _qrsApiClient;

        public QrsProviderService(INpiApiClient npiApiClient, IQrsApiClient qrsApiClient) {
            _npiApiClient = npiApiClient ?? throw new ArgumentNullException(nameof(npiApiClient));
            _qrsApiClient = qrsApiClient ?? throw new ArgumentNullException(nameof(qrsApiClient));
        }

        public async Task<QRSSearchProvider> SearchProviderAsync(QRSProviderFilterCriteria searchProviderCriteria, int pageNo, int limit, string userId) {
            var providerEndpoint = Endpoints.ProviderSearch
                .Replace(Endpoints.Page, pageNo.ToString())
                .Replace(Endpoints.Limit, limit.ToString());

            return await _npiApiClient.PostRequestAsync<QRSProviderFilterCriteria, QRSSearchProvider>(providerEndpoint, searchProviderCriteria, userId);
        }

        public async Task<int> AddProviderAsync(QRSProvider provider, long interactionId, string userId = "") {
            var endPoint = string.Format(Endpoints.Provider, interactionId);
            var qrsProviderId = 0;
            QRSProvider qrsProvider = null;
            var isNewProvider = true;
            try {

                if (provider is null)
                    return qrsProviderId;

                if (provider.QrsProviderId > 0) {
                    var existingProvider = await GetProviderAsync((int)provider.QrsProviderId, userId);
                    if (existingProvider != null) {
                        isNewProvider = false;
                        var editProvider = provider;
                        editProvider.Id = existingProvider.Id;
                        editProvider.IsActive = true;
                        editProvider.DateModified = DateTime.UtcNow;
                        editProvider.DateDeleted = null;
                        qrsProvider = await _qrsApiClient.PatchRequestAsync(Endpoints.ApplicantProvider, (int)provider.QrsProviderId, editProvider, userId);
                    }
                }
                if (isNewProvider) {
                    provider.DateCreated = DateTime.UtcNow;
                    qrsProvider = await _qrsApiClient.PostRequestAsync(endPoint, provider, userId);
                }

                if (qrsProvider != null && qrsProvider.Id > 0) {
                    qrsProviderId = qrsProvider.Id;
                }
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                qrsProviderId = 0;
            }
            return qrsProviderId;
        }

        public async Task<QRSProvider> GetProviderAsync(long qrsProviderId, string userId) {
            string endPoint = $"{Endpoints.ApplicantProvider}/{qrsProviderId}";
            try {
                return await _qrsApiClient.GetAsync<QRSProvider>(endPoint, userId);
            } catch (Exception ex) {
                ApiLogService.LogException(ex, userId);
                return null;
            }
        }
    }
}