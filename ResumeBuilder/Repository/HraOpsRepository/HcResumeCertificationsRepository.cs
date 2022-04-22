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
    public class HcResumeCertificationsRepository : IHcResumeCertificationsRepository
    {
        readonly ILogger<HcResumeCertificationsRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;

        public HcResumeCertificationsRepository(ILogger<HcResumeCertificationsRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
        }

        public async Task<WebresponseNoData> Insert(HcResumeCertificationsViewModel hcCertificationsResumeBankVm, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeCertifications hc_resume_education = new HcResumeCertifications();

                    hc_resume_education.Resumeid = hcCertificationsResumeBankVm.ResumeId;
                    hc_resume_education.Title = hcCertificationsResumeBankVm.Title;
                    hc_resume_education.Typeid = hcCertificationsResumeBankVm.CertificationTypeId;
                    hc_resume_education.Frommonth = hcCertificationsResumeBankVm.FromMonth;
                    hc_resume_education.Fromyear = hcCertificationsResumeBankVm.FromYear;

                    hc_resume_education.Tomonth = hcCertificationsResumeBankVm.ToMonth;
                    hc_resume_education.Toyear = hcCertificationsResumeBankVm.ToYear;

                    hc_resume_education.GradeId = hcCertificationsResumeBankVm.GradeCategoryId;
                    hc_resume_education.Score = hcCertificationsResumeBankVm.Score;
                    hc_resume_education.Instituteid = hcCertificationsResumeBankVm.TrainingProviderId;
                    hc_resume_education.Countryid = hcCertificationsResumeBankVm.CountryId;
                    hc_resume_education.Cityid = hcCertificationsResumeBankVm.CityId;
                    hc_resume_education.CampusTypeId = hcCertificationsResumeBankVm.CampusTypeId;
                    hc_resume_education.Isdeleted = 0;


                    hc_resume_education.Createddate = DateTime.Now;
                    hc_resume_education.Createduserid = userid;

                    await _con.InsertAsync<HcResumeCertifications>(hc_resume_education);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- Insert successfully";


                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeCertificationsRepository)} :::::: {nameof(Insert)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }
        public async Task<WebresponseNoData> Update(HcResumeCertificationsViewModel hcCertificationsResumeBankVm, long rid, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeCertifications hc_resume_education = await _con.GetAsync<HcResumeCertifications>(rid);

                    hc_resume_education.Resumeid = hcCertificationsResumeBankVm.ResumeId;
                    hc_resume_education.Title = hcCertificationsResumeBankVm.Title;
                    hc_resume_education.Typeid = hcCertificationsResumeBankVm.CertificationTypeId;
                    hc_resume_education.Frommonth = hcCertificationsResumeBankVm.FromMonth;
                    hc_resume_education.Fromyear = hcCertificationsResumeBankVm.FromYear;

                    hc_resume_education.Tomonth = hcCertificationsResumeBankVm.ToMonth;
                    hc_resume_education.Toyear = hcCertificationsResumeBankVm.ToYear;

                    hc_resume_education.GradeId = hcCertificationsResumeBankVm.GradeCategoryId;
                    hc_resume_education.Score = hcCertificationsResumeBankVm.Score;
                    hc_resume_education.Instituteid = hcCertificationsResumeBankVm.TrainingProviderId;
                    hc_resume_education.Countryid = hcCertificationsResumeBankVm.CountryId;
                    hc_resume_education.Cityid = hcCertificationsResumeBankVm.CityId;
                    hc_resume_education.CampusTypeId = hcCertificationsResumeBankVm.CampusTypeId;
                    

                    hc_resume_education.Modifieddate = DateTime.Now;
                    hc_resume_education.Modifieduserid = userid;

                    await _con.UpdateAsync<HcResumeCertifications>(hc_resume_education);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- update successfully";

                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeCertificationsRepository)} :::::: {nameof(Update)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<Webresponse<HcResumeCertificationsViewModel_Get>> Delete(long rid)
        {
            Webresponse<HcResumeCertificationsViewModel_Get> webresponse = new Webresponse<HcResumeCertificationsViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    await _con.QueryAsync($"update HC_RESUME_CERTIFICATIONS set isdeleted=1 where RID={rid}");
                    webresponse.status = APIStatus.success;
                    webresponse.message = "Record deleted";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeCertificationsRepository)} :::::: {nameof(Delete)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<HcResumeCertificationsViewModel_Get>> GetById(long rid)
        {
            Webresponse<HcResumeCertificationsViewModel_Get> webresponse = new Webresponse<HcResumeCertificationsViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumeCertifications hc_resume_education = await _con.QueryFirstOrDefaultAsync<HcResumeCertifications>($"select * from HC_RESUME_CERTIFICATIONS where RID={rid} and isdeleted != 1");
                    if (hc_resume_education != null)
                    {
                        HcResumeCertificationsViewModel_Get hcCertificationsResumeBankVm = Convert_Model_to_ViewModel(hc_resume_education);
                        webresponse.data = hcCertificationsResumeBankVm;
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
                    _ilogger.LogError($"{nameof(HcResumeCertificationsRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<HcResumeCertificationsViewModel_Get>>> GetByResumeId(long resumeId)
        {
            Webresponse<IList<HcResumeCertificationsViewModel_Get>> webresponse = new Webresponse<IList<HcResumeCertificationsViewModel_Get>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    string sqlStr = "  select cer.RID as Rid,cer.Resumeid,cer.title,cer.typeid,ctype.Title as CertificateType,cer.frommonth,cer.fromyear,cer.tomonth,cer.toyear,cer.gradeid,cer.score,";
                    sqlStr += "  cer.instituteid,pro.ProviderName as TrainingProvider,cer.countryid,iCountry.CompanyTitle as Country,cer.cityid, iCity.Title as City,cer.createddate,cer.createduserid,cer.modifieddate,cer.modifieduserid,cer.isdeleted,cer.campustypeid";
                    sqlStr += "  from [HC_RESUME_CERTIFICATIONS] cer left outer join HCM_CERTIFICATION ctype on ctype.EngKeyID = cer.typeid and ctype.LanguageType=0";
                    sqlStr += "  left outer join HCM_TRAINING_PROVIDERS pro on pro.RID = cer.instituteid";
                    sqlStr += "  left outer join HCM_COMPANY_TYPE iCountry on iCountry.EngKeyID = cer.countryid and iCountry.LanguageType=0";
                    sqlStr += "  left outer join HCM_COMPANY_LIST iCity on iCity.RID = cer.cityid";
                    sqlStr += $"  where cer.Resumeid = {resumeId} and cer.isdeleted != 1";

                    IEnumerable<HcResumeCertificationsViewModel_Get> hc_resume_educations = await _con.QueryAsync<HcResumeCertificationsViewModel_Get>(sqlStr);

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
                    _ilogger.LogError($"{nameof(HcResumeCertificationsRepository)} :::::: {nameof(GetByResumeId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        private HcResumeCertificationsViewModel_Get Convert_Model_to_ViewModel(HcResumeCertifications _hc_resume_bank)
        {
            HcResumeCertificationsViewModel_Get hcCertificationsResumeBankVm = new HcResumeCertificationsViewModel_Get();
            try
            {
                hcCertificationsResumeBankVm.Rid = _hc_resume_bank.Rid;
                hcCertificationsResumeBankVm.ResumeId = _hc_resume_bank.Resumeid;
                hcCertificationsResumeBankVm.Title = _hc_resume_bank.Title;
                hcCertificationsResumeBankVm.CertificationTypeId = _hc_resume_bank.Typeid;
                hcCertificationsResumeBankVm.FromMonth = _hc_resume_bank.Frommonth;
                hcCertificationsResumeBankVm.FromYear = _hc_resume_bank.Fromyear;

                hcCertificationsResumeBankVm.ToMonth = _hc_resume_bank.Tomonth;
                hcCertificationsResumeBankVm.ToYear = _hc_resume_bank.Toyear;

                hcCertificationsResumeBankVm.GradeCategoryId = _hc_resume_bank.GradeId;
                hcCertificationsResumeBankVm.Score = _hc_resume_bank.Score;
                hcCertificationsResumeBankVm.TrainingProviderId = _hc_resume_bank.Instituteid;
                hcCertificationsResumeBankVm.CountryId = _hc_resume_bank.Countryid;
                hcCertificationsResumeBankVm.CityId = _hc_resume_bank.Cityid;
                hcCertificationsResumeBankVm.CampusTypeId = _hc_resume_bank.CampusTypeId;
                hcCertificationsResumeBankVm.CreatedDate = _hc_resume_bank.Createddate;
                hcCertificationsResumeBankVm.CreatedUserId = _hc_resume_bank.Createduserid;
                hcCertificationsResumeBankVm.ModifiedDate = _hc_resume_bank.Modifieddate;
                hcCertificationsResumeBankVm.ModifiedUserid = _hc_resume_bank.Modifieduserid;
            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumeCertificationsRepository)} :::::: {nameof(Convert_Model_to_ViewModel)} :::: {ex.Message}");
            }

            return hcCertificationsResumeBankVm;
        }


    }
}
