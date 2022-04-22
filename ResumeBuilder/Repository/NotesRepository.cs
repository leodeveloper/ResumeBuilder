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


    public class NotesRepository : INotesRepository
    {
        readonly ILogger<NotesRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IGenericRepositoryPaggingDapper<Notes> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;

        public NotesRepository(IOptions<AppSettings> appSettings, ILogger<NotesRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<Notes> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
        }

        public async Task<bool> Insertnotes(Notes notes)
        {
            try
            {
                int i = await _iUnitOfWork.Connection.InsertAsync<Notes>(notes);
                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(NotesRepository)}::{nameof(Insertnotes)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> Updatenotes(Notes notes)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    return await con.UpdateAsync<Notes>(notes);
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(NotesRepository)}::{nameof(Insertnotes)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> Deletenotes(long Rid)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update Notes set IsDeleted='true' where RID={Rid}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(NotesRepository)}::{nameof(Deletenotes)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<Webresponse<Notes>> GetnotesById(long rid)
        {
            Webresponse<Notes> notes = new Webresponse<Notes>();
            try
            {

                var result = await _iUnitOfWork.Connection.GetAsync<Notes>(rid);
                if (result == null)
                {
                    notes.message = "No Record found";
                }
                else
                {
                    notes.data = result;
                }
                notes.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(NotesRepository)}::{nameof(GetnotesById)} -- " + ex.Message);
                notes.message = ex.Message;
                notes.status = APIStatus.error;
            }
            return notes;
        }

        public async Task<Webresponse<IList<Notes>>> GetnotesByResumeId(long resumeId)
        {
            Webresponse<IList<Notes>> notes = new Webresponse<IList<Notes>>();
            try
            {
                var result = await _iUnitOfWork.Connection.QueryAsync<Notes>($"select * from Notes where Resume_ID={resumeId} and IsDeleted='false'");
                if (result == null)
                {
                    notes.message = "No Record found";
                }
                else
                {
                    notes.data = result.ToList();
                }
                notes.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(GetnotesByResumeId)} -- " + ex.Message);
                notes.message = ex.Message;
                notes.status = APIStatus.error;
            }
            return notes;
        }

        public async Task<WebresponsePaging<IList<Notes>>> GetAllnotes(int pageNumber, int rowCount)
        {
            WebresponsePaging<IList<Notes>> notes = new WebresponsePaging<IList<Notes>>();
            try
            {
                var result = await _igenericRepositoryPaggingDapper.GetAllPagedTotalCountAsync(typeof(Notes).GetProperties()[0].Name, pageNumber, rowCount);
                if (result == null && !result.data.Any())
                {
                    notes.message = "No Record found";
                }
                else
                {
                    notes = result;
                }
                notes.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(NotesRepository)}::{nameof(GetAllnotes)} -- " + ex.Message);
                notes.message = ex.Message;
                notes.status = APIStatus.error;
            }
            return notes;
        }
    }
}
