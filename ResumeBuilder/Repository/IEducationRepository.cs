using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface IEducationRepository
    {
        Task<WebresponsePaging<IList<Education>>> GetAlleducation(int pageNumber, int rowCount);
        Task<Webresponse<Education>> GeteducationById(long rid);
        Task<Webresponse<IList<Education>>> GeteducationByResumeId(long resumeId);
        Task<bool> Inserteducation(Education education);
        Task<bool> Updateeducation(Education education);
        Task<bool> DeleteEducation(long Rid);
        Task<Webresponse<IList<EducationDto>>> GeteducationByResumeIdPreview(long resumeId);
    }
}