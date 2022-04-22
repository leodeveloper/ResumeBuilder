using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface ILanguagesRepository
    {
        Task<bool> DeletejobSeekerLanguage(long Rid);
        Task<WebresponsePaging<IList<JobSeekerLanguages>>> GetAlljobSeekerLanguage(int pageNumber, int rowCount);
        Task<Webresponse<JobSeekerLanguages>> GetjobSeekerLanguageById(long rid);
        Task<Webresponse<IList<JobSeekerLanguages>>> GetjobSeekerLanguageByResumeId(long resumeId);
        Task<bool> InsertjobSeekerLanguage(JobSeekerLanguages jobSeekerLanguage);
        Task<bool> UpdatejobSeekerLanguage(JobSeekerLanguages jobSeekerLanguage);
    }
}