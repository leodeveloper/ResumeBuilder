using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHcResumeEmployerRepository
    {
        Task<Webresponse<HcResumeEmployerViewModel_Get>> Delete(long rid);
        Task<Webresponse<HcResumeEmployerViewModel_Get>> GetById(long rid);
        Task<Webresponse<IList<HcResumeEmployerViewModel_Get>>> GetByResumeId(long resumeId);
        Task<WebresponseNoData> Insert(HcResumeEmployerViewModel hcEmployerResumeBankVm, long userid);
        Task<WebresponseNoData> Update(HcResumeEmployerViewModel hcEmployerResumeBankVm, long rid, long userid);
    }
}