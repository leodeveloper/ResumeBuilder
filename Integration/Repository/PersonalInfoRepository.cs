using System;
using System.Threading.Tasks;
using Serilog;
using Microsoft.Extensions.Logging;
using Integration.Abstract;
using Integration.Helper;
using Integration.Model;

namespace Integration.Repository
{
    public class PersonalInfoRepository : IPersonalInfoRepository
    {

        private readonly IPersonalInfo _iPersonalInfo;
        private readonly ILogger<PersonalInfoRepository> _iLogger;

        public PersonalInfoRepository(IPersonalInfo iPersonalInfo, ILogger<PersonalInfoRepository> iLogger)
        {
            _iPersonalInfo = iPersonalInfo;
            _iLogger = iLogger;
        }

        public async Task<WebResponse<PersonalInfo>> GetPersonalInfoByEmirateID(string emiratesID)
        {
            WebResponse<PersonalInfo> webResponse = new Helper.WebResponse<PersonalInfo>();
            try
            {
                PersonalInfo personalInfo = await _iPersonalInfo.GetEmaritesByEmirateID(emiratesID);
                if(personalInfo !=  null)
                {
                    webResponse.data = personalInfo;
                }
                webResponse.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message, ex);
                webResponse.message = ex.Message;
                webResponse.status = APIStatus.error;
            }           
            return webResponse; 
        }
    }
}
