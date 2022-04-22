using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHcResumePreferenceEscoJobTitleRepository
    {
        Task<Webresponse<HcResumePreferenceJobTitleViewModel_Get>> Delete(long rid);
        Task<Webresponse<HcResumePreferenceJobTitleViewModel_Get>> GetById(long rid);
        Task<Webresponse<IList<HcResumePreferenceJobTitleViewModel_Get>>> GetByResumeId(long resumeId);
        Task<WebresponseNoData> Insert(HcResumePreferenceJobTitleViewModel hcPreferenceJobTitleResumeBankVm, long userid);
        Task<WebresponseNoData> Update(HcResumePreferenceJobTitleViewModel hcPreferenceJobTitleResumeBankVm, long rid, long userid);
    }
}