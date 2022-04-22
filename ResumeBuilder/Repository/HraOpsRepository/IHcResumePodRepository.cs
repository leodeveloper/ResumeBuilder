using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHcResumePodRepository
    {
        Task<Webresponse<HcResumePodViewModel_Get>> Delete(long rid);
        Task<Webresponse<HcResumePodViewModel_Get>> GetById(long rid);
        Task<Webresponse<IList<HcResumePodViewModel_Get>>> GetByResumeId(long resumeId);
        Task<WebresponseNoData> Insert(HcResumePodViewModel hcPodResumeBankVm, long userid);
        Task<WebresponseNoData> Update(HcResumePodViewModel hcPodResumeBankVm, long rid, long userid);
    }
}