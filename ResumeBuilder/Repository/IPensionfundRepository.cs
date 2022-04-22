using ResumeBuilder.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public interface IPensionfundRepository
    {
        Task<bool> DeletepensionFund(string nationId);
        Task<WebresponsePaging<IList<Pensionfund>>> GetAllpensionFund(int pageNumber, int rowCount);
        Task<Webresponse<Pensionfund>> GetpensionFundByNationId(string nationId);

        Task<bool> InsertpensionFund(Pensionfund pensionFund);

        Task<bool> UpdatepensionFund(Pensionfund pensionFund);
    }
}