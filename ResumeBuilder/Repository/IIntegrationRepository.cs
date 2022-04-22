using IntegrationApiClassLibrary.Model;
using ResumeBuilder.Models;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface IIntegrationRepository
    {
        Task<WebresponseNoData> UpdateJobSeekerPersonalInfo(long resumeId);
        Task<WebresponseNoData> InsertUpdatePensionfund(long resumeId);
    }
}