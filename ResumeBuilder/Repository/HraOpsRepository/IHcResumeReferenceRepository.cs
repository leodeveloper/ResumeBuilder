using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHcResumeReferenceRepository
    {
        Task<Webresponse<HcResumeReferenceViewModel_Get>> Delete(long rid);
        Task<Webresponse<HcResumeReferenceViewModel_Get>> GetById(long rid);
        Task<Webresponse<IList<HcResumeReferenceViewModel_Get>>> GetByResumeId(long resumeId);
        Task<WebresponseNoData> Insert(HcResumeReferenceViewModel hcReferenceResumeBankVm, long userid);
        Task<WebresponseNoData> Update(HcResumeReferenceViewModel hcReferenceResumeBankVm, long rid, long userid);
    }
}