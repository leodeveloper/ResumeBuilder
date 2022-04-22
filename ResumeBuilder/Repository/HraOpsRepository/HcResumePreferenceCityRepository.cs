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
    public class HcResumePreferenceCityRepository : IHcResumePreferenceCityRepository
    {
        readonly ILogger<HcResumePreferenceCityRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;

        public HcResumePreferenceCityRepository(ILogger<HcResumePreferenceCityRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
        }

        public async Task<WebresponseNoData> Insert(HcResumePreferenceCityViewModel hcPreferenceCityResumeBankVm, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumePrefferedLocation hcPreferenceCityResumeBank = new HcResumePrefferedLocation();
                    hcPreferenceCityResumeBank.ResumeId = hcPreferenceCityResumeBankVm.ResumeId;
                    hcPreferenceCityResumeBank.LocationId = hcPreferenceCityResumeBankVm.CityId;
                    hcPreferenceCityResumeBank.Createddate = DateTime.Now;
                    hcPreferenceCityResumeBank.Createduserid = userid;


                    await _con.InsertAsync<HcResumePrefferedLocation>(hcPreferenceCityResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- Insert successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumePreferenceCityRepository)} :::::: {nameof(Insert)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }
        public async Task<WebresponseNoData> Update(HcResumePreferenceCityViewModel hcPreferenceCityResumeBankVm, long rid, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HcResumePrefferedLocation hcPreferenceCityResumeBank = await _con.GetAsync<HcResumePrefferedLocation>(rid);
                    hcPreferenceCityResumeBank.ResumeId = hcPreferenceCityResumeBankVm.ResumeId;
                    hcPreferenceCityResumeBank.LocationId = hcPreferenceCityResumeBankVm.CityId;

                    hcPreferenceCityResumeBank.Modifieddate = DateTime.Now;
                    hcPreferenceCityResumeBank.Modifieduserid = userid;
                    await _con.UpdateAsync<HcResumePrefferedLocation>(hcPreferenceCityResumeBank);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- update successfully";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumePreferenceCityRepository)} :::::: {nameof(Update)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<Webresponse<HcResumePreferenceCityViewModel_Get>> Delete(long rid)
        {
            Webresponse<HcResumePreferenceCityViewModel_Get> webresponse = new Webresponse<HcResumePreferenceCityViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    await _con.QueryAsync($"update HC_RESUME_PREFFERED_LOCATION set isdeleted=1 where RID={rid}");
                    webresponse.status = APIStatus.success;
                    webresponse.message = "Record deleted";
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumePreferenceCityRepository)} :::::: {nameof(Delete)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<HcResumePreferenceCityViewModel_Get>> GetById(long rid)
        {
            Webresponse<HcResumePreferenceCityViewModel_Get> webresponse = new Webresponse<HcResumePreferenceCityViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    string strSql = " SELECT location.RID,location.LocationID as CityId,rlocation.LocationTitle  as City,rlocationAe.LocationTitle as CityAe ,location.ResumeID,location.PType,";
                    strSql += " location.createduserid,location.createddate,location.modifieduserid,location.modifieddate , location.isdeleted ";
                    strSql += " FROM HC_RESUME_PREFFERED_LOCATION location";
                    strSql += " left outer join hcm_resume_locations rlocation on rlocation.EngKeyID = location.LocationID and rlocation.LanguageType = 0";
                    strSql += " left outer join hcm_resume_locations rlocationAe on rlocationAe.EngKeyID = location.LocationID and rlocationAe.LanguageType = 1";
                    strSql += $" where rlocation.ReqLocationStatus=1 and location.RID = {rid} and (isdeleted = 0  or isdeleted is null)";
                    HcResumePreferenceCityViewModel_Get hcPreferenceCityResumeBank = await _con.QueryFirstOrDefaultAsync<HcResumePreferenceCityViewModel_Get>(strSql);
                    if (hcPreferenceCityResumeBank != null)
                    {
                      
                        webresponse.data = hcPreferenceCityResumeBank;
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
                    _ilogger.LogError($"{nameof(HcResumePreferenceCityRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        public async Task<Webresponse<IList<HcResumePreferenceCityViewModel_Get>>> GetByResumeId(long resumeId)
        {
            Webresponse<IList<HcResumePreferenceCityViewModel_Get>> webresponse = new Webresponse<IList<HcResumePreferenceCityViewModel_Get>>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    string strSql = " SELECT location.RID as Rid,location.LocationID as CityId,rlocation.LocationTitle  as City,rlocationAe.LocationTitle as CityAe ,location.ResumeID,location.PType,";
                    strSql += " location.createduserid,location.createddate,location.modifieduserid,location.modifieddate , location.isdeleted ";
                    strSql += " FROM HC_RESUME_PREFFERED_LOCATION location";
                    strSql += " left outer join hcm_resume_locations rlocation on rlocation.EngKeyID = location.LocationID and rlocation.LanguageType = 0";
                    strSql += " left outer join hcm_resume_locations rlocationAe on rlocationAe.EngKeyID = location.LocationID and rlocationAe.LanguageType = 1";
                    strSql += $" where rlocation.ReqLocationStatus=1 and ResumeID = {resumeId} and (isdeleted = 0  or isdeleted is null)";
                    IEnumerable<HcResumePreferenceCityViewModel_Get> hcPreferenceCityResumeBanks = await _con.QueryAsync<HcResumePreferenceCityViewModel_Get>(strSql);
                    if (hcPreferenceCityResumeBanks != null)
                    {
                        webresponse.data = hcPreferenceCityResumeBanks.ToList();
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
                    _ilogger.LogError($"{nameof(HcResumePreferenceCityRepository)} :::::: {nameof(GetByResumeId)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        private HcResumePreferenceCityViewModel_Get Convert_Model_to_ViewModel(HcResumePrefferedLocation _hc_resume_bank)
        {
            HcResumePreferenceCityViewModel_Get hcPreferenceCityResumeBankVm = new HcResumePreferenceCityViewModel_Get();
            try
            {
                hcPreferenceCityResumeBankVm.Rid = _hc_resume_bank.Rid;
                hcPreferenceCityResumeBankVm.ResumeId = _hc_resume_bank.ResumeId;
                hcPreferenceCityResumeBankVm.CityId = _hc_resume_bank.LocationId;


                hcPreferenceCityResumeBankVm.CreatedDate = _hc_resume_bank.Createddate;
                hcPreferenceCityResumeBankVm.CreatedUserId = _hc_resume_bank.Createduserid;
                hcPreferenceCityResumeBankVm.ModifiedDate = _hc_resume_bank.Modifieddate;
                hcPreferenceCityResumeBankVm.ModifiedUserId = _hc_resume_bank.Modifieduserid;

            }
            catch (Exception ex)
            {
                _ilogger.LogError($"{nameof(HcResumePreferenceCityRepository)} :::::: {nameof(Convert_Model_to_ViewModel)} :::: {ex.Message}");
            }

            return hcPreferenceCityResumeBankVm;
        }
    }
}
