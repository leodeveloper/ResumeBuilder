using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface IlookupRespository
    {
        Task<Webresponse<IList<Status>>> GetAllStatus();
        Task<Webresponse<IList<LookupReason>>> GetReasonWithStatusId();
        Task<Webresponse<IList<DocumentType>>> GetAllDocumentType();
        Task<T> GetHttpClient<T>(string Url);
        IList<MartialStatus> GetMartialStatus(int? id = 0);
        IList<Gender> GetGender(int? id = 0);
        IList<MilitaryServiceStatus> GetMilitaryServiceStatus(int? id = 0);
        IList<Salutation> GetSalutation(int? id = 0);
    }
}