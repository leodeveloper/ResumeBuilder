using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface ICertificationRepository
    {
        Task<bool> Deletecertification(int Id);
        Task<WebresponsePaging<IList<Certification>>> GetAllcertification(int pageNumber, int rowCount);
        Task<Webresponse<Certification>> GetcertificationById(long rid);
        Task<Webresponse<IList<Certification>>> GetcertificationByResumeId(long resumeId);
        Task<bool> Insertcertification(Certification certification);
        Task<bool> Updatecertification(Certification certification);
    }
}