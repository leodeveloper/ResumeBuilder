using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface ISourceRepository
    {
        Task<WebresponsePaging<IList<Source>>> GetAllsource(int pageNumber, int rowCount);
        Task<Webresponse<IList<Source>>> GetsourceByResumeId(long resumeId);
        Task<Webresponse<Source>> GetsourceById(long rid);
        Task<bool> Insertsource(Source source);
        Task<bool> Updatesource(Source source);
        Task<bool> Deletesource(long Rid, string UserName);
        Task<bool> SetOtherIsPriorityFalse(long rid, long resume_id);
    }
}