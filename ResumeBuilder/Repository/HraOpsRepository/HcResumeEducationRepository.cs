using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Dto.HraOpsDto;
using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public class HcResumeEducationRepository : IHcResumeEducationRepository
    {
        readonly ILogger<HcResumeEducationRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;
        readonly ITypeSenceApiRepository _iTypeSenceApiRepository;

        public HcResumeEducationRepository(ITypeSenceApiRepository iTypeSenceApiRepository,ILogger<HcResumeEducationRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
            _iTypeSenceApiRepository = iTypeSenceApiRepository;
        }

        public async Task<WebresponseNoData> Insert(HcResumeEducationViewModel hcEducationResumeBankVm, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeEducation hc_resume_education = new HcResumeEducation();
                    hc_resume_education.StartDate = hcEducationResumeBankVm.StartDate;
                    hc_resume_education.EndDate = hcEducationResumeBankVm.EndDate;
                    hc_resume_education.IsEducationAbroad = hcEducationResumeBankVm.IsEducationAbroad;
                    hc_resume_education.EducationAbroadCountryId = hcEducationResumeBankVm.EducationAbroadCountryId;

                    hc_resume_education.EducationId = hcEducationResumeBankVm.EducationTypeId;
                    hc_resume_education.ResumeId = hcEducationResumeBankVm.ResumeId;
                    hc_resume_education.Institute = "";
                    hc_resume_education.Year = null;
                    hc_resume_education.GradeId = hcEducationResumeBankVm.GradeId;
                    hc_resume_education.Grade = hcEducationResumeBankVm.Score;
                    hc_resume_education.SpecializationId = hcEducationResumeBankVm.MajorId;
                    hc_resume_education.SerialNo = 0;
                    hc_resume_education.InstituteGradeId = 0;
                    hc_resume_education.Type = 0;
                    hc_resume_education.UniversityId = hcEducationResumeBankVm.UniversityId;
                    hc_resume_education.UniversityTypeId = 0;

                    hc_resume_education.Specialization2 = 0;
                    hc_resume_education.Month = 0;
                    hc_resume_education.Notes = "";
                    hc_resume_education.Organization = "";

                    hc_resume_education.Department = "";
                    hc_resume_education.Duration = 0;
                    hc_resume_education.MeasurementUnit = 0;
                    hc_resume_education.MarksObtainedUnit = 0;
                    hc_resume_education.InstituteId = 0;
                    hc_resume_education.Certification = 0;

                    hc_resume_education.EducationGroupId = hcEducationResumeBankVm.GroupId;
                    hc_resume_education.DeviationReason = "";
                    hc_resume_education.IsHighestEducation = hcEducationResumeBankVm.IsHighestEducation;

                    hc_resume_education.UniversityText = "";
                    hc_resume_education.LocationText = "";
                    hc_resume_education.Createddate = DateTime.Now;
                    hc_resume_education.Createduserid = userid;
                    hc_resume_education.Cddate = DateTime.Now;
                    await _con.InsertAsync<HcResumeEducation>(hc_resume_education);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- Insert successfully";

                    if (hcEducationResumeBankVm.IsEducationAbroad)
                        getLastInsertedId(hcEducationResumeBankVm.ResumeId);

                    //Update type sence index
                    _iTypeSenceApiRepository.UpdateTypeSenceIndex(hcEducationResumeBankVm.ResumeId);

                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeEducationRepository)} :::::: {nameof(Insert)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }
        public async Task<WebresponseNoData> Update(HcResumeEducationViewModel hcEducationResumeBankVm, long rid, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeEducation hc_resume_education = await _con.GetAsync<HcResumeEducation>(rid);
                    hc_resume_education.StartDate = hcEducationResumeBankVm.StartDate;
                    hc_resume_education.EndDate = hcEducationResumeBankVm.EndDate;
                    hc_resume_education.IsEducationAbroad = hcEducationResumeBankVm.IsEducationAbroad;
                    hc_resume_education.EducationAbroadCountryId = hcEducationResumeBankVm.EducationAbroadCountryId;

                    hc_resume_education.EducationId = hcEducationResumeBankVm.EducationTypeId;
                    hc_resume_education.ResumeId = hcEducationResumeBankVm.ResumeId;
                    hc_resume_education.GradeId = hcEducationResumeBankVm.GradeId;
                    hc_resume_education.Grade = hcEducationResumeBankVm.Score;
                    hc_resume_education.SpecializationId = hcEducationResumeBankVm.MajorId;
                    hc_resume_education.UniversityId = hcEducationResumeBankVm.UniversityId;

                    hc_resume_education.EducationGroupId = hcEducationResumeBankVm.GroupId;

                    hc_resume_education.IsHighestEducation = hcEducationResumeBankVm.IsHighestEducation;

                    hc_resume_education.ModifiedDate = DateTime.Now;
                    hc_resume_education.ModifiedUser = userid.ToString();

                    await _con.UpdateAsync<HcResumeEducation>(hc_resume_education);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- update successfully";

                    if (hcEducationResumeBankVm.IsHighestEducation)
                        SetOtherThisIsMyHigestEduFalse(rid, hcEducationResumeBankVm.ResumeId);

                    //Update type sence index
                    _iTypeSenceApiRepository.UpdateTypeSenceIndex(hcEducationResumeBankVm.ResumeId);


                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeEducationRepository)} :::::: {nameof(Update)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<Webresponse<HcResumeEducationViewModel_Get>> Delete(long rid)
        {
            Webresponse<HcResumeEducationViewModel_Get> webresponse = new Webresponse<HcResumeEducationViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    await _con.QueryAsync($"update HC_RESUME_EDUCATION set Is_Deleted='true' where RID={rid}");
                    HcResumeEducation hC_RESUME_BANK = await _con.GetAsync<HcResumeEducation>(rid);
                    //Update type sence index
                    _iTypeSenceApiRepository.UpdateTypeSenceIndex(hC_RESUME_BANK.ResumeId);
                    webresponse.status = APIStatus.success;
                    webresponse.message = "Record deleted";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeEducationRepository)} :::::: {nameof(Delete)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<HcResumeEducationViewModel_Get>> GetById(long rid)
        {
            Webresponse<HcResumeEducationViewModel_Get> webresponse = new Webresponse<HcResumeEducationViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeEducation hc_resume_education = await _con.QueryFirstOrDefaultAsync<HcResumeEducation>($"select * from HC_RESUME_EDUCATION where RID={rid} and Is_Deleted='false'");
                    if (hc_resume_education != null)
                    {
                        HcResumeEducationViewModel_Get hcEducationResumeBankVm = Convert_Model_to_ViewModel(hc_resume_education);
                        webresponse.data = hcEducationResumeBankVm;
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
                    _ilogger.LogError($"{nameof(HcResumeEducationRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<HcResumeEducationViewModel_Get>>> GetByResumeId(long resumeId)
        {
            Webresponse<IList<HcResumeEducationViewModel_Get>> webresponse = new Webresponse<IList<HcResumeEducationViewModel_Get>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    string sqlStr = "select edu.*,gra.Title as GradeTitle,grpen.GroupTitle as GroupTitle,typeen.EducationType as EducationTypeTitle,spen.SpecializationTitle as MajorTitle, ";
                    sqlStr += "Country.Title as CountryName, Univeristy.Title as UniversityName from HC_RESUME_EDUCATION edu ";
                    sqlStr += "left outer join HCM_EDUCATION_GROUP grpen on edu.educationgroupid = grpen.EngKeyID and grpen.LanguageType = 0  ";
                    sqlStr += "left outer join HCM_EDUCATION_TYPES typeen on edu.educationId = typeen.EngKeyID and typeen.LanguageType = 0 ";
                    sqlStr += "left outer join HCM_EDUCATION_SPECIALIZATION_TYPES spen on edu.specializationid = spen.EngKeyID and spen.LanguageType = 0 ";
                    sqlStr += "left outer join HCM_GRADE gra on edu.GradeId = gra.EngKeyID and gra.LangugaeType = 0 ";
                    sqlStr += "left outer join HCM_NATIONALITY Country on edu.EducationAbroadCountryId = Country.EngKeyID and Country.LanguageType = 0 ";
                    sqlStr += "left outer join HCM_UNIVERSITY Univeristy on edu.UniversityID = Univeristy.EngKeyID and Univeristy.LanguageType = 0 ";
                    sqlStr += $"where ResumeID={resumeId} and Is_Deleted='false' ";
                    IEnumerable<HcResumeEducationViewModel_Get> hc_resume_educations = await _con.QueryAsync<HcResumeEducationViewModel_Get>(sqlStr);
                   
                    if (hc_resume_educations != null)
                    {
                       
                        webresponse.data = hc_resume_educations.ToList();
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
                    _ilogger.LogError($"{nameof(HcResumeEducationRepository)} :::::: {nameof(GetByResumeId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

       

        private HcResumeEducationViewModel_Get Convert_Model_to_ViewModel(HcResumeEducation _hc_resume_bank)
        {
            HcResumeEducationViewModel_Get hcEducationResumeBankVm = new HcResumeEducationViewModel_Get();
            try
            {
                hcEducationResumeBankVm.Rid = _hc_resume_bank.Rid;
                hcEducationResumeBankVm.StartDate = _hc_resume_bank.StartDate;
                hcEducationResumeBankVm.EndDate = _hc_resume_bank.EndDate;
                hcEducationResumeBankVm.IsEducationAbroad = _hc_resume_bank.IsEducationAbroad;
                hcEducationResumeBankVm.EducationAbroadCountryId = _hc_resume_bank.EducationAbroadCountryId;

                hcEducationResumeBankVm.EducationTypeId = _hc_resume_bank.EducationId;
                hcEducationResumeBankVm.ResumeId = _hc_resume_bank.ResumeId;
                hcEducationResumeBankVm.GradeId = _hc_resume_bank.GradeId;
                hcEducationResumeBankVm.Score = _hc_resume_bank.Grade;
                hcEducationResumeBankVm.MajorId = _hc_resume_bank.SpecializationId;
                hcEducationResumeBankVm.UniversityId = _hc_resume_bank.UniversityId;

                hcEducationResumeBankVm.GroupId = _hc_resume_bank.EducationGroupId;

                hcEducationResumeBankVm.IsHighestEducation = _hc_resume_bank.IsHighestEducation;

                hcEducationResumeBankVm.CreatedDate = _hc_resume_bank.Createddate;
                hcEducationResumeBankVm.CreatedUserId = _hc_resume_bank.Createduserid;
                hcEducationResumeBankVm.ModifiedDate = _hc_resume_bank.ModifiedDate;
                hcEducationResumeBankVm.ModifiedUser = _hc_resume_bank.ModifiedUser;
            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumeEducationRepository)} :::::: {nameof(Convert_Model_to_ViewModel)} :::: {ex.Message}");
            }

            return hcEducationResumeBankVm;
        }

        private async void getLastInsertedId(long resumeId)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync<long>($"select top 1 rid from HC_RESUME_EDUCATION where ResumeID ={resumeId}  order by Createddate desc ");
                    SetOtherThisIsMyHigestEduFalse(result.FirstOrDefault(), resumeId);
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(HcResumeEducationRepository)}::{nameof(getLastInsertedId)} -- " + ex.Message);
            }
        }

        public async void SetOtherThisIsMyHigestEduFalse(long rid, long resume_id)
        {
            try
            {
                using (var con = _iUnitOfWork.Connection)
                {
                    var result = await con.QueryAsync($"update HC_RESUME_EDUCATION set IsHighestEducation='false' where RID!={rid} and ResumeID={resume_id}");
                }
            }
            catch (Exception ex)
            {
                _ilogger.LogError(ex, $"{nameof(HcResumeEducationRepository)}::{nameof(SetOtherThisIsMyHigestEduFalse)} -- " + ex.Message);
            }
        }
    }
}
