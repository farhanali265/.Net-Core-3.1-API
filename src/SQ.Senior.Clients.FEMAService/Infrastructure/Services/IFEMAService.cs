using SQ.Senior.Clients.FEMAService.Models;
using System;
using System.Threading.Tasks;

namespace SQ.Senior.Clients.FEMAService.Infrastructure.Services
{
    public interface IFEMAService
    {
        Task<DisasterDeclarationsSummariesResponse> GetDisasterDeclarationsSummaries(FEMARequest femaRequest);
    }
}
