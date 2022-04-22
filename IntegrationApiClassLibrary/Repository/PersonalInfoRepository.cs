using System;
using System.Threading.Tasks;
using Serilog;
using Microsoft.Extensions.Logging;
using IntegrationApiClassLibrary.Model;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace IntegrationApiClassLibrary.Repository
{
    public class PersonalInfoRepository : IPersonalInfoRepository
    {
        private readonly ILogger<PersonalInfoRepository> _iLogger;
        private IHttpClientFactory _httpClientFactory;

        public PersonalInfoRepository(ILogger<PersonalInfoRepository> iLogger, IHttpClientFactory httpClientFactory)
        {
            _iLogger = iLogger;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PersonalInfo> GetPersonalInfoByEmirateID(string emiratesID)
        {           
            try
            {
                using(var httpClient = _httpClientFactory.CreateClient("IntegrationApi"))
                {
                    PersonalInfoPost personalInfoPost = new PersonalInfoPost { emiratesID=emiratesID };
                    HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync<PersonalInfoPost>("/GradeAPI/api/Grade/verifyemiratesID", personalInfoPost);
                    if(responseMessage.IsSuccessStatusCode)
                    {
                        Webresponse<PersonalInfo> personalInfo = await responseMessage.Content.ReadFromJsonAsync<Webresponse<PersonalInfo>>();
                        if(personalInfo?.data != null)
                            return personalInfo.data;
                    }
                }
                return null;              
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message, ex);
                return null;
            }   
        }

        /// <summary>
        /// Getting pension fund from the Integration API
        /// </summary>
        /// <param name="pensionfundPost"></param>
        /// <returns></returns>
        public async Task<PensionfundDto> GetPensionfund(PensionfundPost pensionfundPost)
        {
            try
            {
                
                using (var httpClient = _httpClientFactory.CreateClient("IntegrationApiWithPort"))
                {
                    
                    HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync<PensionfundPost>("/pensionfund/", pensionfundPost);
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        Webresponse<PensionfundDto> pensionfundDto = await responseMessage.Content.ReadFromJsonAsync<Webresponse<PensionfundDto>>();
                        if (pensionfundDto?.data != null)
                            return pensionfundDto.data;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                _iLogger.LogError(ex.Message, ex);
                return null;
            }
        }
    }
}
