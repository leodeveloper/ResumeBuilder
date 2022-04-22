using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface IReferenceRepository
    {
        Task<WebresponsePaging<IList<Reference>>> GetAllreference(int pageNumber, int rowCount);
        Task<Webresponse<IList<Reference>>> GetreferenceByResumeId(long resumeId);
        Task<Webresponse<Reference>> GetreferenceById(long rid);
        Task<bool> Insertreference(Reference reference);
        Task<bool> Updatereference(Reference reference);
        Task<bool> Deletereference(long Rid);
    }
}