using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHcResumeRepository
    {
        Task<Webresponse<Hc_ResumeBankViewModel_Get>> Insert(Hc_ResumeBankViewModel hcResumeBankVm, long userid);
        Task<Webresponse<Hc_ResumeBankViewModel_Get>> Update(Hc_ResumeBankViewModel hcResumeBankVm, long userid);
        Task<Webresponse<Hc_ResumeBankViewModel_Get>> GetById(long rid);
        Task<Webresponse<Hc_ResumeBankViewModel_Get>> GetByEmiratesId(string emiratesId);
        Task<Webresponse<Hc_ResumeBankViewModel_Get>> GetByJobSeekerId(string jobseekerId);

        Task<WebresponseNoData> Update(Hc_ContactInfoViewModel hcContactVm, long rid, long userid);
        Task<Webresponse<Hc_ContactInfoViewModel>> GetContactInfoById(long rid);

        Task<WebresponseNoData> Update(Hc_AdditionalInfoViewModel hcAdditionalVm, long rid, long userid);
        Task<Webresponse<Hc_AdditionalInfoViewModel>> GetAdditionalInfoById(long rid);

        Task<WebresponseNoData> UpdateJPCAssessment(long Rid, int JPCAssessment);
        Task<WebresponseNoData> UpdateJPCAssessmentstatus(long Rid, string JPCAssessmentStaus);
        Task<WebresponseNoData> InsertChallanges(HcResumeChallangesViewModel hcResumeChallangesViewModel, int userId);
        Task<Webresponse<HcResumeChallangesViewModel>> GetChallanges(long resumeId);


    }
}