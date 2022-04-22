using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Dto;
using ResumeBuilder.Helper;
using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ResumeBuilder.Helper.EnumHelper;

namespace ResumeBuilder.Repository
{
    public class IntegrationLogRepository : IIntegrationLogRepository
    {

        readonly ILogger<IntegrationLogRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IGenericRepositoryPaggingDapper<IntegrationLogs> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;
        private readonly IOptions<LookUpApiUrl> _appSettingsAPIUrls;
        private IlookupRespository _ilookupRespository;
        readonly IHttpContextAccessor _ihttpContextAccessor;

        public IntegrationLogRepository(IHttpContextAccessor ihttpContextAccessor, IlookupRespository ilookupRespository, IOptions<LookUpApiUrl> appSettingsAPIUrls, IOptions<AppSettings> appSettings, ILogger<IntegrationLogRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<IntegrationLogs> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
            _appSettingsAPIUrls = appSettingsAPIUrls;
            _ilookupRespository = ilookupRespository;
            _ihttpContextAccessor = ihttpContextAccessor;
        }

        public async Task<bool> InsertintegrationLogs(IntegrationEnum integrationEnumName, bool isSuccessfull, string status, long rowId)
        {
            try
            {
                using(var _con = _iUnitOfWork.Connection)
                {
                    _ilogger.LogInformation($"Integration {integrationEnumName.ToString()}, Done status {isSuccessfull}, status {status}");
                    IntegrationLogs integrationLogs = new IntegrationLogs
                    { RowID = rowId, IntegrationName = integrationEnumName.ToString(), IsSuccessfull = isSuccessfull, Status = status, LastUpdateDateTime = DateTime.Now, UserName = _ihttpContextAccessor.HttpContext.User.Identity.Name };
                    int i = await _con.InsertAsync<IntegrationLogs>(integrationLogs);
                    return true;
                }
              
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(IntegrationLogRepository)}::{nameof(InsertintegrationLogs)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<Webresponse<IList<IntegrationLogsDto>>> GetintegrationLogsByIntegrationName(IntegrationEnum initegrationName, long rowId)
        {
            Webresponse<IList<IntegrationLogsDto>> integrationLogs = new Webresponse<IList<IntegrationLogsDto>>();
            try
            {
                using (var _con = _iUnitOfWork.Connection)
                {
                    var result = await _con.QueryAsync<IntegrationLogsDto>($"select [RID],[IntegrationName],[Status],[LastUpdateDateTime],[UserName],[IsSuccessfull] from IntegrationLogs where IntegrationName='{initegrationName.ToString()}' and RowID={rowId} order by LastUpdateDateTime desc");
                    if (result == null)
                    {
                        integrationLogs.message = "No Record found";
                    }
                    else
                    {
                        integrationLogs.data = result.ToList();
                    }
                    integrationLogs.status = APIStatus.success;
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(IntegrationLogRepository)}::{nameof(GetintegrationLogsByIntegrationName)} -- " + ex.Message);
                integrationLogs.message = ex.Message;
                integrationLogs.status = APIStatus.error;
            }
            return integrationLogs;
        }
    }
}
