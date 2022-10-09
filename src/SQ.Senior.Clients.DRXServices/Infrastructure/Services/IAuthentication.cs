using SQ.Senior.Clients.DrxServices.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SQ.Senior.Clients.DrxServices.Infrastructure.Services {
    public interface IAuthentication {
        Task<DrxToken> GetDrxTokenAsync();
        Task<DrxSession> GetDrxSessionAsync(List<SelectedPrescriptions> prescriptions, ApplicantCompleteInfo applicant, int healthStatus, DrxToken drxToken, Action<string> setDrxXml = null);
        string GenerateDrxSession(List<SelectedPrescriptions> prescriptions, ApplicantCompleteInfo applicant, int healthStatus);
    }
}
