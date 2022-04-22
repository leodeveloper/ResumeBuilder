using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface INotesRepository
    {
        Task<bool> Deletenotes(long Rid);
        Task<WebresponsePaging<IList<Notes>>> GetAllnotes(int pageNumber, int rowCount);
        Task<Webresponse<Notes>> GetnotesById(long rid);
        Task<Webresponse<IList<Notes>>> GetnotesByResumeId(long resumeId);
        Task<bool> Insertnotes(Notes notes);
        Task<bool> Updatenotes(Notes notes);
    }
}