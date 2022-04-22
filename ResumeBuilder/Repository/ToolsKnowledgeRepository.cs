using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Dto;
using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository
{
    public class ToolsKnowledgeRepository : IToolsKnowledgeRepository
    {
        readonly ILogger<ToolsKnowledgeRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IGenericRepositoryPaggingDapper<ToolsKnowledge> _igenericRepositoryPaggingDapper;
        readonly IUnitOfWork _iUnitOfWork;
        private readonly IOptions<LookUpApiUrl> _appSettingsAPIUrls;
        private IlookupRespository _ilookupRespository;

        public ToolsKnowledgeRepository(IlookupRespository ilookupRespository, IOptions<LookUpApiUrl> appSettingsAPIUrls, IOptions<AppSettings> appSettings, ILogger<ToolsKnowledgeRepository> ilogger, IUnitOfWork iUnitOfWork, IGenericRepositoryPaggingDapper<ToolsKnowledge> igenericRepositoryPaggingDapper)
        {
            _appSettings = appSettings;
            _ilogger = ilogger;
            _iUnitOfWork = iUnitOfWork;
            _igenericRepositoryPaggingDapper = igenericRepositoryPaggingDapper;
            _appSettingsAPIUrls = appSettingsAPIUrls;
            _ilookupRespository = ilookupRespository;
        }

        public async Task<bool> InsertDeletetoolsKnowledge(ToolsKnowledgeViewModel toolsKnowledgeVM)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {                    
                    var result = await con.QueryAsync<ToolsKnowledge>($"select * from ToolsKnowledge where Resume_ID={toolsKnowledgeVM.Resume_ID}");
                    if(result.Any())
                    {
                        var InserttoolKnowledge = toolsKnowledgeVM.ToolsKnowledgeId.Where(z => !result.Select(i => i.ToolsKnowledgeId).Contains(z));
                        foreach (int toolsKnowledge in InserttoolKnowledge)
                        {
                            int i = await con.InsertAsync<ToolsKnowledge>(new ToolsKnowledge { Resume_ID = toolsKnowledgeVM.Resume_ID, ToolsKnowledgeId = toolsKnowledge });
                        }
                        var deletetoolKnowledge = result.Where(z => !toolsKnowledgeVM.ToolsKnowledgeId.Contains(z.ToolsKnowledgeId));
                        foreach (ToolsKnowledge toolsKnowledge in deletetoolKnowledge)
                        {
                            bool i = await con.DeleteAsync<ToolsKnowledge>(toolsKnowledge);
                        }
                    }
                    else
                    {
                        foreach(int toolsKnowledge in toolsKnowledgeVM.ToolsKnowledgeId)
                        {
                            int i = await con.InsertAsync<ToolsKnowledge>(new ToolsKnowledge { Resume_ID = toolsKnowledgeVM.Resume_ID, ToolsKnowledgeId = toolsKnowledge });
                        }
                        
                    }
                    return true;
                }  
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ToolsKnowledgeRepository)}::{nameof(InsertDeletetoolsKnowledge)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<bool> InserttoolsKnowledge(ToolsKnowledge toolsKnowledge)
        {
            try
            {
                int i = await _iUnitOfWork.Connection.InsertAsync<ToolsKnowledge>(toolsKnowledge);
                return true;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ToolsKnowledgeRepository)}::{nameof(InserttoolsKnowledge)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> UpdatetoolsKnowledge(ToolsKnowledge toolsKnowledge)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    return await con.UpdateAsync<ToolsKnowledge>(toolsKnowledge);
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ToolsKnowledgeRepository)}::{nameof(InserttoolsKnowledge)} -- " + ex.Message);
                return false;
            }

        }

        public async Task<bool> DeletetoolsKnowledge(long rid)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update ToolsKnowledge set IsDeleted='true' where RID={rid}");
                    return true;
                }

            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ToolsKnowledgeRepository)}::{nameof(DeletetoolsKnowledge)} -- " + ex.Message);
                return false;
            }
        }

        public async Task<Webresponse<ToolsKnowledge>> GettoolsKnowledgeById(long rid)
        {
            Webresponse<ToolsKnowledge> toolsKnowledge = new Webresponse<ToolsKnowledge>();
            try
            {

                var result = await _iUnitOfWork.Connection.GetAsync<ToolsKnowledge>(rid);
                if (result == null)
                {
                    toolsKnowledge.message = "No Record found";
                }
                else
                {
                    toolsKnowledge.data = result;
                }
                toolsKnowledge.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ToolsKnowledgeRepository)}::{nameof(GettoolsKnowledgeById)} -- " + ex.Message);
                toolsKnowledge.message = ex.Message;
                toolsKnowledge.status = APIStatus.error;
            }
            return toolsKnowledge;
        }

        public async Task<Webresponse<IList<ToolsKnowledge>>> GettoolsKnowledgeByResumeId(long resumeId)
        {
            Webresponse<IList<ToolsKnowledge>> toolsKnowledge = new Webresponse<IList<ToolsKnowledge>>();
            try
            {
                var result = await _iUnitOfWork.Connection.QueryAsync<ToolsKnowledge>($"select * from ToolsKnowledge where Resume_ID={resumeId} and IsDeleted='false'");
                if (result == null)
                {
                    toolsKnowledge.message = "No Record found";
                }
                else
                {
                    toolsKnowledge.data = result.ToList();
                }
                toolsKnowledge.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(EducationRepository)}::{nameof(GettoolsKnowledgeByResumeId)} -- " + ex.Message);
                toolsKnowledge.message = ex.Message;
                toolsKnowledge.status = APIStatus.error;
            }
            return toolsKnowledge;
        }

        public async Task<Webresponse<IList<ToolsKnowledgeDto>>> GetToolsKnowledgeByResumeIdPreview(long resumeId)
        {
            Webresponse<IList<ToolsKnowledgeDto>> toolsKnowledgeDto = new Webresponse<IList<ToolsKnowledgeDto>> { data = new List<ToolsKnowledgeDto>() };
            try
            {
                var toolsKnowledges = await GettoolsKnowledgeByResumeId(resumeId);
                int[] toolsKnowledgeIds = toolsKnowledges.data.Select(z => z.ToolsKnowledgeId).ToArray();
                ToolsKnowledgeDto _toolsKnowledgeDto = new ToolsKnowledgeDto();
                IList<ToolsKnowledgeLookup> toolsKnowledgeLookups = await _ilookupRespository.GetHttpClient<IList<ToolsKnowledgeLookup>>($"{_appSettingsAPIUrls.Value.GetToolsKnowledgeLookupUrl}");
                toolsKnowledgeLookups = toolsKnowledgeLookups.Where(z => toolsKnowledgeIds.Contains(z.Id)).ToList();
                toolsKnowledgeDto.data = toolsKnowledgeLookups.Select(i => new ToolsKnowledgeDto { Id = i.Id, ArTitle = i.ArTitle, EnTitle = i.EnTitle }).ToList();
                toolsKnowledgeDto.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ToolsKnowledgeRepository)}::{nameof(GettoolsKnowledgeByResumeId)} -- " + ex.Message);
                toolsKnowledgeDto.message = ex.Message;
                toolsKnowledgeDto.status = APIStatus.error;
            }
            return toolsKnowledgeDto;
        }

        public async Task<WebresponsePaging<IList<ToolsKnowledge>>> GetAlltoolsKnowledge(int pageNumber, int rowCount)
        {
            WebresponsePaging<IList<ToolsKnowledge>> toolsKnowledge = new WebresponsePaging<IList<ToolsKnowledge>>();
            try
            {
                var result = await _igenericRepositoryPaggingDapper.GetAllPagedTotalCountAsync(typeof(ToolsKnowledge).GetProperties()[0].Name, pageNumber, rowCount);
                if (result == null && !result.data.Any())
                {
                    toolsKnowledge.message = "No Record found";
                }
                else
                {
                    toolsKnowledge = result;
                }
                toolsKnowledge.status = APIStatus.success;
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(ToolsKnowledgeRepository)}::{nameof(GetAlltoolsKnowledge)} -- " + ex.Message);
                toolsKnowledge.message = ex.Message;
                toolsKnowledge.status = APIStatus.error;
            }
            return toolsKnowledge;
        }
    }
}
