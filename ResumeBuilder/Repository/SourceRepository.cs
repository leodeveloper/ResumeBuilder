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
    public class SourceRepository : ISourceRepository
    {
        readonly ILogger<SourceRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IGenericRepositoryPaggingDapper<Source> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;

        public SourceRepository(IOptions<AppSettings> appSettings, ILogger<SourceRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<Source> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
        }

        public async Task<bool> SetOtherIsPriorityFalse(long rid, long resume_id)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update Source set IsPriority='false' where RID!={rid} and Resume_ID={resume_id}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(SourceRepository)}::{nameof(SetOtherIsPriorityFalse)} -- " + ex.Message);
                return false;
            }
           
        }

        public async Task<bool> Insertsource(Source source)
        {
            try
            {
                int i = await _iUnitOfWork.Connection.InsertAsync<Source>(source);
                if (source.IsPriority == true)
                    await SetOtherIsPriorityFalse(i, source.Resume_ID);

                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(SourceRepository)}::{nameof(Insertsource)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> Updatesource(Source source)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.UpdateAsync<Source>(source);
                    if (source.IsPriority == true)
                        await SetOtherIsPriorityFalse(source.Rid, source.Resume_ID);
                    return result;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(SourceRepository)}::{nameof(Insertsource)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> Deletesource(long Rid, string UserName)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update Source set LastUpdatedDate='{DateTime.Now}', UpdateUserName='{UserName}',   IsDeleted='true' where RID={Rid}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(SourceRepository)}::{nameof(Deletesource)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<Webresponse<Source>> GetsourceById(long rid)
        {
            Webresponse<Source> source = new Webresponse<Source>();
            try
            {

                var result = await _iUnitOfWork.Connection.GetAsync<Source>(rid);
                if (result == null)
                {
                    source.message = "No Record found";
                }
                else
                {
                    source.data = result;
                }
                source.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(SourceRepository)}::{nameof(GetsourceById)} -- " + ex.Message);
                source.message = ex.Message;
                source.status = APIStatus.error;
            }
            return source;
        }

        public async Task<Webresponse<IList<Source>>> GetsourceByResumeId(long resumeId)
        {
            Webresponse<IList<Source>> source = new Webresponse<IList<Source>>();
            try
            {
                var result = await _iUnitOfWork.Connection.QueryAsync<Source>($"select * from Source where Resume_ID={resumeId} and IsDeleted='false'");
                if (result == null)
                {
                    source.message = "No Record found";
                }
                else
                {
                    source.data = result.ToList();
                }
                source.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(GetsourceByResumeId)} -- " + ex.Message);
                source.message = ex.Message;
                source.status = APIStatus.error;
            }
            return source;
        }

        public async Task<WebresponsePaging<IList<Source>>> GetAllsource(int pageNumber, int rowCount)
        {
            WebresponsePaging<IList<Source>> source = new WebresponsePaging<IList<Source>>();
            try
            {
                var result = await _igenericRepositoryPaggingDapper.GetAllPagedTotalCountAsync(typeof(Source).GetProperties()[0].Name, pageNumber, rowCount);
                if (result == null && !result.data.Any())
                {
                    source.message = "No Record found";
                }
                else
                {
                    source = result;
                }
                source.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(SourceRepository)}::{nameof(GetAllsource)} -- " + ex.Message);
                source.message = ex.Message;
                source.status = APIStatus.error;
            }
            return source;
        }
    }
}
