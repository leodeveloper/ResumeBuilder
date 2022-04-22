using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface IPeopleofDeterminationRepository
    {
        Task<bool> DeletejobSeekerPod(long rid);
        Task<WebresponsePaging<IList<JobSeekerPod>>> GetAlljobSeekerPod(int pageNumber, int rowCount);
        Task<Webresponse<JobSeekerPod>> GetjobSeekerPodById(long rid);
        Task<Webresponse<IList<JobSeekerPod>>> GetjobSeekerPodByResumeId(long resumeId);
        Task<Webresponse<IList<SpecialNeedDto>>> GetJobSeekerPodByResumeIdPreview(long resumeId);
        Task<bool> InsertDeletejobSeekerPod(JobSeekerPodViewModel jobSeekerPodVM);
        Task<bool> InsertjobSeekerPod(JobSeekerPod jobSeekerPod);
        Task<bool> UpdatejobSeekerPod(JobSeekerPod jobSeekerPod);
    }
}