using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Models;
using System;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using ResumeBuilder.Dto;

namespace ResumeBuilder.Repository
{
    public class lookupRespository : IlookupRespository
    {
        readonly ILogger<lookupRespository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWork _iUnitOfWork;
        private IHttpClientFactory _httpClientFactory;

        public lookupRespository(IHttpClientFactory httpClientFactory, IOptions<AppSettings> appSettings, ILogger<lookupRespository> ilogger, IUnitOfWork iUnitOfWork)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _httpClientFactory = httpClientFactory;
        }

        public IList<MartialStatus> GetMartialStatus(int? id = 0)
        {
            IList<MartialStatus> martialStatus = new List<MartialStatus>();
            martialStatus.Add(new MartialStatus { Id = 1, ArName = "أعزب", EnName = "Single" });
            martialStatus.Add(new MartialStatus { Id = 2, ArName = "متزوج", EnName = "Married" });
            martialStatus.Add(new MartialStatus { Id = 3, ArName = "مطلق", EnName = "Divorced" });
            martialStatus.Add(new MartialStatus { Id = 4, ArName = "أرمل", EnName = "Widowed" });
            if (id > 0)
            {
                return martialStatus.Where(z => z.Id == id).ToList();
            }
            else
            {
                return martialStatus;
            }
        }

        public IList<Gender> GetGender(int? id = 0)
        {
            IList<Gender> gender = new List<Gender>();
            gender.Add(new Gender { Id = 1, ArName = "ذكر", EnName = "Male" });
            gender.Add(new Gender { Id = 2, ArName = "أنثى", EnName = "Female" });
            if (id > 0)
            {
                return gender.Where(z => z.Id == id).ToList();
            }
            else
            {
                return gender;
            }
        }
        public IList<Salutation> GetSalutation(int? id = 0)
        {
            IList<Salutation> salutation = new List<Salutation>();
            salutation.Add(new Salutation { Id = 1,EnName = "Mr." , ArName = "السيد" });
            salutation.Add(new Salutation { Id = 2, EnName = "Mrs.", ArName = "السيدة" });
            salutation.Add(new Salutation { Id = 3, EnName = "Dr.", ArName = "طبيبة" });
            salutation.Add(new Salutation { Id = 4, EnName = "Miss.", ArName = "يفتقد" });
            salutation.Add(new Salutation { Id = 5, EnName = "Ms.", ArName = "تصلب متعدد" });
            if (id > 0)
            {
                return salutation.Where(z => z.Id == id).ToList();
            }
            else
            {
                return salutation;
            }
        }

      

public IList<MilitaryServiceStatus> GetMilitaryServiceStatus(int? id = 0)
        {
            IList<MilitaryServiceStatus> militaryServiceStatuses = new List<MilitaryServiceStatus>();
            militaryServiceStatuses.Add(new MilitaryServiceStatus { Id = 1, ArName = "انضم", EnName = "Joined" });
            militaryServiceStatuses.Add(new MilitaryServiceStatus { Id = 2, ArName = "منجز", EnName = "Completed" });
            militaryServiceStatuses.Add(new MilitaryServiceStatus { Id = 3, ArName = "تحت المعالجة", EnName = "InProcess" });
            militaryServiceStatuses.Add(new MilitaryServiceStatus { Id = 4, ArName = "قضية عذر", EnName = "Excuse Case" });
            militaryServiceStatuses.Add(new MilitaryServiceStatus { Id = 5, ArName = "مؤجل", EnName = "Postponed" });
            militaryServiceStatuses.Add(new MilitaryServiceStatus { Id = 6, ArName = "غير منضم", EnName = "Not Joined" });
            if (id > 0)
            {
                return militaryServiceStatuses.Where(z => z.Id == id).ToList();
            }
            else
            {
                return militaryServiceStatuses;
            }
        }

        public async Task<Webresponse<IList<Status>>> GetAllStatus()
        {
            Webresponse<IList<Status>> statuses = new Webresponse<IList<Status>>();
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var status = await con.GetAllAsync<Status>();
                    if (status == null)
                    {
                        statuses.message = "No Record found";
                    }
                    else
                    {
                        statuses.data = status.ToList();
                    }
                    statuses.status = APIStatus.success;
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(lookupRespository)}::{nameof(GetAllStatus)} -- " + ex.Message);
                statuses.message = ex.Message;
                statuses.status = APIStatus.error;
            }
            return statuses;
        }

        public async Task<Webresponse<IList<DocumentType>>> GetAllDocumentType()
        {
            Webresponse<IList<DocumentType>> documentTypes = new Webresponse<IList<DocumentType>>();
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var status = await con.GetAllAsync<DocumentType>();
                    if (status == null)
                    {
                        documentTypes.message = "No Record found";
                    }
                    else
                    {
                        documentTypes.data = status.ToList();
                    }
                    documentTypes.status = APIStatus.success;
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(lookupRespository)}::{nameof(GetAllDocumentType)} -- " + ex.Message);
                documentTypes.message = ex.Message;
                documentTypes.status = APIStatus.error;
            }
            return documentTypes;
        }

        public async Task<Webresponse<IList<LookupReason>>> GetReasonWithStatusId()
        {
            Webresponse<IList<LookupReason>> reasons = new Webresponse<IList<LookupReason>>();
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var reason = await con.QueryAsync<LookupReason>($"select R.RID,R.Title,R.TitleAr, RS.Status_ID from [dbo].[Reason] as R, [dbo].[Reason_Status] as RS where RS.Reason_ID = R.RID");
                    if (reason == null)
                    {
                        reasons.message = "No Record found";
                    }
                    else
                    {
                        reasons.data = reason.ToList();
                    }
                    reasons.status = APIStatus.success;
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(lookupRespository)}::{nameof(GetReasonWithStatusId)} -- " + ex.Message);
                reasons.message = ex.Message;
                reasons.status = APIStatus.error;
            }
            return reasons;
        }

        public async Task<T> GetHttpClient<T>(string Url)
        {
            using (var _httpClient = _httpClientFactory.CreateClient("LookUPApi"))
            {
                try
                {
                    return await _httpClient.GetFromJsonAsync<T>(Url);
                }
                catch (Exception ex)
                {
                    _ilogger.LogError(ex, $"lookupController:::GetHttpClient lookup GetHttpClient api is down {Url}");
                }
            }
            return default(T);
        }

    }
}
