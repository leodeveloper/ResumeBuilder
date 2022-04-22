using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public class TypeSenceApiRepository : ITypeSenceApiRepository
    {
        readonly ILogger<TypeSenceApiRepository> _ilogger;
        private IOptions<AppSettings> _appsetting;
        public TypeSenceApiRepository(IOptions<AppSettings> appsetting, ILogger<TypeSenceApiRepository> ilogger)
        {
            _appsetting = appsetting;
            _ilogger = ilogger;
        }

        /// <summary>
        /// Fire and forget
        /// </summary>
        /// <param name="resumeId"></param>
        public async void UpdateTypeSenceIndex(long resumeId)
        {
            try
            {
                using (HttpClient _httpClient = new HttpClient())
                {
                    var response = await _httpClient.PutAsJsonAsync<long>($"{_appsetting.Value.TypeSenceApi}JobSeeker?rid={resumeId}", resumeId);
                    if (response.IsSuccessStatusCode)
                    {
                        Webresponse<JobSeekerResume> webresponse = await response.Content.ReadFromJsonAsync<Webresponse<JobSeekerResume>>();
                        if(webresponse.status != APIStatus.success)
                        {
                            _ilogger.LogError($"TypeSenceApiRepository : UpdateTypeSenceIndex : Api call failed for this jobseeker id {resumeId}");
                        }
                    }
                    else
                    {
                        _ilogger.LogError($"TypeSenceApiRepository : UpdateTypeSenceIndex : Api call failed for this jobseeker id {resumeId}");
                    }
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError("TypeSenceApiRepository : UpdateTypeSenceIndex : " + ex.Message);
            }
        }
    }
}
