using IntegrationApiClassLibrary.Model;
using System.Threading.Tasks;

namespace IntegrationApiClassLibrary.Repository
{
    public interface IPersonalInfoRepository
    {
        Task<PersonalInfo> GetPersonalInfoByEmirateID(string emiratesID);
        Task<PensionfundDto> GetPensionfund(PensionfundPost pensionfundPost);
    }
}