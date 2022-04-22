using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHcResumeBeneficiaryRepository
    {
        Task<Webresponse<HcResumeBeneficiaryViewModel_Get>> Delete(long rid);
        Task<Webresponse<HcResumeBeneficiaryViewModel_Get>> GetById(long rid);
        Task<Webresponse<IList<HcResumeBeneficiaryViewModel_Get>>> GetByResumeId(long resumeId);
        Task<WebresponseNoData> Insert(HcResumeBeneficiaryViewModel hcBeneficiaryResumeBankVm, long userid);
        Task<WebresponseNoData> Update(HcResumeBeneficiaryViewModel hcBeneficiaryResumeBankVm, long rid, long userid);
    }
}