using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface IEmployerRepository
    {
        Task<WebresponsePaging<IList<Employer>>> GetAllemployer(int pageNumber, int rowCount);
        Task<Webresponse<Employer>> GetemployerById(long rid);
        Task<Webresponse<IList<Employer>>> GetemployerByResumeId(long resumeId);
        Task<bool> Insertemployer(Employer employer);
        Task<bool> Updateemployer(Employer employer);
        Task<bool> Deleteemployer(long Rid);
        Task<Webresponse<IList<EmployerDto>>> GetemployerByResumeIdPreview(long resumeId);
    }
}