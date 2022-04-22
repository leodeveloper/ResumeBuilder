using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHcResumeCertificationsRepository
    {
        Task<Webresponse<HcResumeCertificationsViewModel_Get>> Delete(long rid);
        Task<Webresponse<HcResumeCertificationsViewModel_Get>> GetById(long rid);
        Task<Webresponse<IList<HcResumeCertificationsViewModel_Get>>> GetByResumeId(long resumeId);
        Task<WebresponseNoData> Insert(HcResumeCertificationsViewModel hcCertificationsResumeBankVm, long userid);
        Task<WebresponseNoData> Update(HcResumeCertificationsViewModel hcCertificationsResumeBankVm, long rid, long userid);
    }
}