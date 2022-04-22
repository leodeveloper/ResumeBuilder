using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Microsoft.Extensions.Configuration;
using ResumeBuilder.XPOHireCraft.Database;
using ResumeBuilder.Dto;
using ResumeBuilder.Repository;
using Microsoft.AspNetCore.Authorization;

namespace ResumeBuilder.Controllers
{
    // If you need to use Data Annotation attributes, attach them to this view model instead of an XPO data model.
   
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class HcResumeBankController : Controller
    {

        private IConfiguration _iConfiguration;
        readonly IlookupRespository _ilookupRespository;
        readonly IEducationRepository _iEducationRepository;
        readonly IResumeRepository _iResumeRepository;

        public HcResumeBankController(IConfiguration configuration, IResumeRepository iResumeRepository, IlookupRespository ilookupRespository, IEducationRepository iEducationRepository)
        {
            _iConfiguration = configuration;
            _ilookupRespository = ilookupRespository;
            _iEducationRepository = iEducationRepository;
            _iResumeRepository = iResumeRepository;
        }

        private UnitOfWork GetConnection()
        {
            return new UnitOfWork(ConnectionHelper.GetDataLayer(_iConfiguration, AutoCreateOption.SchemaAlreadyExists));
        }

       

        //public HcResumeBankController(IConfiguration configuration) {
        //    this._uow = new UnitOfWork(ConnectionHelper.GetDataLayer(configuration, AutoCreateOption.SchemaAlreadyExists));
        //}

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions, int? LocationId = null,
            int? EducationGroupId = null,
            int? EducationTypeId = null,
            int? EducationMajorId = null,
            string StatusId = null,
            string EmiratesId = null,
            string JobSeekerId = null,
            string CommaSepratedFilter = null
            )
        {

            IList<int> statusIdList = new List<int>();
            if (StatusId != null)
            {
                statusIdList = StatusId.Split(',').Select(int.Parse).ToList();
            }

            IList<string> emiratesIdList = new List<string>();
            if (EmiratesId != null)
            {
                emiratesIdList = EmiratesId.Split(',').ToList();
            }

            IList<string> jobseekerIdList = new List<string>();
            if (JobSeekerId != null)
            {
                jobseekerIdList = JobSeekerId.Split(',').ToList();
            }

            IList<string> CommaSepratedList = new List<string>();
            if (CommaSepratedFilter != null)
            {
                CommaSepratedList = CommaSepratedFilter.Split(',').Select(p => p.Trim()).ToList();
            }
            var getAllStatus = await _ilookupRespository.GetAllStatus();
            using (var _uow = GetConnection())
            {
                var hc_resume_bank = _uow.Query<HC_RESUME_BANK>()

                       .Where(z => (
                    ((LocationId != null) && (z.LocationID == LocationId)
                    ||
                    (LocationId == null))
                    //&&
                    // ((EducationGroupId != null)
                    //  && (z.Educations.Any(t => t.Education_Group_ID == EducationGroupId)))
                    //||
                    //(EducationGroupId == null)
                    
                    )
                 //&&
                 //     ((EducationTypeId != null)
                 //     && (z.Educations.Any(t => t.Education_Type_ID == EducationTypeId))
                 //    ||
                 //    (EducationTypeId == null))
                 //&&
                 // ((EducationMajorId != null)
                 // && (z.Educations.Any(t => t.Education_Major_ID == EducationMajorId))
                 //||
                 //(EducationMajorId == null))
                  &&
                 ((StatusId != null)
                 && (statusIdList.Contains(z.ResumeStatus))
                ||
                (StatusId == null))
                &&
                  ((EmiratesId != null)
                 && (emiratesIdList.Contains(z.NSRNumber))
                ||
                (EmiratesId == null))
                 &&
                  ((JobSeekerId != null)
                 && (jobseekerIdList.Contains(z.UniqueNo))
                ||
                (JobSeekerId == null))
                &&
                 ((CommaSepratedFilter != null)
                 &&
                 (CommaSepratedList.Contains(z.PassportNo) ||
                 CommaSepratedList.Contains(z.EmailID) ||
                 CommaSepratedList.Contains(z.FirstName) || CommaSepratedList.Contains(z.ExFirstNameAr) ||
                 CommaSepratedList.Contains(z.LastName) || CommaSepratedList.Contains(z.ExLastNameAr))
                ||
                (CommaSepratedFilter == null))
                )
                    .Select(i => new JobSeekerResumesViewModel
                {
                    Rid = i.RID,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    EmailId = i.EmailID,
                    PassportNumber = i.PassportNo,
                    MobilePhone = i.Mobile,
                    //PassportValidity = i.PassportValidity,
                    // TotalExp = i.TotalExp,
                    DOB = i.DOB,
                    Notes = i.Notes,
                    GenderId = i.Gender,
                    JobSeekerId = i.UniqueNo,
                    //LocationId = i.LocationID,
                    //CreatedUserID = i.CreatedUserID,
                    //ResumeStatus = i.ResumeStatus,
                    CreatedDate = i.CreatedDate,
                    LastUpdateDate = i.LastUpdateDate,
                    //LastModifiedUserID = i.LastModifiedUserID,
                    // CountryID = i.CountryID,
                    //   StateID = i.StateID,
                    Salutation = i.Salutation,
                    //  DesignationText = i.DesignationText,
                    //  Address1 = i.Address1,
                    //  Address2 = i.Address2,
                    //   MName = i.MName,
                    EmiratesId = i.NSRNumber,
                    //  MartialStatus = i.ExtMaritialStatus,
                    //  ExFullNameAr = i.ExFullNameAr,
                    FirstNameAr = i.ExtFirstNameAr,
                    MiddleName = i.ExSecondNameAr,
                    LastNameAr = i.ExLastNameAr,
                    //   ExPPPlaceOfIssue = i.ExPPPlaceOfIssue,
                    //   ExPPPlaceOfIssueAr = i.ExPPPlaceOfIssueAr,
                    KAQNo = i.ExKAQNo,
                    KAQPageNo = i.ExKAQPageNo,
                    //   ExGenderAr = i.ExGenderAr,
                    //  MartialStatus = i.ExMaritalStatus,
                    //  ExAddressAr = i.ExAddressAr,
                    //   PoBoxCity = i.ExPoBoxCity,
                    //   ExPoBoxCityAr = i.ExPoBoxCityAr,
                    //  ExAlternateEmail = i.ExAlternateEmail,
                    //   ThirdName = i.ExThirdName,
                    // ExtPlaceofBirth = i.ExtPlaceofBirth,
                    //  ExtTownNo = i.ExtTownNo,
                    //   ExtCardIssueDate = i.ExtCardIssueDate,
                    //   ExtCardExpiryDate = i.ExtCardExpiryDate,
                    //    ExPassport = i.ExPassport,
                    //   ExFullNameEng = i.ExFullNameEng,
                    //    ThirdName = i.ExThirdNameEng,
                    //   ExFamilyNameEng = i.ExFamilyNameEng,
                    //    ExArabicTitleAr = i.ExArabicTitleAr,
                    //    ExtTownPageNo = i.ExtTownPageNo,
                    //   ExFamilyNameAr = i.ExFamilyNameAr,
                    //   K = i.ExKAQFamilyNo,
                    //    ExtResumeStatus = i.ExtResumeStatus,
                    //   ExMaritialStatus = i.ExMaritialStatus,
                    //   ExPlaceofBirth = i.ExPlaceofBirth,
                    // Ar = i.ExFirstNameAr,
                    //   ThirdNameAr = i.ExThirdNameAr,
                    //  ExFaxNo = i.ExFaxNo,
                    //   MaritialstatusVal = i.MaritialstatusVal,
                    //   MaritalStatusID = i.MaritalStatusID,
                    //    RegionID = i.RegionID,
                    //  MilitaryServiceStatus = i.ExMilitaryServiceStatus,
                    MilitaryServiceBatch = i.ExtMilitaryServiceBatch,
                    StatusTitle = getAllStatus.data.FirstOrDefault(z => z.Rid == i.ResumeStatus+1).Title,
                    StatusTitleAr = getAllStatus.data.FirstOrDefault(z => z.Rid == i.ResumeStatus+1).TitleAr
                });

                // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
                // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
                // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
                 loadOptions.PrimaryKey = new[] { "RID" };
                 loadOptions.PaginateViaPrimaryKey = true;

                var response = await DataSourceLoader.LoadAsync(hc_resume_bank, loadOptions);
                _uow.Connection.Close();
                _uow.Dispose();
                return Json(response);

              
            }

               
        }

       


        const string RID = nameof(HC_RESUME_BANK.RID);
        const string FIRST_NAME = nameof(HC_RESUME_BANK.FirstName);
        const string LAST_NAME = nameof(HC_RESUME_BANK.LastName);
        const string EMAIL_ID = nameof(HC_RESUME_BANK.EmailID);
        const string PASSPORT_NO = nameof(HC_RESUME_BANK.PassportNo);
        const string MOBILE = nameof(HC_RESUME_BANK.Mobile);
        const string PASSPORT_VALIDITY = nameof(HC_RESUME_BANK.PassportValidity);
        const string TOTAL_EXP = nameof(HC_RESUME_BANK.TotalExp);
        const string DOB = nameof(HC_RESUME_BANK.DOB);
        const string NOTES = nameof(HC_RESUME_BANK.Notes);
        const string GENDER = nameof(HC_RESUME_BANK.Gender);
        const string UNIQUE_NO = nameof(HC_RESUME_BANK.UniqueNo);
        const string LOCATION_ID = nameof(HC_RESUME_BANK.LocationID);
        const string CREATED_USER_ID = nameof(HC_RESUME_BANK.CreatedUserID);
        const string RESUME_STATUS = nameof(HC_RESUME_BANK.ResumeStatus);
        const string CREATED_DATE = nameof(HC_RESUME_BANK.CreatedDate);
        const string LAST_UPDATE_DATE = nameof(HC_RESUME_BANK.LastUpdateDate);
        const string LAST_MODIFIED_USER_ID = nameof(HC_RESUME_BANK.LastModifiedUserID);
        const string COUNTRY_ID = nameof(HC_RESUME_BANK.CountryID);
        const string STATE_ID = nameof(HC_RESUME_BANK.StateID);
        const string SALUTATION = nameof(HC_RESUME_BANK.Salutation);
        const string DESIGNATION_TEXT = nameof(HC_RESUME_BANK.DesignationText);
        const string ADDRESS1 = nameof(HC_RESUME_BANK.Address1);
        const string ADDRESS2 = nameof(HC_RESUME_BANK.Address2);
        const string MNAME = nameof(HC_RESUME_BANK.MName);
        const string NSRNUMBER = nameof(HC_RESUME_BANK.NSRNumber);
        const string EXT_MARITIAL_STATUS = nameof(HC_RESUME_BANK.ExtMaritialStatus);
        const string EX_FULL_NAME_AR = nameof(HC_RESUME_BANK.ExFullNameAr);
        const string EXT_FIRST_NAME_AR = nameof(HC_RESUME_BANK.ExtFirstNameAr);
        const string EX_SECOND_NAME_AR = nameof(HC_RESUME_BANK.ExSecondNameAr);
        const string EX_LAST_NAME_AR = nameof(HC_RESUME_BANK.ExLastNameAr);
        const string EX_PPPLACE_OF_ISSUE = nameof(HC_RESUME_BANK.ExPPPlaceOfIssue);
        const string EX_PPPLACE_OF_ISSUE_AR = nameof(HC_RESUME_BANK.ExPPPlaceOfIssueAr);
        const string EX_KAQNO = nameof(HC_RESUME_BANK.ExKAQNo);
        const string EX_KAQPAGE_NO = nameof(HC_RESUME_BANK.ExKAQPageNo);
        const string EX_GENDER_AR = nameof(HC_RESUME_BANK.ExGenderAr);
        const string EX_MARITAL_STATUS = nameof(HC_RESUME_BANK.ExMaritalStatus);
        const string EX_ADDRESS_AR = nameof(HC_RESUME_BANK.ExAddressAr);
        const string EX_PO_BOX_CITY = nameof(HC_RESUME_BANK.ExPoBoxCity);
        const string EX_PO_BOX_CITY_AR = nameof(HC_RESUME_BANK.ExPoBoxCityAr);
        const string EX_ALTERNATE_EMAIL = nameof(HC_RESUME_BANK.ExAlternateEmail);
        const string EX_THIRD_NAME = nameof(HC_RESUME_BANK.ExThirdName);
        const string EXT_PLACEOF_BIRTH = nameof(HC_RESUME_BANK.ExtPlaceofBirth);
        const string EXT_TOWN_NO = nameof(HC_RESUME_BANK.ExtTownNo);
        const string EXT_CARD_ISSUE_DATE = nameof(HC_RESUME_BANK.ExtCardIssueDate);
        const string EXT_CARD_EXPIRY_DATE = nameof(HC_RESUME_BANK.ExtCardExpiryDate);
        const string EX_PASSPORT = nameof(HC_RESUME_BANK.ExPassport);
        const string EX_FULL_NAME_ENG = nameof(HC_RESUME_BANK.ExFullNameEng);
        const string EX_THIRD_NAME_ENG = nameof(HC_RESUME_BANK.ExThirdNameEng);
        const string EX_FAMILY_NAME_ENG = nameof(HC_RESUME_BANK.ExFamilyNameEng);
        const string EX_ARABIC_TITLE_AR = nameof(HC_RESUME_BANK.ExArabicTitleAr);
        const string EXT_TOWN_PAGE_NO = nameof(HC_RESUME_BANK.ExtTownPageNo);
        const string EX_FAMILY_NAME_AR = nameof(HC_RESUME_BANK.ExFamilyNameAr);
        const string EX_KAQFAMILY_NO = nameof(HC_RESUME_BANK.ExKAQFamilyNo);
        const string EXT_RESUME_STATUS = nameof(HC_RESUME_BANK.ExtResumeStatus);
        const string EX_MARITIAL_STATUS = nameof(HC_RESUME_BANK.ExMaritialStatus);
        const string EX_PLACEOF_BIRTH = nameof(HC_RESUME_BANK.ExPlaceofBirth);
        const string EX_FIRST_NAME_AR = nameof(HC_RESUME_BANK.ExFirstNameAr);
        const string EX_THIRD_NAME_AR = nameof(HC_RESUME_BANK.ExThirdNameAr);
        const string EX_FAX_NO = nameof(HC_RESUME_BANK.ExFaxNo);
        const string MARITIALSTATUS_VAL = nameof(HC_RESUME_BANK.MaritialstatusVal);
        const string MARITAL_STATUS_ID = nameof(HC_RESUME_BANK.MaritalStatusID);
        const string REGION_ID = nameof(HC_RESUME_BANK.RegionID);
        const string EX_MILITARY_SERVICE_STATUS = nameof(HC_RESUME_BANK.ExMilitaryServiceStatus);
        const string EXT_MILITARY_SERVICE_BATCH = nameof(HC_RESUME_BANK.ExtMilitaryServiceBatch);

     

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}