using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public interface IHcResumeEngagementRepository
    {
        Task<Webresponse<HcResumeEngagementViewModel_Get>> Delete(long rid);
        Task<Webresponse<HcResumeEngagementViewModel_Get>> GetById(long rid);
        Task<Webresponse<IList<HcResumeEngagementViewModel_Get>>> GetAllOrByAdvisorId(HcResumeEngagementViewModel_GetByAdvisor hcResumeEngagementViewModel_GetByAdvisor);
        Task<Webresponse<IList<HcResumeEngagementViewModel_Get>>> GetByResumeId(long resumeId);
        Task<WebresponseNoData> Insert(HcResumeEngagementViewModel hcEngagementResumeBankVm, int userid);
        Task<WebresponseNoData> Update(HcResumeEngagementViewModel hcEngagementResumeBankVm, long rid, int userid);
        Task<WebresponseNoData> UpdateJobSeekerStatus(HcResumeEngagementStatusNotes hcResumeEngagementStatusNotes);
    }
}