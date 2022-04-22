using System;
using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ResumeBuilder.DapperUnitOfWork;
using ResumeBuilder.Models;
using ResumeBuilder.Models.HraOpsModels;
using ResumeBuilder.Dto;
using ResumeBuilder.Dto.HraOpsDto;
using System.Data;

namespace ResumeBuilder.Repository.HraOpsRepository
{
    public class HcResumeRepository : IHcResumeRepository
    {
        readonly ILogger<ResumeRepository> _ilogger;
        readonly IOptions<AppSettings> _appSettings;
        readonly IUnitOfWorkHra _iUnitOfWork;
        readonly ITypeSenceApiRepository _iTypeSenceApiRepository;

        public HcResumeRepository(ILogger<ResumeRepository> ilogger, IOptions<AppSettings> appSettings, IUnitOfWorkHra iUnitOfWork, ITypeSenceApiRepository iTypeSenceApiRepository)
        {
            _ilogger = ilogger;
            _appSettings = appSettings;
            _iUnitOfWork = iUnitOfWork;
            _iTypeSenceApiRepository = iTypeSenceApiRepository;
        }

        #region personalinfo
        public async Task<Webresponse<Hc_ResumeBankViewModel_Get>> Insert(Hc_ResumeBankViewModel hcResumeBankVm, long userid)
        {

            Webresponse<Hc_ResumeBankViewModel_Get> webresponseNoData = new Webresponse<Hc_ResumeBankViewModel_Get>();
            string newJboSeekerId = await getNewJobSeekerId(_iUnitOfWork.Connection);
            if (string.IsNullOrEmpty(newJboSeekerId))
            {
                webresponseNoData.message = $"Error in generating new jobseeker id check the stored procedure USP_Next_JobSeekerId";
                webresponseNoData.status = APIStatus.error;
                return webresponseNoData;
            }
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HC_RESUME_BANK hC_RESUME_BANK = new HC_RESUME_BANK();

                    hC_RESUME_BANK.UniqueNo = newJboSeekerId;
                    hC_RESUME_BANK.EmpId = "";
                    hC_RESUME_BANK.PinCode = "";
                    hC_RESUME_BANK.BankName = "";
                    hC_RESUME_BANK.BranchName = "";
                    hC_RESUME_BANK.AccountNumber = "";
                    hC_RESUME_BANK.CompanyEmployerId = "";
                    hC_RESUME_BANK.OnshoreLocation = "";
                    hC_RESUME_BANK.ExtIsPartTimeStudent = "";
                    hC_RESUME_BANK.FatherName = "";
                    hC_RESUME_BANK.IdentificationNumber = "";
                    hC_RESUME_BANK.ExtMilitaryService = "";
                    hC_RESUME_BANK.Nsrnumber = hcResumeBankVm.emirateid;
                    hC_RESUME_BANK.ExFullNameEng = hcResumeBankVm.fullnameen;
                    hC_RESUME_BANK.ExFullNameAr = hcResumeBankVm.fullnameae;
                    hC_RESUME_BANK.Dob = hcResumeBankVm.dob;
                    hC_RESUME_BANK.Gender = hcResumeBankVm.genderid;
                    hC_RESUME_BANK.FirstName = hcResumeBankVm.firstnameen;
                    hC_RESUME_BANK.MiddleName = hcResumeBankVm.secondnameen;
                    hC_RESUME_BANK.Mname = hcResumeBankVm.secondnameen;
                    hC_RESUME_BANK.ExThirdName = hcResumeBankVm.thirdnameen;
                    hC_RESUME_BANK.LastName = hcResumeBankVm.fourthnameen;
                    hC_RESUME_BANK.ExFamilyNameEng = hcResumeBankVm.tribename;

                    hC_RESUME_BANK.ExFirstNameAr = hcResumeBankVm.firstnameae;
                    hC_RESUME_BANK.ExSecondNameAr = hcResumeBankVm.secondnameae;
                    hC_RESUME_BANK.ExThirdNameAr = hcResumeBankVm.thirdnameae;
                    hC_RESUME_BANK.ExLastNameAr = hcResumeBankVm.fourthnameae;
                    hC_RESUME_BANK.ExFamilyNameAr = hcResumeBankVm.tribenameae;

                    hC_RESUME_BANK.ExKaqfamilyNo = hcResumeBankVm.familybookno;
                    hC_RESUME_BANK.SubFunctionText = hcResumeBankVm.kalasathno;
                    hC_RESUME_BANK.VisaText = hcResumeBankVm.edbarano;
                    hC_RESUME_BANK.ExJd = hcResumeBankVm.countryofbirthid;
                    hC_RESUME_BANK.ExPlaceofBirth = hcResumeBankVm.placeofbirthen;
                    hC_RESUME_BANK.ExtPlaceofBirth = hcResumeBankVm.placeofbirthae;

                    hC_RESUME_BANK.ResPrefLocation = hcResumeBankVm.emirateofbirthid.ToString();
                    hC_RESUME_BANK.ResWillingToTravel = hcResumeBankVm.cityofbirthid.ToString();
                    hC_RESUME_BANK.ThumbNail = hcResumeBankVm.countryofbirthid.ToString();

                    if (hcResumeBankVm.emirateofbirthid != null)
                        hC_RESUME_BANK.ResPrefLocation = hcResumeBankVm.emirateofbirthid.ToString();

                    hC_RESUME_BANK.AvailableAfter = hcResumeBankVm.eidissuedate;
                    hC_RESUME_BANK.ResPrefJoiningDate = hcResumeBankVm.eidexpirydate;
                    hC_RESUME_BANK.MaritialstatusVal = hcResumeBankVm.maritalstatusid;
                    hC_RESUME_BANK.PassportNo = hcResumeBankVm.passportno;
                    hC_RESUME_BANK.ValidatedDate = hcResumeBankVm.passportissuedate;
                    hC_RESUME_BANK.ExtPassportExpiry = hcResumeBankVm.passportexpirydate;
                    hC_RESUME_BANK.CreatedDate = DateTime.Now;
                    hC_RESUME_BANK.LastUpdateDate = DateTime.Now;
                    hC_RESUME_BANK.DocModifiedDate = DateTime.Now;
                    hC_RESUME_BANK.ExpCompDate = DateTime.Now;
                    hC_RESUME_BANK.CreatedUserId = userid;
                    hC_RESUME_BANK.SourceDate = Convert.ToDateTime("1900-01-01 00:00:00.000");
                    hC_RESUME_BANK.LastOfferRejectionDateRank = Convert.ToDateTime("1900-01-01 00:00:00.000");
                    hC_RESUME_BANK.LastResignationdaterank = Convert.ToDateTime("1900-01-01 00:00:00.000");
                    await _con.InsertAsync<HC_RESUME_BANK>(hC_RESUME_BANK);
                    webresponseNoData = await GetByJobSeekerId(hC_RESUME_BANK.UniqueNo);
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message += " -- Insert successfully";
                    _iTypeSenceApiRepository.UpdateTypeSenceIndex(hcResumeBankVm.rid);
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeRepository)} :::::: {nameof(Insert)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }

            }
            return webresponseNoData;
        }

        public async Task<Webresponse<Hc_ResumeBankViewModel_Get>> Update(Hc_ResumeBankViewModel hcResumeBankVm, long userid)
        {
            Webresponse<Hc_ResumeBankViewModel_Get> webresponseNoData = new Webresponse<Hc_ResumeBankViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HC_RESUME_BANK hC_RESUME_BANK = await _con.GetAsync<HC_RESUME_BANK>(hcResumeBankVm.rid);

                    if(hC_RESUME_BANK == null)
                    {
                        webresponseNoData.status = APIStatus.NotFound;
                        webresponseNoData.message = $"No record found against this rid {hcResumeBankVm.rid}";
                    }
                    else
                    {
                        hC_RESUME_BANK.Nsrnumber = hcResumeBankVm.emirateid;
                        hC_RESUME_BANK.ExFullNameEng = hcResumeBankVm.fullnameen;
                        hC_RESUME_BANK.ExFullNameAr = hcResumeBankVm.fullnameae;
                        hC_RESUME_BANK.Dob = hcResumeBankVm.dob;
                        hC_RESUME_BANK.Gender = hcResumeBankVm.genderid;
                        hC_RESUME_BANK.FirstName = hcResumeBankVm.firstnameen;
                        hC_RESUME_BANK.MiddleName = hcResumeBankVm.secondnameen;
                        hC_RESUME_BANK.ExThirdName = hcResumeBankVm.thirdnameen;
                        hC_RESUME_BANK.LastName = hcResumeBankVm.fourthnameen;
                        hC_RESUME_BANK.ExFamilyNameEng = hcResumeBankVm.tribename;

                        hC_RESUME_BANK.ExFirstNameAr = hcResumeBankVm.firstnameae;
                        hC_RESUME_BANK.ExSecondNameAr = hcResumeBankVm.secondnameae;
                        hC_RESUME_BANK.ExThirdNameAr = hcResumeBankVm.thirdnameae;
                        hC_RESUME_BANK.ExLastNameAr = hcResumeBankVm.fourthnameae;
                        hC_RESUME_BANK.ExFamilyNameAr = hcResumeBankVm.tribenameae;

                        hC_RESUME_BANK.ExKaqfamilyNo = hcResumeBankVm.familybookno;
                        hC_RESUME_BANK.SubFunctionText = hcResumeBankVm.kalasathno;
                        hC_RESUME_BANK.VisaText = hcResumeBankVm.edbarano;
                        hC_RESUME_BANK.ExJd = hcResumeBankVm.countryofbirthid;
                        hC_RESUME_BANK.ExPlaceofBirth = hcResumeBankVm.placeofbirthen;
                        hC_RESUME_BANK.ExtPlaceofBirth = hcResumeBankVm.placeofbirthae;

                        hC_RESUME_BANK.ResPrefLocation = hcResumeBankVm.emirateofbirthid.ToString();
                        hC_RESUME_BANK.ResWillingToTravel = hcResumeBankVm.cityofbirthid.ToString();
                        hC_RESUME_BANK.ThumbNail = hcResumeBankVm.countryofbirthid.ToString();

                        if (hcResumeBankVm.emirateofbirthid != null)
                            hC_RESUME_BANK.ResPrefLocation = hcResumeBankVm.emirateofbirthid.ToString();

                        hC_RESUME_BANK.AvailableAfter = hcResumeBankVm.eidissuedate;
                        hC_RESUME_BANK.ResPrefJoiningDate = hcResumeBankVm.eidexpirydate;
                        hC_RESUME_BANK.MaritialstatusVal = hcResumeBankVm.maritalstatusid;
                        hC_RESUME_BANK.PassportNo = hcResumeBankVm.passportno;
                        hC_RESUME_BANK.ValidatedDate = hcResumeBankVm.passportissuedate;
                        hC_RESUME_BANK.ExtPassportExpiry = hcResumeBankVm.passportexpirydate;
                        hC_RESUME_BANK.LastUpdateDate = DateTime.Now;
                        hC_RESUME_BANK.LastModifiedUserId = userid;
                        await _con.UpdateAsync<HC_RESUME_BANK>(hC_RESUME_BANK);
                        webresponseNoData = await GetByJobSeekerId(hC_RESUME_BANK.UniqueNo);
                        webresponseNoData.status = APIStatus.success;
                        webresponseNoData.message += " -- Update successfully";
                        _iTypeSenceApiRepository.UpdateTypeSenceIndex(hcResumeBankVm.rid);
                    }                   
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeRepository)} :::::: {nameof(Update)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }

            }
            return webresponseNoData;
        }

        public async Task<Webresponse<Hc_ResumeBankViewModel_Get>> GetById(long rid)
        {
            Webresponse<Hc_ResumeBankViewModel_Get> webresponse = new Webresponse<Hc_ResumeBankViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HC_RESUME_BANK _hc_resume_bank = await _con.GetAsync<HC_RESUME_BANK>(rid);
                    if(_hc_resume_bank != null)
                    {
                        Hc_ResumeBankViewModel_Get hcResumeBankVm = Convert_Model_to_ViewModel(_hc_resume_bank, _con);
                        webresponse.data = hcResumeBankVm;
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
                    _ilogger.LogError($"{nameof(HcResumeRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }

            return webresponse;
        }
        public async Task<Webresponse<Hc_ResumeBankViewModel_Get>> GetByEmiratesId(string emiratesId)
        {
            
            Webresponse<Hc_ResumeBankViewModel_Get> webresponse = new Webresponse<Hc_ResumeBankViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HC_RESUME_BANK _hc_resume_bank = await _con.QueryFirstOrDefaultAsync<HC_RESUME_BANK>($"select * from HC_RESUME_BANK where Nsrnumber='{emiratesId}'");
                    if (_hc_resume_bank != null)
                    {
                        Hc_ResumeBankViewModel_Get hcResumeBankVm = Convert_Model_to_ViewModel(_hc_resume_bank, _con);

                        webresponse.data = hcResumeBankVm;
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
                    _ilogger.LogError($"{nameof(HcResumeRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }

            return webresponse;
        }

        public async Task<Webresponse<Hc_ResumeBankViewModel_Get>> GetByJobSeekerId(string jobseekerId)
        {

            Webresponse<Hc_ResumeBankViewModel_Get> webresponse = new Webresponse<Hc_ResumeBankViewModel_Get>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HC_RESUME_BANK _hc_resume_bank = await _con.QueryFirstOrDefaultAsync<HC_RESUME_BANK>($"select * from HC_RESUME_BANK where UniqueNo='{jobseekerId}'");
                    if (_hc_resume_bank != null)
                    {
                        Hc_ResumeBankViewModel_Get hcResumeBankVm = Convert_Model_to_ViewModel(_hc_resume_bank, _con);

                        webresponse.data = hcResumeBankVm;
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
                    _ilogger.LogError($"{nameof(HcResumeRepository)} :::::: {nameof(GetById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }

            return webresponse;
        }

        private static Hc_ResumeBankViewModel_Get Convert_Model_to_ViewModel(HC_RESUME_BANK _hc_resume_bank, IDbConnection _con)
        {
            Hc_ResumeBankViewModel_Get hcResumeBankVm = new Hc_ResumeBankViewModel_Get();
            hcResumeBankVm.emirateid = _hc_resume_bank.Nsrnumber;
            hcResumeBankVm.fullnameen = _hc_resume_bank.ExFullNameEng;
            hcResumeBankVm.fullnameae = _hc_resume_bank.ExFullNameAr;
            hcResumeBankVm.dob = _hc_resume_bank.Dob;
            hcResumeBankVm.genderid = _hc_resume_bank.Gender;
            hcResumeBankVm.firstnameen = _hc_resume_bank.FirstName;
            hcResumeBankVm.secondnameen = _hc_resume_bank.MiddleName;
            hcResumeBankVm.thirdnameen = _hc_resume_bank.ExThirdName;
            hcResumeBankVm.fourthnameen = _hc_resume_bank.LastName;
            hcResumeBankVm.tribename = _hc_resume_bank.ExFamilyNameEng;
            hcResumeBankVm.resumestatus = _hc_resume_bank.ResumeStatus;
            hcResumeBankVm.unifiedno = _hc_resume_bank.UniqueNo;

            hcResumeBankVm.firstnameae = _hc_resume_bank.ExFirstNameAr;
            hcResumeBankVm.secondnameae = _hc_resume_bank.ExSecondNameAr;
            hcResumeBankVm.thirdnameae = _hc_resume_bank.ExThirdNameAr;
            hcResumeBankVm.fourthnameae = _hc_resume_bank.ExLastNameAr;
            hcResumeBankVm.tribenameae = _hc_resume_bank.ExFamilyNameAr;

            hcResumeBankVm.familybookno = _hc_resume_bank.ExKaqfamilyNo;
            hcResumeBankVm.kalasathno = _hc_resume_bank.SubFunctionText;
            hcResumeBankVm.edbarano = _hc_resume_bank.VisaText;
            hcResumeBankVm.rid = _hc_resume_bank.Rid;


            hcResumeBankVm.countryofbirthid = _hc_resume_bank.ExJd;

            hcResumeBankVm.placeofbirthen = _hc_resume_bank.ExPlaceofBirth;
            hcResumeBankVm.placeofbirthae = _hc_resume_bank.ExtPlaceofBirth;

            if (int.TryParse(_hc_resume_bank.ResPrefLocation, out int _cityofbirthid))
                hcResumeBankVm.cityofbirthid = _cityofbirthid;
            if (int.TryParse(_hc_resume_bank.ResPrefLocation, out int _countryofbirthid))
                hcResumeBankVm.countryofbirthid = _countryofbirthid;

            if (int.TryParse(_hc_resume_bank.ResPrefLocation, out int _emirateofbirthid))
                hcResumeBankVm.emirateofbirthid = _emirateofbirthid;

            hcResumeBankVm.eidissuedate = _hc_resume_bank.AvailableAfter;
            hcResumeBankVm.eidexpirydate = _hc_resume_bank.ResPrefJoiningDate;
            hcResumeBankVm.maritalstatusid = _hc_resume_bank.MaritialstatusVal;
            hcResumeBankVm.passportno = _hc_resume_bank.PassportNo;
            hcResumeBankVm.passportissuedate = _hc_resume_bank.ValidatedDate;
            hcResumeBankVm.passportexpirydate = _hc_resume_bank.ExtPassportExpiry;
            hcResumeBankVm.ModifiedDate = _hc_resume_bank.LastUpdateDate;
            hcResumeBankVm.CreatedDate = _hc_resume_bank.CreatedDate;

            hcResumeBankVm.JPCAssessment = _hc_resume_bank.JPCAssessment;
            hcResumeBankVm.JPCAssessmentStatusID = _hc_resume_bank.JPCAssessmentStatusID;
            IList<HcmResumeJpcStatus> hcmResumeJpcStatus = _con.Query<HcmResumeJpcStatus>($"select * from Hcm_Resume_Jpc_Status where EngKeyID={_hc_resume_bank.JPCAssessmentStatusID}").ToList();
            if(hcmResumeJpcStatus.Count > 0)
            {
                hcResumeBankVm.JPCAssessmentStatus = hcmResumeJpcStatus.FirstOrDefault(z => z.LanguageType == 0).Title;
                hcResumeBankVm.JPCAssessmentStatusAe = hcmResumeJpcStatus.FirstOrDefault(z => z.LanguageType == 1).Title;
            }
          
            hcResumeBankVm.JPCAssessmentStatusID = _hc_resume_bank.JPCAssessmentStatusID;
            hcResumeBankVm.ChallangesNotes = _hc_resume_bank.ChallangesNotes;

            return hcResumeBankVm;
        }

        private async Task<string> getNewJobSeekerId(IDbConnection dbConnection)
        {
            string jobseekerId=string.Empty;
           
                try
                {
                    var p = new DynamicParameters();
                    p.Add("jobSeekerId", dbType: DbType.String, size: 200, direction: ParameterDirection.Output);
                    p.Add("_status", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                    await dbConnection.QueryAsync("USP_Next_JobSeekerId", p, commandType: CommandType.StoredProcedure);
                    bool status = p.Get<bool>("_status");

                    if (status)
                        jobseekerId = p.Get<string>("jobSeekerId");
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeRepository)} ::::: {nameof(getNewJobSeekerId)} :::: {ex.Message}");
                }
           
            
            return jobseekerId;
        }

        public async Task<WebresponseNoData> InsertChallanges(HcResumeChallangesViewModel hcResumeChallangesViewModel, int userId)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    if(!string.IsNullOrEmpty(hcResumeChallangesViewModel.Notes))
                    {
                        HC_RESUME_BANK hc_RESUME_BANK = await _con.GetAsync<HC_RESUME_BANK>(hcResumeChallangesViewModel.ResumeId);
                        hc_RESUME_BANK.ChallangesNotes = hcResumeChallangesViewModel.Notes;
                        await _con.UpdateAsync<HC_RESUME_BANK>(hc_RESUME_BANK);
                    }
                    
                    IEnumerable<HcResumeChallanges> hcResumeChallanges = await _con.QueryAsync<HcResumeChallanges>($"delete from HC_RESUME_CHALLANGES where ResumeId={hcResumeChallangesViewModel.ResumeId}");

                    foreach(var challangeId in hcResumeChallangesViewModel.ChallangeIds)
                    {
                        HcResumeChallanges newhcResumeChallange = new HcResumeChallanges();
                        newhcResumeChallange.ChallangeId = challangeId;
                        newhcResumeChallange.ResumeId = hcResumeChallangesViewModel.ResumeId;
                        newhcResumeChallange.CreatedUserId = userId;
                        newhcResumeChallange.CreatedDate = DateTime.Now;
                        await _con.InsertAsync<HcResumeChallanges>(newhcResumeChallange);
                    }

                    webresponseNoData.status = APIStatus.success;

                }
                catch (Exception ex) 
                {
                    _ilogger.LogError($"{nameof(HcResumeRepository)} ::::: {nameof(InsertChallanges)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<Webresponse<HcResumeChallangesViewModel>> GetChallanges(long resumeId)
        {
            Webresponse<HcResumeChallangesViewModel> webresponse = new Webresponse<HcResumeChallangesViewModel>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HC_RESUME_BANK hc_RESUME_BANK = await _con.GetAsync<HC_RESUME_BANK>(resumeId);
                    HcResumeChallangesViewModel hcResumeChallangesViewModel = new HcResumeChallangesViewModel();
                    hcResumeChallangesViewModel.ChallangeIds = new List<int>();
                    hcResumeChallangesViewModel.ResumeId = hc_RESUME_BANK.Rid;
                    hcResumeChallangesViewModel.Notes = hc_RESUME_BANK.ChallangesNotes;

                    IEnumerable<HcResumeChallanges> hcResumeChallanges = await _con.QueryAsync<HcResumeChallanges>($"select * from HC_RESUME_CHALLANGES where ResumeId={resumeId}");

                    foreach (var challangeId in hcResumeChallanges)
                    {
                        hcResumeChallangesViewModel.ChallangeIds.Add(challangeId.ChallangeId);
                    }

                    webresponse.data = hcResumeChallangesViewModel;
                    webresponse.status = APIStatus.success;
                    

                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeRepository)} ::::: {nameof(GetChallanges)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }
            return webresponse;
        }

        #endregion

        #region contactinfo
        public async Task<WebresponseNoData> Update(Hc_ContactInfoViewModel hcContactVm, long rid, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HC_RESUME_BANK hc_RESUME_BANK = await _con.GetAsync<HC_RESUME_BANK>(rid);

                    if (hc_RESUME_BANK == null)
                    {
                        webresponseNoData.status = APIStatus.NotFound;
                        webresponseNoData.message = $"No record found against this rid {rid}";
                    }
                    else
                    {
                        hc_RESUME_BANK.CountryId = hcContactVm.emirate;
                        hc_RESUME_BANK.StateId = hcContactVm.cityid;
                     
                        hc_RESUME_BANK.LocationId = hcContactVm.locationid;
                        hc_RESUME_BANK.ExSecretQ = hcContactVm.pobox;

                        hc_RESUME_BANK.Address1 = hcContactVm.address;
                        hc_RESUME_BANK.Mobile = hcContactVm.mobile;
                        hc_RESUME_BANK.EmailId = hcContactVm.email;
                        hc_RESUME_BANK.OfficePh = hcContactVm.phoneno;
                        hc_RESUME_BANK.OfficePhExt = hcContactVm.alterphoneno;

                        hc_RESUME_BANK.LastUpdateDate = DateTime.Now;
                        hc_RESUME_BANK.LastModifiedUserId = userid;
                        await _con.UpdateAsync<HC_RESUME_BANK>(hc_RESUME_BANK);
                        webresponseNoData.status = APIStatus.success;
                        webresponseNoData.message = "Update successfully";
                    }
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeRepository)} :::::: {nameof(Update)} AddionalInfo:::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }

            }
            return webresponseNoData;
        }
        public async Task<Webresponse<Hc_ContactInfoViewModel>> GetContactInfoById(long rid)
        {
            Webresponse<Hc_ContactInfoViewModel> webresponse = new Webresponse<Hc_ContactInfoViewModel>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HC_RESUME_BANK hc_resume_bank = await _con.GetAsync<HC_RESUME_BANK>(rid);
                    Hc_ContactInfoViewModel hcResumeBankVmContactInfo = new Hc_ContactInfoViewModel();


                    hcResumeBankVmContactInfo.emirate = hc_resume_bank.CountryId;
                    hcResumeBankVmContactInfo.cityid = hc_resume_bank.StateId;
                   
                    hcResumeBankVmContactInfo.locationid = hc_resume_bank.LocationId;
                    hcResumeBankVmContactInfo.pobox = hc_resume_bank.ExSecretQ;

                    hcResumeBankVmContactInfo.address = hc_resume_bank.Address1;
                    hcResumeBankVmContactInfo.mobile = hc_resume_bank.Mobile;
                    hcResumeBankVmContactInfo.email = hc_resume_bank.EmailId;
                    hcResumeBankVmContactInfo.phoneno = hc_resume_bank.OfficePh;
                    hcResumeBankVmContactInfo.alterphoneno = hc_resume_bank.OfficePhExt;



                    webresponse.data = hcResumeBankVmContactInfo;
                    webresponse.status = APIStatus.success;
                    webresponse.message = "Record found";

                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeRepository)} :::::: {nameof(GetContactInfoById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }

            return webresponse;
        }
        #endregion

        #region AdditionalInfo
        public async Task<WebresponseNoData> Update(Hc_AdditionalInfoViewModel hcAdditionalVm, long rid, long userid)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HC_RESUME_BANK hc_RESUME_BANK = await _con.GetAsync<HC_RESUME_BANK>(rid);

                    if (hc_RESUME_BANK == null)
                    {
                        webresponseNoData.status = APIStatus.NotFound;
                        webresponseNoData.message = $"No record found against this rid {rid}";
                    }
                    else
                    {
                        hc_RESUME_BANK.ExSplNeed = hcAdditionalVm.ispod.ToString();
                        hc_RESUME_BANK.IsSSABeneficiary = hcAdditionalVm.IsSSABeneficiary;
                       
                        hc_RESUME_BANK.IsMilitaryCompleted = hcAdditionalVm.ismilitarycompleted;
                        hc_RESUME_BANK.Rating = hcAdditionalVm.militarybatchnoid;

                        hc_RESUME_BANK.EmployeeId = hcAdditionalVm.isdrivinglicense;
                        hc_RESUME_BANK.ExDrivingLicenceText = hcAdditionalVm.typeofdrivinglicenseid.ToString();

                        hc_RESUME_BANK.ResWillingToTravel = hcAdditionalVm.linkedinurl;
                        hc_RESUME_BANK.AlarmNotes = hcAdditionalVm.twitterurl;

                        hc_RESUME_BANK.CvcompleteFlag = hcAdditionalVm.llpprofile;
                        hc_RESUME_BANK.Panno = hcAdditionalVm.takafoprofile;
                        hc_RESUME_BANK.OnshoreLocation = hcAdditionalVm.hrmsprofile;
                        hc_RESUME_BANK.SubFunctionText = hcAdditionalVm.nafisprofile;
                        hc_RESUME_BANK.ConfUsers = hcAdditionalVm.studentidcard;

                        hc_RESUME_BANK.LastUpdateDate = DateTime.Now;
                        hc_RESUME_BANK.LastModifiedUserId = userid;
                        await _con.UpdateAsync<HC_RESUME_BANK>(hc_RESUME_BANK);
                        webresponseNoData.status = APIStatus.success;
                        webresponseNoData.message = "Update successfully";
                        _iTypeSenceApiRepository.UpdateTypeSenceIndex(rid);
                    }
                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeRepository)} :::::: {nameof(Update)} AddionalInfo:::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }

            }
            return webresponseNoData;
        }
        public async Task<Webresponse<Hc_AdditionalInfoViewModel>> GetAdditionalInfoById(long rid)
        {
            Webresponse<Hc_AdditionalInfoViewModel> webresponse = new Webresponse<Hc_AdditionalInfoViewModel>();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    HC_RESUME_BANK hc_resume_bank = await _con.GetAsync<HC_RESUME_BANK>(rid);
                    Hc_AdditionalInfoViewModel hcResumeBankVmAdditionalInfo = new Hc_AdditionalInfoViewModel();


                    if(bool.TryParse(hc_resume_bank.ExSplNeed, out bool _ispod))
                        hcResumeBankVmAdditionalInfo.ispod = _ispod;

                    hcResumeBankVmAdditionalInfo.IsSSABeneficiary = hc_resume_bank.IsSSABeneficiary;                   
                    hcResumeBankVmAdditionalInfo.ismilitarycompleted = hc_resume_bank.IsMilitaryCompleted ;
                    hcResumeBankVmAdditionalInfo.militarybatchnoid = hc_resume_bank.Rating;

                     hcResumeBankVmAdditionalInfo.isdrivinglicense = hc_resume_bank.EmployeeId;
                    if(int.TryParse(hc_resume_bank.ExDrivingLicenceText, out int _typeofdrivinglicenseid))
                        hcResumeBankVmAdditionalInfo.typeofdrivinglicenseid= _typeofdrivinglicenseid;

                    hcResumeBankVmAdditionalInfo.linkedinurl = hc_resume_bank.ResWillingToTravel;
                    hcResumeBankVmAdditionalInfo.twitterurl = hc_resume_bank.AlarmNotes;

                    hcResumeBankVmAdditionalInfo.llpprofile = hc_resume_bank.CvcompleteFlag;
                    hcResumeBankVmAdditionalInfo.takafoprofile = hc_resume_bank.Panno;
                    hcResumeBankVmAdditionalInfo.hrmsprofile = hc_resume_bank.OnshoreLocation;
                    hcResumeBankVmAdditionalInfo.nafisprofile = hc_resume_bank.SubFunctionText;
                    hcResumeBankVmAdditionalInfo.studentidcard = hc_resume_bank.ConfUsers;



                    webresponse.data = hcResumeBankVmAdditionalInfo;
                    webresponse.status = APIStatus.success;
                    webresponse.message = "Record found";

                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeRepository)} :::::: {nameof(GetAdditionalInfoById)} :::: {ex.Message}");
                    webresponse.status = APIStatus.error;
                    webresponse.message = ex.Message;
                }
            }

            return webresponse;
        }
        #endregion

        #region JPCAssessment
        public async Task<WebresponseNoData> UpdateJPCAssessment(long Rid, int JPCAssessment)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    await _con.QueryAsync($"update hc_resume_bank set JPCAssessment={JPCAssessment} where Rid={Rid}");
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message = "Record update found";
                    _iTypeSenceApiRepository.UpdateTypeSenceIndex(Rid);

                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeRepository)} :::::: {nameof(UpdateJPCAssessment)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }

        public async Task<WebresponseNoData> UpdateJPCAssessmentstatus(long Rid, string JPCAssessmentStaus)
        {
            WebresponseNoData webresponseNoData = new WebresponseNoData();
            using (var _con = _iUnitOfWork.Connection)
            {
                try
                {
                    await _con.QueryAsync($"update hc_resume_bank set JPCAssessmentStatus='{JPCAssessmentStaus}' where Rid={Rid}");
                    webresponseNoData.status = APIStatus.success;
                    webresponseNoData.message = "Record update found";
                    _iTypeSenceApiRepository.UpdateTypeSenceIndex(Rid);

                }
                catch (Exception ex)
                {
                    _ilogger.LogError($"{nameof(HcResumeRepository)} :::::: {nameof(UpdateJPCAssessment)} :::: {ex.Message}");
                    webresponseNoData.status = APIStatus.error;
                    webresponseNoData.message = ex.Message;
                }
            }
            return webresponseNoData;
        }
        #endregion
    }
}
