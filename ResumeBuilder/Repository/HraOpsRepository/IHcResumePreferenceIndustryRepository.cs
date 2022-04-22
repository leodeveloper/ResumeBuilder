using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHcResumePreferenceIndustryRepository
    {
        Task<Webresponse<HcResumePreferenceIndustryViewModel_Get>> Delete(long rid);
        Task<Webresponse<HcResumePreferenceIndustryViewModel_Get>> GetById(long rid);
        Task<Webresponse<IList<HcResumePreferenceIndustryViewModel_Get>>> GetByResumeId(long resumeId);
        Task<WebresponseNoData> Insert(HcResumePreferenceIndustryViewModel hcPreferenceIndustryResumeBankVm, long userid);
        Task<WebresponseNoData> Update(HcResumePreferenceIndustryViewModel hcPreferenceIndustryResumeBankVm, long rid, long userid);
    }
}