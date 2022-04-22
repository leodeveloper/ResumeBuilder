using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHcResumeSkillRepository
    {
        Task<Webresponse<HcResumeSkillViewModel_Get>> Delete(long rid);
        Task<Webresponse<HcResumeSkillViewModel_Get>> GetById(long rid);
        Task<Webresponse<IList<HcResumeSkillViewModel_Get>>> GetByResumeId(long resumeId);
        Task<WebresponseNoData> Insert(HcResumeSkillViewModel hcSkillResumeBankVm, long userid);
        Task<WebresponseNoData> Update(HcResumeSkillViewModel hcSkillResumeBankVm, long rid, long userid);
    }
}