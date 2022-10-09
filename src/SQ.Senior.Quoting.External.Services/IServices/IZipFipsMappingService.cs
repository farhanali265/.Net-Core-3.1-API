using SQ.Senior.Quoting.DatabaseAdapter.Models;
using SQ.Senior.Quoting.External.Services.ViewModels.Fips;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SQ.Senior.Quoting.External.Services.IServices {
    public interface IZipFipsMappingService {
        Task<List<ZipFipsMapping>> GetFipsCode(string zipCode);
        Task<List<FipsMappingResponse>> GetFipsMappingResponsesAsync(string zipCode);
        Task<ZipFipsMapping> GetRegisterdUserZipDetail(string email);
        Task UpdateUserZipCodeAsync(string id, string zipCode, long interactionId);
        Task UpdateUserFipsCodeAsync(string id, string fipsCode, long interactionId);
        Task<string> GetCountyName(string fipsCode);
        Task<ZipFipsMapping> GetZipFipsDetailByUserId(string userId);
    }
}
