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
    public class ReferenceRepository : IReferenceRepository
    {
        readonly ILogger<ReferenceRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IGenericRepositoryPaggingDapper<Reference> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;

        public ReferenceRepository(IOptions<AppSettings> appSettings, ILogger<ReferenceRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<Reference> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
        }

        public async Task<bool> Insertreference(Reference reference)
        {
            try
            {
                int i = await _iUnitOfWork.Connection.InsertAsync<Reference>(reference);
                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ReferenceRepository)}::{nameof(Insertreference)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> Updatereference(Reference reference)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    return await con.UpdateAsync<Reference>(reference);
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ReferenceRepository)}::{nameof(Insertreference)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> Deletereference(long Rid)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update Reference set IsDeleted='true' where RID={Rid}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ReferenceRepository)}::{nameof(Deletereference)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<Webresponse<IList<Reference>>> GetreferenceByResumeId(long resumeId)
        {
            Webresponse<IList<Reference>> reference = new Webresponse<IList<Reference>>();
            try
            {
                var result = await _iUnitOfWork.Connection.QueryAsync<Reference>($"select * from Reference where Resume_ID={resumeId} and IsDeleted='false'");
                if (result == null)
                {
                    reference.message = "No Record found";
                }
                else
                {
                    reference.data = result.ToList();
                }
                reference.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(GetreferenceByResumeId)} -- " + ex.Message);
                reference.message = ex.Message;
                reference.status = APIStatus.error;
            }
            return reference;
        }

        public async Task<Webresponse<Reference>> GetreferenceById(long rid)
        {
            Webresponse<Reference> reference = new Webresponse<Reference>();
            try
            {

                var result = await _iUnitOfWork.Connection.GetAsync<Reference>(rid);
                if (result == null)
                {
                    reference.message = "No Record found";
                }
                else
                {
                    reference.data = result;
                }
                reference.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ReferenceRepository)}::{nameof(GetreferenceById)} -- " + ex.Message);
                reference.message = ex.Message;
                reference.status = APIStatus.error;
            }
            return reference;
        }

        public async Task<WebresponsePaging<IList<Reference>>> GetAllreference(int pageNumber, int rowCount)
        {
            WebresponsePaging<IList<Reference>> reference = new WebresponsePaging<IList<Reference>>();
            try
            {
                var result = await _igenericRepositoryPaggingDapper.GetAllPagedTotalCountAsync(typeof(Reference).GetProperties()[0].Name, pageNumber, rowCount);
                if (result == null && !result.data.Any())
                {
                    reference.message = "No Record found";
                }
                else
                {
                    reference = result;
                }
                reference.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ReferenceRepository)}::{nameof(GetAllreference)} -- " + ex.Message);
                reference.message = ex.Message;
                reference.status = APIStatus.error;
            }
            return reference;
        }
    }
}
