using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHcResumeLanguagesRepository
    {
        Task<Webresponse<HcResumeLanguagesViewModel_Get>> Delete(long rid);
        Task<Webresponse<HcResumeLanguagesViewModel_Get>> GetById(long rid);
        Task<Webresponse<IList<HcResumeLanguagesViewModel_Get>>> GetByResumeId(long resumeId);
        Task<WebresponseNoData> Insert(HcResumeLanguagesViewModel hcLanguageResumeBankVm, long userid);
        Task<WebresponseNoData> Update(HcResumeLanguagesViewModel hcLanguageResumeBankVm, long rid, long userid);
    }
}