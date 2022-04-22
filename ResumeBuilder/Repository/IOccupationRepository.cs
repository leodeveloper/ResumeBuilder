using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface IOccupationRepository
    {
        Task<bool> Deleteoccupation(long rid);
        Task<WebresponsePaging<IList<Occupation>>> GetAlloccupation(int pageNumber, int rowCount);
        Task<Webresponse<Occupation>> GetoccupationById(long rid);
        Task<Webresponse<IList<Occupation>>> GetoccupationByResumeId(long resumeId);
        Task<Webresponse<IList<OccupationDto>>> GetoccupationByResumeIdPreview(long resumeId);
        Task<bool> Insertoccupation(Occupation occupation);
        Task<bool> Updateoccupation(Occupation occupation);
    }
}