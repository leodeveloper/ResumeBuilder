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

    public class CertificationRepository : ICertificationRepository
    {
        readonly ILogger<CertificationRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IGenericRepositoryPaggingDapper<Certification> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;

        public CertificationRepository(IOptions<AppSettings> appSettings, ILogger<CertificationRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<Certification> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
        }

        public async Task<bool> Insertcertification(Certification certification)
        {
            try
            {
                int i = await _iUnitOfWork.Connection.InsertAsync<Certification>(certification);
                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(CertificationRepository)}::{nameof(Insertcertification)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> Updatecertification(Certification certification)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    return await con.UpdateAsync<Certification>(certification);
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(CertificationRepository)}::{nameof(Insertcertification)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> Deletecertification(int Id)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update Certification set IsDeleted='true' where Rid={Id}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(CertificationRepository)}::{nameof(Deletecertification)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<Webresponse<Certification>> GetcertificationById(long rid)
        {
            Webresponse<Certification> certification = new Webresponse<Certification>();
            try
            {

                var result = await _iUnitOfWork.Connection.GetAsync<Certification>(rid);
                if (result == null)
                {
                    certification.message = "No Record found";
                }
                else
                {
                    certification.data = result;
                }
                certification.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(CertificationRepository)}::{nameof(GetcertificationById)} -- " + ex.Message);
                certification.message = ex.Message;
                certification.status = APIStatus.error;
            }
            return certification;
        }

        public async Task<Webresponse<IList<Certification>>> GetcertificationByResumeId(long resumeId)
        {
            Webresponse<IList<Certification>> certification = new Webresponse<IList<Certification>>();
            try
            {
                var result = await _iUnitOfWork.Connection.QueryAsync<Certification>($"select * from Certification where Resume_ID={resumeId} and IsDeleted='false'");
                if (result == null)
                {
                    certification.message = "No Record found";
                }
                else
                {
                    certification.data = result.ToList();
                }
                certification.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(CertificationRepository)}::{nameof(GetcertificationByResumeId)} -- " + ex.Message);
                certification.message = ex.Message;
                certification.status = APIStatus.error;
            }
            return certification;
        }

        public async Task<WebresponsePaging<IList<Certification>>> GetAllcertification(int pageNumber, int rowCount)
        {
            WebresponsePaging<IList<Certification>> certification = new WebresponsePaging<IList<Certification>>();
            try
            {
                var result = await _igenericRepositoryPaggingDapper.GetAllPagedTotalCountAsync(typeof(Certification).GetProperties()[0].Name, pageNumber, rowCount);
                if (result == null && !result.data.Any())
                {
                    certification.message = "No Record found";
                }
                else
                {
                    certification = result;
                }
                certification.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(CertificationRepository)}::{nameof(GetAllcertification)} -- " + ex.Message);
                certification.message = ex.Message;
                certification.status = APIStatus.error;
            }
            return certification;
        }
    }
}
