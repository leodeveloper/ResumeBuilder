using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHcResumeEducationRepository
    {
        Task<Webresponse<HcResumeEducationViewModel_Get>> Delete(long rid);
        Task<Webresponse<HcResumeEducationViewModel_Get>> GetById(long rid);
        Task<Webresponse<IList<HcResumeEducationViewModel_Get>>> GetByResumeId(long resumeId);
        Task<WebresponseNoData> Insert(HcResumeEducationViewModel hcEducationResumeBankVm, long userid);
        Task<WebresponseNoData> Update(HcResumeEducationViewModel hcEducationResumeBankVm, long rid, long userid);
    }
}