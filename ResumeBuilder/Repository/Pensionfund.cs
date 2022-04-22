using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public class PensionfundRepository : IPensionfundRepository
    {
        readonly ILogger<PensionfundRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IGenericRepositoryPaggingDapper<Pensionfund> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;
        private readonly IOptions<LookUpApiUrl> _appSettingsAPIUrls;
        private IlookupRespository _ilookupRespository;

        public PensionfundRepository(IlookupRespository ilookupRespository, IOptions<LookUpApiUrl> appSettingsAPIUrls, IOptions<AppSettings> appSettings, ILogger<PensionfundRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<Pensionfund> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
            _appSettingsAPIUrls = appSettingsAPIUrls;
            _ilookupRespository = ilookupRespository;
        }

        public async Task<bool> InsertpensionFund(Pensionfund pensionFund)
        {
            try
            {
                int i = await _iUnitOfWork.Connection.InsertAsync<Pensionfund>(pensionFund);
                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(PensionfundRepository)}::{nameof(InsertpensionFund)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdatepensionFund(Pensionfund pensionFund)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    return await con.UpdateAsync<Pensionfund>(pensionFund);
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(PensionfundRepository)}::{nameof(InsertpensionFund)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<bool> DeletepensionFund(string nationId)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update Pensionfund set IsDeleted='true' where NationId={nationId}");
                    return true;
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(PensionfundRepository)}::{nameof(DeletepensionFund)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<Webresponse<Pensionfund>> GetpensionFundByNationId(string nationId)
        {
            Webresponse<Pensionfund> pensionFund = new Webresponse<Pensionfund>();
            try
            {
                var result = await _iUnitOfWork.Connection.QueryAsync<Pensionfund>($"select * from Pensionfund where NationId='{nationId}'");
                if (result == null || result.Count() == 0)
                {
                    pensionFund.status = APIStatus.NotFound;
                    pensionFund.message = "No Record found";
                }
                else
                {
                    pensionFund.status = APIStatus.success;
                    pensionFund.data = result.FirstOrDefault();
                }                
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(PensionfundRepository)}::{nameof(GetpensionFundByNationId)} -- " + ex.Message);
                pensionFund.message = ex.Message;
                pensionFund.status = APIStatus.error;
            }
            return pensionFund;
        }

        public async Task<WebresponsePaging<IList<Pensionfund>>> GetAllpensionFund(int pageNumber, int rowCount)
        {
            WebresponsePaging<IList<Pensionfund>> pensionFund = new WebresponsePaging<IList<Pensionfund>>();
            try
            {
                var result = await _igenericRepositoryPaggingDapper.GetAllPagedTotalCountAsync(typeof(Pensionfund).GetProperties()[0].Name, pageNumber, rowCount);
                if (result == null && !result.data.Any())
                {
                    pensionFund.message = "No Record found";
                }
                else
                {
                    pensionFund = result;
                }
                pensionFund.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(PensionfundRepository)}::{nameof(GetAllpensionFund)} -- " + ex.Message);
                pensionFund.message = ex.Message;
                pensionFund.status = APIStatus.error;
            }
            return pensionFund;
        }
    }
}
