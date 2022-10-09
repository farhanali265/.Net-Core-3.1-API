using Microsoft.EntityFrameworkCore;
using SQ.Senior.Clients.QrsService.Infrastructure.Constants;
using SQ.Senior.Clients.QrsService.Infrastructure.IQRSServices;
using SQ.Senior.Clients.QrsService.Models;
using SQ.Senior.Quoting.DatabaseAdapter.Abstractions;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using SQ.Senior.Quoting.External.Services.Infrastructure.Constants;
using SQ.Senior.Quoting.External.Services.IServices;
using SQ.Senior.Quoting.External.Services.ViewModels.Plan;
using SQ.Senior.Quoting.External.Services.ViewModels.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services {
    public class ProviderService : BaseService, IProviderService {
        private readonly ISQSeniorExternalQuotingDbContext _dbContext;
        private readonly IQrsProviderService _qrsProviderService;
        private readonly IApplicantService _applicantService;
        private readonly IQrsService _qrsService;
        public ProviderService(ISQSeniorExternalQuotingDbContext dbContext, IQrsProviderService qrsProviderService, IApplicantService applicantService, IQrsService qrsService, IUserService userService) : base(userService) {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _qrsProviderService = qrsProviderService;
            _applicantService = applicantService;
            _qrsService = qrsService;
        }

        public async Task<IEnumerable<ProviderResponse>> GetApplicantProviderAsync(string userId, bool isActiveProviders = true) {
            List<ApplicantProvider> providers;
            if (isActiveProviders) {
                providers = await _dbContext.Set<ApplicantProvider>().Where(x => x.UserId == userId && x.IsActive == isActiveProviders).OrderBy(x => x.Id).ToListAsync();
            } else {
                providers = await _dbContext.Set<ApplicantProvider>().Where(x => x.UserId == userId).OrderBy(x => x.Id).ToListAsync();
            }
            return providers.Select(provider => GetProviderResponseMapping(provider));
        }

        public async Task<QRSSearchProvider> SearchProviders(SearchProvidersRequest searchRequest) {
            var qrsFilterCriteria = SearchProvidersMapping(searchRequest);
            return await _qrsProviderService.SearchProviderAsync(qrsFilterCriteria, searchRequest.Page, searchRequest.Limit);
        }
        private (string, string, Contact) GetProviderInfo(List<ProviderDetail> providerDetails) {

            if (providerDetails is null || !providerDetails.Any()) {
                return (string.Empty, string.Empty, null);
            }
            var phone = providerDetails[0].Contacts?.Where(x => x.ContactTypeId == ProviderContactType.Phone).Select(x => x.PhoneNumber).FirstOrDefault() ?? string.Empty;
            var addressInfo = providerDetails[0].Contacts?.FirstOrDefault(x => x.ContactTypeId == ProviderContactType.Address);
            var taxonomy = providerDetails[0].Taxonomy?.FirstOrDefault(x => x.is_primary == ProviderInfo.TaxanomyType);
            var taxonamyText = $"{taxonomy?.NationalProviderIdentifierTaxonomyCode?.Classification ?? string.Empty} {taxonomy?.NationalProviderIdentifierTaxonomyCode?.Specialization ?? string.Empty}";
            return (phone, taxonamyText, addressInfo);
        }

        public async Task<ApplicantProvider> GetProviderByNpiNumberAsync(int nationalProviderIdentifier) {

            var applicantProvider = new ApplicantProvider();
            if (nationalProviderIdentifier < 1) {
                return applicantProvider;
            }
            var qrsProvider = new QRSProviderFilterCriteria { NationalProviderIdentifier = nationalProviderIdentifier };
            var qrsProviders = await _qrsProviderService.SearchProviderAsync(qrsProvider, ProviderInfo.PageNo, ProviderInfo.Limit);
            if (qrsProviders?.ProviderDetails?.Any() ?? false) {
                applicantProvider = ApplicantProviderMapping(qrsProviders.ProviderDetails[0], GetProviderInfo(qrsProviders.ProviderDetails));
            }
            return applicantProvider;
        }
        public async Task<bool> SaveProvidersAsync(int nationalProviderIdentifier, string userId, BrandType brandType, List<ApplicantProvider> applicantProviders = null, bool isAqe = false) {

            var status = true;
            if (isAqe && applicantProviders != null && applicantProviders.Any()) {
                foreach (var applicantProvider in applicantProviders) {
                    applicantProvider.UserId = userId;
                    applicantProvider.QrsProviderId = await ManageQRSProviderAsync(applicantProvider, brandType);
                    _dbContext.Set<ApplicantProvider>().Add(applicantProvider);
                }
            } else {
                if (nationalProviderIdentifier < 1) {
                    return false;
                }
                var applicantprovider = await GetProviderByNpiNumberAsync(nationalProviderIdentifier);
                if (applicantprovider is null) {
                    return false;
                }
                var isNewProvider = false;
                var existingProvider = _dbContext.Set<ApplicantProvider>().FirstOrDefault(provider => provider.UserId == userId && provider.NationalProviderIdentifier == applicantprovider.NationalProviderIdentifier);
                if (existingProvider is null) {
                    isNewProvider = true;
                    existingProvider = applicantprovider;
                    existingProvider.IsActive = true;
                } else {
                    if (existingProvider.IsActive) {
                        status = false;
                    } else {
                        existingProvider.IsActive = true;
                        status = true;
                    }
                }
                existingProvider.UserId = userId;
                existingProvider.QrsProviderId = await ManageQRSProviderAsync(existingProvider, brandType);
                if (isNewProvider) {
                    _dbContext.Set<ApplicantProvider>().Add(existingProvider);
                    status = true;
                }
            }
            await _dbContext.SaveChangesAsync();
            return status;
        }

        public async Task<int> ManageQRSProviderAsync(ApplicantProvider provider, BrandType brandType, long interactionId = 0) {

            interactionId = interactionId > 0 ? interactionId : await _applicantService.GetCurrentInteractionIdAsync(provider.UserId, brandType);
            if (interactionId == 0)
                return 0;

            var qrsProvider = ProviderMapping(provider);
            qrsProvider.InteractionId = interactionId;
            return await _qrsProviderService.AddProviderAsync(qrsProvider, interactionId, provider.UserId);

        }

        public async Task<int> DeleteProviderAsync(string userId, int providerId) {

            var provider = await _dbContext.Set<ApplicantProvider>().FirstOrDefaultAsync(x => x.UserId == userId && x.Id == providerId);
            if (provider is null)
                return 0;

            provider.IsActive = false;
            await _dbContext.SaveChangesAsync();
            if (provider.QrsProviderId > 0) {
                await _qrsService.DeleteQrsDataAsync(Endpoints.ApplicantProvider, provider.QrsProviderId, userId);
            }

            return provider.Id;
        }
    }
}
