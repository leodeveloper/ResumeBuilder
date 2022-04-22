using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHcResumeCoverLetterRepository
    {
        Task<Webresponse<HcResumeCoverLetterViewModel_Get>> GetById(long rid);
        Task<Webresponse<HcResumeCoverLetterViewModel_Get>> GetByResumeId(long resumeId);
        Task<WebresponseNoData> InsertUpdate(HcResumeCoverLetterViewModel hcCoverLetterResumeBankVm, long rid, long userid);
    }
}