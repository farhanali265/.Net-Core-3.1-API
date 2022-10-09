using Microsoft.EntityFrameworkCore;
using SQ.Senior.Clients.QrsService.Infrastructure.Constants;
using SQ.Senior.Clients.QrsService.Infrastructure.IQRSServices;
using SQ.Senior.Clients.QrsService.Models;
using SQ.Senior.Quoting.DatabaseAdapter.Abstractions;
using SQ.Senior.Quoting.DatabaseAdapter.Models;
using SQ.Senior.Quoting.External.Services.IServices;
using SQ.Senior.Quoting.External.Services.ViewModels.Fips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQ.Senior.Quoting.External.Services {
    public class ZipFipsMappingService : BaseService, IZipFipsMappingService {
        private readonly ISQSeniorExternalQuotingDbContext _dbContext;
        private readonly IQrsService _qrsService;

        public ZipFipsMappingService(ISQSeniorExternalQuotingDbContext dbContext, IQrsService qrsService, IUserService userService) : base(userService) {
            _qrsService = qrsService;
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<ZipFipsMapping>> GetFipsCode(string zipCode)
            => await _dbContext.Set<ZipFipsMapping>().Where(x => x.ZipCode == zipCode).Distinct().ToListAsync();

        public async Task<List<FipsMappingResponse>> GetFipsMappingResponsesAsync(string zipCode) {
            var fipsMappings = await GetFipsCode(zipCode);

            if (fipsMappings == null || !fipsMappings.Any()) {
                return new List<FipsMappingResponse>();
            }

            return fipsMappings.Select(mapping => new FipsMappingResponse {
                FipsCode = mapping.CountyFipsCode,
                CountyName = mapping.CountyName,
                StateCode = mapping.StateCode
            }).ToList();
        }

        public async Task<ZipFipsMapping> GetRegisterdUserZipDetail(string Email)
            => await (from res in _dbContext.Set<AspNetUser>()
                      join z in _dbContext.Set<ZipFipsMapping>() on res.ZipCode equals z.ZipCode
                      where res.Email == Email && res.FipsCode == z.CountyFipsCode
                      select z).Distinct().FirstOrDefaultAsync();

        public async Task UpdateUserZipCodeAsync(string Id, string zipCode, long interactionId) {
            var user = await _dbContext.Set<AspNetUser>().FindAsync(Id);
            if (user != null) {
                // qrs only run when existing zip code !== current zip code
                if (string.IsNullOrEmpty(user.ZipCode) || !user.ZipCode.Equals(zipCode) || !(user.QrsContactId > 0)) {
                    if (user.QrsContactId > 0) {
                        await _qrsService.DeleteQrsDataAsync(endPoint: Endpoints.ApplicantContact, recordId: user.QrsContactId, userId: user.Id);
                    }
                    user.QrsContactId = await _qrsService.AddZipFipCode(contact: new QRSContact() { ZipCode = zipCode, InteractionId = interactionId }, interactionId: interactionId, userId: user.Id);
                }
                user.ZipCode = zipCode;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateUserFipsCodeAsync(string id, string fipsCode, long interactionId) {
            var user = await _dbContext.Set<AspNetUser>().FindAsync(id);
            if (user != null) {
                // qrs only run when existing fip code !== current fip code
                if (user.QrsContactId != null) {
                    var qrsContact = new QRSContact() {
                        ZipCode = user.ZipCode,
                        FipsCode = fipsCode,
                        InteractionId = interactionId,
                        QrsContactId = (int)user.QrsContactId
                    };
                    user.QrsContactId = await _qrsService.AddZipFipCode(qrsContact, interactionId, user.Id);
                }

                user.FipsCode = fipsCode;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<string> GetCountyName(string fipsCode)
            => (await _dbContext
            .Set<ZipFipsMapping>()
            .FirstOrDefaultAsync(x => x.CountyFipsCode == fipsCode)
            ).CountyName;

        public async Task<ZipFipsMapping> GetZipFipsDetailByUserId(string userId)
            => await (from user in _dbContext.Set<AspNetUser>()
                      join zipDetail in _dbContext.Set<ZipFipsMapping>() on user.ZipCode equals zipDetail.ZipCode
                      where user.Id == userId && user.ZipCode == zipDetail.ZipCode
                      select zipDetail).Distinct().FirstOrDefaultAsync();
    }
}
