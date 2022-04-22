using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public class HcResumeCoverLetterRepository : IHcResumeCoverLetterRepository
    {
        readonly ILogger<HcResumeCoverLetterRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;
        readonly ITypeSenceApiRepository _iTypeSenceApiRepository;

        public HcResumeCoverLetterRepository(ILogger<HcResumeCoverLetterRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork, ITypeSenceApiRepository iTypeSenceApiRepository)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
            _iTypeSenceApiRepository = iTypeSenceApiRepository;
        }

        public async Task<WebresponseNoData> InsertUpdate(HcResumeCoverLetterViewModel hcCoverLetterResumeBankVm, long rid, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeCoverLetter hc_resume_coverletter = await _con.QueryFirstOrDefaultAsync<HcResumeCoverLetter>($"select * from HC_RESUME_CoverLetter where ResumeID={rid}");
                    if (hc_resume_coverletter != null)
                    {
                        hc_resume_coverletter.ResumeId = hcCoverLetterResumeBankVm.ResumeID;
                        hc_resume_coverletter.Name = hcCoverLetterResumeBankVm.ConverLetterName;
                        hc_resume_coverletter.Content = hcCoverLetterResumeBankVm.Content;
                        hc_resume_coverletter.UpdatedDate = DateTime.Now;
                        hc_resume_coverletter.UpdatedUserId = userid;

                        await _con.UpdateAsync<HcResumeCoverLetter>(hc_resume_coverletter);
                        webresponseNoData.message += " -- update successfully";
                    }
                    else
                    {
                        hc_resume_coverletter = new HcResumeCoverLetter();
                        hc_resume_coverletter.ResumeId = hcCoverLetterResumeBankVm.ResumeID;
                        hc_resume_coverletter.Name = hcCoverLetterResumeBankVm.ConverLetterName;
                        hc_resume_coverletter.Content = hcCoverLetterResumeBankVm.Content;
                        hc_resume_coverletter.Postdate = DateTime.Now;
                        hc_resume_coverletter.CreatedDate = DateTime.Now;
                        hc_resume_coverletter.CreatedUserId = userid;

                        await _con.InsertAsync<HcResumeCoverLetter>(hc_resume_coverletter);
                      
                        webresponseNoData.message += " -- insert successfully";
                    }
                    await _con.ExecuteAsync($"update HC_RESUME_BANK set ExtAssociateJD='{hcCoverLetterResumeBankVm.Content}' where rid= {hcCoverLetterResumeBankVm.ResumeID}");
                    _iTypeSenceApiRepository.UpdateTypeSenceIndex(rid);
                    webresponseNoData.status = APIStatus.success;

                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeCoverLetterRepository)} :::::: {nameof(InsertUpdate)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }



        public async Task<Webresponse<HcResumeCoverLetterViewModel_Get>> GetById(long rid)
        {
            Webresponse<HcResumeCoverLetterViewModel_Get> webresponse = new Webresponse<HcResumeCoverLetterViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeCoverLetter hc_resume_coverletter = await _con.QueryFirstOrDefaultAsync<HcResumeCoverLetter>($"select * from HC_RESUME_CoverLetter where ResumeID={rid}");
                    if (hc_resume_coverletter != null)
                    {
                        HcResumeCoverLetterViewModel_Get hcCoverLetterResumeBankVm = Convert_Model_to_ViewModel(hc_resume_coverletter);
                        webresponse.data = hcCoverLetterResumeBankVm;
                        webresponse.status = APIStatus.success;
                        webresponse.message = "Record found";
                    }
                    else
                    {
                        webresponse.status = APIStatus.NotFound;
                        webresponse.message = "No Record found";
                    }
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeCoverLetterRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<HcResumeCoverLetterViewModel_Get>> GetByResumeId(long resumeId)
        {
            Webresponse<HcResumeCoverLetterViewModel_Get> webresponse = new Webresponse<HcResumeCoverLetterViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    string sqlStr = "  SELECT [RID] as Rid,[ResumeID],[Name] as ConverLetterName,[Content],[postdate],[CreatedDate],[CreatedUserId],[UpdatedDate]";
                    sqlStr += "  ,[UpdatedUserId] FROM HC_RESUME_COVERLETTER";
                    sqlStr += $"  where Resumeid = {resumeId} ";

                    HcResumeCoverLetterViewModel_Get hc_resume_coverletters = await _con.QueryFirstOrDefaultAsync<HcResumeCoverLetterViewModel_Get>(sqlStr);

                    if (hc_resume_coverletters != null)
                    {

                        webresponse.data = hc_resume_coverletters;
                        webresponse.status = APIStatus.success;
                        webresponse.message = "Record found";
                    }
                    else
                    {
                        webresponse.status = APIStatus.NotFound;
                        webresponse.message = "No Record found";
                    }


                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeCoverLetterRepository)} :::::: {nameof(GetByResumeId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        private HcResumeCoverLetterViewModel_Get Convert_Model_to_ViewModel(HcResumeCoverLetter _hc_resume_bank)
        {
            HcResumeCoverLetterViewModel_Get hcCoverLetterResumeBankVm = new HcResumeCoverLetterViewModel_Get();
            try
            {
                hcCoverLetterResumeBankVm.Rid = _hc_resume_bank.Rid;
                hcCoverLetterResumeBankVm.ResumeID = _hc_resume_bank.ResumeId;
                hcCoverLetterResumeBankVm.ConverLetterName = _hc_resume_bank.Name;
                hcCoverLetterResumeBankVm.Content = _hc_resume_bank.Content;
                hcCoverLetterResumeBankVm.PostDate = _hc_resume_bank.Postdate;
                hcCoverLetterResumeBankVm.CreatedDate = _hc_resume_bank.CreatedDate;
                hcCoverLetterResumeBankVm.CreatedUserId = _hc_resume_bank.CreatedUserId;
                hcCoverLetterResumeBankVm.UpdatedDate = _hc_resume_bank.UpdatedDate;
                hcCoverLetterResumeBankVm.UpdatedUserId = _hc_resume_bank.UpdatedUserId;
            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumeCoverLetterRepository)} :::::: {nameof(Convert_Model_to_ViewModel)} :::: {ex.Message}");
            }

            return hcCoverLetterResumeBankVm;
        }
    }
}
