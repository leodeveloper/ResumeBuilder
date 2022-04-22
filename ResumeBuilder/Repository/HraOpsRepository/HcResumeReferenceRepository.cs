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
    public class HcResumeReferenceRepository : IHcResumeReferenceRepository
    {
        readonly ILogger<HcResumeReferenceRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;

        public HcResumeReferenceRepository(ILogger<HcResumeReferenceRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
        }

        public async Task<WebresponseNoData> Insert(HcResumeReferenceViewModel hcReferenceResumeBankVm, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeReference hc_resume_reference = new HcResumeReference();

                    hc_resume_reference.ResumeId = hcReferenceResumeBankVm.ResumeID;
                    hc_resume_reference.ReferenceTypeId = hcReferenceResumeBankVm.ReferenceTypeId;
                    hc_resume_reference.Name = hcReferenceResumeBankVm.LetterName;
                    hc_resume_reference.ReferenceLetterNumber = hcReferenceResumeBankVm.LetterNumber;
                    hc_resume_reference.ReferenceLetterDate = hcReferenceResumeBankVm.LetterDate;
                    hc_resume_reference.MongoDbID = hcReferenceResumeBankVm.MongoDbID;
                    hc_resume_reference.Verified = 0;
                    hc_resume_reference.AssessmentSheet = 0;
                    hc_resume_reference.Designation = "";
                    hc_resume_reference.Occupation = 0;
                    hc_resume_reference.DesignationId = 0;
                    hc_resume_reference.Status = 0;
                    hc_resume_reference.Notes = "";

                    hc_resume_reference.CreatedDate = DateTime.Now;
                    hc_resume_reference.ReferenceCreatedDate = DateTime.Now;
                    hc_resume_reference.ReferenceCreatedUserId = userid;

                    await _con.InsertAsync<HcResumeReference>(hc_resume_reference);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- Insert successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeReferenceRepository)} :::::: {nameof(Insert)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }
        public async Task<WebresponseNoData> Update(HcResumeReferenceViewModel hcReferenceResumeBankVm, long rid, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeReference hc_resume_reference = await _con.GetAsync<HcResumeReference>(rid);

                    hc_resume_reference.ResumeId = hcReferenceResumeBankVm.ResumeID;
                    hc_resume_reference.ReferenceTypeId = hcReferenceResumeBankVm.ReferenceTypeId;
                    hc_resume_reference.Name = hcReferenceResumeBankVm.LetterName;
                    hc_resume_reference.ReferenceLetterNumber = hcReferenceResumeBankVm.LetterNumber;
                    hc_resume_reference.ReferenceLetterDate = hcReferenceResumeBankVm.LetterDate;
                    hc_resume_reference.MongoDbID = hcReferenceResumeBankVm.MongoDbID;
                    hc_resume_reference.Verified = 0;
                    hc_resume_reference.AssessmentSheet = 0;
                    hc_resume_reference.Designation = "";
                    hc_resume_reference.Occupation = 0;
                    hc_resume_reference.DesignationId = 0;
                    hc_resume_reference.Status = 0;
                    hc_resume_reference.Notes = "";

                    hc_resume_reference.ModifiedDate = DateTime.Now;
                    hc_resume_reference.ModifiedUserid = userid;

                    await _con.UpdateAsync<HcResumeReference>(hc_resume_reference);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- update successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeReferenceRepository)} :::::: {nameof(Update)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<Webresponse<HcResumeReferenceViewModel_Get>> Delete(long rid)
        {
            Webresponse<HcResumeReferenceViewModel_Get> webresponse = new Webresponse<HcResumeReferenceViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    await _con.QueryAsync($"update HC_RESUME_REFERENCE set Is_Deleted='true' where RID={rid}");
                    webresponse.status = APIStatus.success;
                    webresponse.message = "Record deleted";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeReferenceRepository)} :::::: {nameof(Delete)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<HcResumeReferenceViewModel_Get>> GetById(long rid)
        {
            Webresponse<HcResumeReferenceViewModel_Get> webresponse = new Webresponse<HcResumeReferenceViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeReference hc_resume_reference = await _con.QueryFirstOrDefaultAsync<HcResumeReference>($"select * from HC_RESUME_REFERENCE where RID={rid} and Is_Deleted = 'false'");
                    if (hc_resume_reference != null)
                    {
                        HcResumeReferenceViewModel_Get hcReferenceResumeBankVm = Convert_Model_to_ViewModel(hc_resume_reference);
                        webresponse.data = hcReferenceResumeBankVm;
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
                    _ilogger.LogError($"{nameof(HcResumeReferenceRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<HcResumeReferenceViewModel_Get>>> GetByResumeId(long resumeId)
        {
            Webresponse<IList<HcResumeReferenceViewModel_Get>> webresponse = new Webresponse<IList<HcResumeReferenceViewModel_Get>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    string sqlStr = "  SELECT ref.RID,ref.ResumeID,ref.Name as LetterName,ref.ReferenceCreatedUserID,ref.ReferenceCreatedDate,ref.CreatedDate,ref.ReferenceTypeID as ReferenceTypeId,reftype.Title as ReferenceType,ref.ReferenceLetterNumber as LetterNumber ";
                    sqlStr += "  ,ref.ReferenceLetterDate as LetterDate,ref.Is_Deleted,ref.MongoDbID,ref.ModifiedDate,ref.ModifiedUserid";
                    sqlStr += "  FROM HC_RESUME_REFERENCE ref";
                    sqlStr += "  left outer join HCM_RESUME_REFERENCE_TYPE reftype on reftype.EngKeyID = ref.ReferenceTypeID and LanguageType = 0";
                    sqlStr += $"  where ref.Resumeid = {resumeId} and ref.Is_Deleted = 'false'";

                    IEnumerable<HcResumeReferenceViewModel_Get> hc_resume_references = await _con.QueryAsync<HcResumeReferenceViewModel_Get>(sqlStr);

                    if (hc_resume_references != null)
                    {

                        webresponse.data = hc_resume_references.ToList();
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
                    _ilogger.LogError($"{nameof(HcResumeReferenceRepository)} :::::: {nameof(GetByResumeId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        private HcResumeReferenceViewModel_Get Convert_Model_to_ViewModel(HcResumeReference _hc_resume_bank)
        {
            HcResumeReferenceViewModel_Get hcReferenceResumeBankVm = new HcResumeReferenceViewModel_Get();
            try
            {
                hcReferenceResumeBankVm.Rid = _hc_resume_bank.Rid;
                hcReferenceResumeBankVm.ResumeID = _hc_resume_bank.ResumeId;
                hcReferenceResumeBankVm.ReferenceTypeId = _hc_resume_bank.ReferenceTypeId;
                hcReferenceResumeBankVm.LetterName = _hc_resume_bank.Name;
                hcReferenceResumeBankVm.LetterNumber = _hc_resume_bank.ReferenceLetterNumber;
                hcReferenceResumeBankVm.MongoDbID = _hc_resume_bank.MongoDbID;
                hcReferenceResumeBankVm.LetterDate = _hc_resume_bank.ReferenceLetterDate;
                hcReferenceResumeBankVm.ReferenceCreatedDate = _hc_resume_bank.ReferenceCreatedDate;
                hcReferenceResumeBankVm.ReferenceCreatedUserId = _hc_resume_bank.ReferenceCreatedUserId;
                hcReferenceResumeBankVm.ModifiedDate = _hc_resume_bank.ModifiedDate;
                hcReferenceResumeBankVm.ModifiedUserid = _hc_resume_bank.ModifiedUserid;
            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumeReferenceRepository)} :::::: {nameof(Convert_Model_to_ViewModel)} :::: {ex.Message}");
            }
            return hcReferenceResumeBankVm;
        }
    }
}
