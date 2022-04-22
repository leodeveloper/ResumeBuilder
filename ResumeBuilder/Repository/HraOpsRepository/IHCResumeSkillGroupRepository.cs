using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHCResumeSkillGroupRepository
    {
        Task<Webresponse<HcResumeSkillGroupViewModel_Get>> Delete(long rid);
        Task<Webresponse<HcResumeSkillGroupViewModel_Get>> GetById(long rid);
        Task<Webresponse<IList<HcResumeSkillGroupViewModel_Get>>> GetByResumeId(long resumeId);
        Task<WebresponseNoData> Insert(HcResumeSkillGroupViewModel hcSkillGroupResumeBankVm, long userid);
        Task<WebresponseNoData> Update(HcResumeSkillGroupViewModel hcSkillGroupResumeBankVm, long rid, long userid);
    }
}