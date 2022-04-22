using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHcResumePreferenceCityRepository
    {
        Task<Webresponse<HcResumePreferenceCityViewModel_Get>> Delete(long rid);
        Task<Webresponse<HcResumePreferenceCityViewModel_Get>> GetById(long rid);
        Task<Webresponse<IList<HcResumePreferenceCityViewModel_Get>>> GetByResumeId(long resumeId);
        Task<WebresponseNoData> Insert(HcResumePreferenceCityViewModel hcPreferenceCityResumeBankVm, long userid);
        Task<WebresponseNoData> Update(HcResumePreferenceCityViewModel hcPreferenceCityResumeBankVm, long rid, long userid);
    }
}