using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace ResumeBuilder.Repository
{
    public class TrainingRepository : ITrainingRepository
    {
        readonly ILogger<TrainingRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IGenericRepositoryPaggingDapper<Training> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;
        readonly IUserRepository _iUserRepository;
        readonly IHttpClientFactory _httpClientFactory;

        public TrainingRepository(IHttpClientFactory httpClientFactory, IOptions<AppSettings> appSettings, IUserRepository iUserRepository, ILogger<TrainingRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<Training> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
            _iUserRepository = iUserRepository;
            _httpClientFactory = httpClientFactory;
        }



        public async Task<bool> Inserttraining(Training training)
        {
            try
            {
                int i = await _iUnitOfWork.Connection.InsertAsync<Training>(training);
                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(TrainingRepository)}::{nameof(Inserttraining)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<bool> Updatetraining(Training training)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    return await con.UpdateAsync<Training>(training);
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(TrainingRepository)}::{nameof(Inserttraining)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<bool> Deletetraining(long Rid)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update Training set IsDeleted='true' where RID={Rid}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(TrainingRepository)}::{nameof(Deletetraining)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<Webresponse<Training>> GettrainingById(long rid)
        {
            Webresponse<Training> training = new Webresponse<Training>();
            try
            {

                var result = await _iUnitOfWork.Connection.GetAsync<Training>(rid);
                if (result == null)
                {
                    training.message = "No Record found";
                }
                else
                {
                    training.data = result;
                }
                training.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(TrainingRepository)}::{nameof(GettrainingById)} -- " + ex.Message);
                training.message = ex.Message;
                training.status = APIStatus.error;
            }
            return training;
        }
        public async Task<Webresponse<IList<Training>>> GettrainingByResumeId(long resumeId)
        {
            Webresponse<IList<Training>> training = new Webresponse<IList<Training>>();
            try
            {
                var result = await _iUnitOfWork.Connection.QueryAsync<Training>($"select * from Training where Resume_ID={resumeId} and IsDeleted='false'");
                if (result == null)
                {
                    training.message = "No Record found";
                }
                else
                {
                    training.data = result.ToList();
                }
                training.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(GettrainingByResumeId)} -- " + ex.Message);
                training.message = ex.Message;
                training.status = APIStatus.error;
            }
            return training;
        }
        public async Task<WebresponsePaging<IList<Training>>> GetAlltraining(int pageNumber, int rowCount)
        {
            WebresponsePaging<IList<Training>> training = new WebresponsePaging<IList<Training>>();
            try
            {
                var result = await _igenericRepositoryPaggingDapper.GetAllPagedTotalCountAsync(typeof(Training).GetProperties()[0].Name, pageNumber, rowCount);
                if (result == null && !result.data.Any())
                {
                    training.message = "No Record found";
                }
                else
                {
                    training = result;
                }
                training.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(TrainingRepository)}::{nameof(GetAlltraining)} -- " + ex.Message);
                training.message = ex.Message;
                training.status = APIStatus.error;
            }
            return training;
        }

        public async Task<Webresponse<IList<TrainingBatchDto>>> GetTrainingBatchs(long resumeId, string userName)
        {
            Webresponse<IList<TrainingBatchDto>> webresponse = new Webresponse<IList<TrainingBatchDto>>();
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                try
                {
                    string token = await _iUserRepository.GetToken(userName);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    webresponse = await _httpClient.GetFromJsonAsync<Webresponse<IList<TrainingBatchDto>>>($"{_appSettings.Value.TrainingAPI}/TrainingBatchApi/GetAllTrainingAndBatchsByResumeID?resume_ID={resumeId}");
                }
                catch (Exception ex)
                {
                    _ilogger.LogError(ex, "TrainingRepository:::GetTrainingBatchs api is down");
                    webresponse.status = APIStatus.error;
                    webresponse.message = "Failed to get the training batch, please check the error log";
                }
            }
            return webresponse;
        }

        public async Task<WebresponseNoData> PostTrainingBatchs(TrainingApply trainingApply, string userName)
        {
            WebresponseNoData webresponse = new WebresponseNoData();
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                try
                {
                    string token = await _iUserRepository.GetToken(userName);
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await _httpClient.PostAsJsonAsync<TrainingApply>($"{_appSettings.Value.TrainingAPI}/TrainingBatchApi/JobSeekerTrainingEnrolment", trainingApply);
                    webresponse = JsonConvert.DeserializeObject<WebresponseNoData>(await response.Content.ReadAsStringAsync());
                }
                catch (Exception ex)
                {
                    _ilogger.LogError(ex, "TrainingRepository:::PostTrainingBatchs PostTrainingBatchs api is down");
                    webresponse.status = APIStatus.error;
                    webresponse.message = "Failed to post the trainingApply for detail please check the error log";
                }
            }
            return webresponse;
        }
    }
}
