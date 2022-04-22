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
    public class AttachmentRepository : IAttachmentRepository
    {
        readonly ILogger<AttachmentRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IGenericRepositoryPaggingDapper<JobSeekerAttachment> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;

        public AttachmentRepository(IOptions<AppSettings> appSettings, ILogger<AttachmentRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<JobSeekerAttachment> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
        }

        public async Task<bool> Insertattachment(JobSeekerAttachment attachment)
        {
            try
            {
                
                int i = await _iUnitOfWork.Connection.InsertAsync<JobSeekerAttachment>(attachment);
                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(AttachmentRepository)}::{nameof(Insertattachment)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> Updateattachment(JobSeekerAttachment attachment)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    return await con.UpdateAsync<JobSeekerAttachment>(attachment);
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(AttachmentRepository)}::{nameof(Updateattachment)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> DeleteAttachment(long Rid)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update JobSeekerAttachment set IsDeleted='true' where RID={Rid}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(AttachmentRepository)}::{nameof(DeleteAttachment)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<Webresponse<JobSeekerAttachment>> GetattachmentById(long rid)
        {
            Webresponse<JobSeekerAttachment> attachment = new Webresponse<JobSeekerAttachment>();
            try
            {

                var result = await _iUnitOfWork.Connection.GetAsync<JobSeekerAttachment>(rid);
                if (result == null)
                {
                    attachment.message = "No Record found";
                }
                else
                {
                    attachment.data = result;
                }
                attachment.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(AttachmentRepository)}::{nameof(GetattachmentById)} -- " + ex.Message);
                attachment.message = ex.Message;
                attachment.status = APIStatus.error;
            }
            return attachment;
        }

        public async Task<Webresponse<IList<JobSeekerAttachment>>> GetattachmentByResumeId(long resumeId)
        {
            Webresponse<IList<JobSeekerAttachment>> attachment = new Webresponse<IList<JobSeekerAttachment>>();
            try
            {
                var result = await _iUnitOfWork.Connection.QueryAsync<JobSeekerAttachment>($"select * from JobSeekerAttachment where Resume_ID={resumeId} and IsDeleted='false'");
                if (result == null)
                {
                    attachment.message = "No Record found";
                }
                else
                {
                    attachment.data = result.ToList();
                }
                attachment.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(AttachmentRepository)}::{nameof(GetattachmentByResumeId)} -- " + ex.Message);
                attachment.message = ex.Message;
                attachment.status = APIStatus.error;
            }
            return attachment;
        }

        /// <summary>
        /// this is jobapplication
        /// </summary>
        /// <param name="resumeId"></param>
        /// <returns></returns>
        public async Task<Webresponse<IList<JobSeekerAttachment>>> GetattachmentFromJobApplicationByResumeId(long resumeId)
        {
            Webresponse<IList<JobSeekerAttachment>> attachment = new Webresponse<IList<JobSeekerAttachment>>();
            try
            {
                var result = await _iUnitOfWork.Connection.QueryAsync<JobSeekerAttachment>($"select * from JobSeekerAttachment where Resume_ID={resumeId} and IsDeleted='false'");
                if (result == null)
                {
                    attachment.message = "No Record found";
                }
                else
                {
                    attachment.data = result.ToList();
                }
                attachment.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(AttachmentRepository)}::{nameof(GetattachmentByResumeId)} -- " + ex.Message);
                attachment.message = ex.Message;
                attachment.status = APIStatus.error;
            }
            return attachment;
        }
    }
}
