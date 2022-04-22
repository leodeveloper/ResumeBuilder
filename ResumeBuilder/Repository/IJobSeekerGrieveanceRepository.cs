using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface IJobSeekerGrieveanceRepository
    {
        Task<WebresponsePaging<IList<JobSeekerGrieveance>>> GetAlljobSeekerGrieveance(int pageNumber, int rowCount);
        Task<Webresponse<JobSeekerGrieveance>> GetjobSeekerGrieveanceById(long rid);
        Task<Webresponse<IList<JobSeekerGrieveance>>> GetjobSeekerGrieveanceByResumeId(long resumeId);
        Task<bool> InsertjobSeekerGrieveance(JobSeekerGrieveance jobSeekerGrieveance);
        Task<bool> UpdatejobSeekerGrieveance(JobSeekerGrieveance jobSeekerGrieveance);
    }
}