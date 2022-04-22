using Integration.Helper;
using Integration.Model;
using System.Threading.Tasks;

namespace Integration.Repository
{
    public interface IPersonalInfoRepository
    {
        Task<WebResponse<PersonalInfo>> GetPersonalInfoByEmirateID(string emiratesID);
    }
}