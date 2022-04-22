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
using ResumeBuilder.XPO.Database;
using ResumeBuilder.Dto;
using ResumeBuilder.Repository;
using Microsoft.AspNetCore.Authorization;
using ResumeBuilder.Helper;

namespace ResumeBuilder.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class JobSeekerResumesController : Controller
    {
        private IConfiguration _iConfiguration;
        readonly IlookupRespository _ilookupRespository;
        readonly IEducationRepository _iEducationRepository;
        readonly IResumeRepository _iResumeRepository;

        public JobSeekerResumesController(IConfiguration configuration, IResumeRepository iResumeRepository, IlookupRespository ilookupRespository, IEducationRepository iEducationRepository) {
            _iConfiguration = configuration;
            _ilookupRespository = ilookupRespository;
            _iEducationRepository = iEducationRepository;
            _iResumeRepository = iResumeRepository;
        }

        private UnitOfWork GetConnection()
        {
            return new UnitOfWork(ConnectionHelper.GetDataLayer(_iConfiguration, AutoCreateOption.SchemaAlreadyExists)); 
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmiratesIds(DataSourceLoadOptions loadOptions)
        {
            using (var _uow = GetConnection())
            {

                var jobseekerresumes = _uow.Query<JobSeekerResume>().Where(z => z.EmiratesId != string.Empty && z.EmiratesId != null).Select(i => new JobSeekerEmiratesIdsViewModel
                {
                    Id = i.EmiratesId,
                    Value = i.EmiratesId,
                }).Distinct();


                loadOptions.PrimaryKey = new[] { "Id" };
                loadOptions.PaginateViaPrimaryKey = true;

                var response = await DataSourceLoader.LoadAsync(jobseekerresumes, loadOptions);
                _uow.Connection.Close();
                _uow.Dispose();
                return Json(response);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJobSeekerIds(DataSourceLoadOptions loadOptions)
        {
            using (var _uow = GetConnection())
            {

                var jobseekerresumes = _uow.Query<JobSeekerResume>().Where(z=>z.JobSeekerID != string.Empty && z.JobSeekerID != null).Select(i => new JobSeekerIdsViewModel
                {
                    Id = i.JobSeekerID,
                    Value = i.JobSeekerID,
                }).Distinct();


                loadOptions.PrimaryKey = new[] { "Id" };
                loadOptions.PaginateViaPrimaryKey = true;

                var response = await DataSourceLoader.LoadAsync(jobseekerresumes, loadOptions);
                _uow.Connection.Close();
                _uow.Dispose();
                return Json(response);
            }
        }

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
                var jobseekerresumes = _uow.Query<JobSeekerResume>()
                     .Where(z => (
                    ((LocationId != null) && (z.LocationId == LocationId)
                    ||
                    (LocationId == null))
                    &&
                     ((EducationGroupId != null)
                      && (z.Educations.Any(t => t.Education_Group_ID == EducationGroupId)))
                    ||
                    (EducationGroupId == null))
                 &&
                      ((EducationTypeId != null)
                      && (z.Educations.Any(t => t.Education_Type_ID == EducationTypeId))
                     ||
                     (EducationTypeId == null))
                 &&
                  ((EducationMajorId != null)
                  && (z.Educations.Any(t => t.Education_Major_ID == EducationMajorId))
                 ||
                 (EducationMajorId == null))
                  &&
                 ((StatusId != null)
                 && (statusIdList.Contains(z.resumestatus))
                ||
                (StatusId == null))
                &&
                  ((EmiratesId != null)
                 && (emiratesIdList.Contains(z.EmiratesId))
                ||
                (EmiratesId == null))
                 &&
                  ((JobSeekerId != null)
                 && (jobseekerIdList.Contains(z.JobSeekerID))
                ||
                (JobSeekerId == null))
                &&
                 ((CommaSepratedFilter != null)
                 && 
                 (CommaSepratedList.Contains(z.PassportNumber) || 
                 CommaSepratedList.Contains(z.EmailId) || 
                 CommaSepratedList.Contains(z.FirstName) || CommaSepratedList.Contains(z.FirstNameAr) ||
                 CommaSepratedList.Contains(z.LastName) || CommaSepratedList.Contains(z.LastNameAr))
                ||
                (CommaSepratedFilter == null))              
                )
                    .Select(i => new JobSeekerResumesViewModel
                     {

                         Rid = i.RID,                       
                         FirstName = i.FirstName,
                         MiddleName = i.MiddleName,
                         ThridName = i.ThridName,
                         LastName = i.LastName,
                         FirstNameAr = i.FirstNameAr,
                         MiddleNameAr = i.MiddleNameAr,
                         ThridNameAr = i.ThridNameAr,
                         LastNameAr = i.LastNameAr,
                         FamilyNameAr = i.FamilyNameAr,
                         FamilyName = i.FamilyName,
                         Salutation = i.Salutation,
                         GenderId = i.GenderId,
                         DOB = i.DOB,
                         PlaceOfBirth = i.PlaceOfBirth,
                         MartialStatus = i.MartialStatus,
                         KAQNo = i.KAQNo,
                         FamilyNo = i.FamilyNo,
                         TownNo = i.TownNo,
                         KAQPageNo = i.KAQPageNo,
                         EmiratesId = i.EmiratesId,
                         EmiratesIdExpiryDate = i.EmiratesIdExpiryDate,
                         PassportNumber = i.PassportNumber,
                         PassportPlaceOfIssue = i.PassportPlaceOfIssue,
                         JobSeekerId = i.JobSeekerID,
                         Emirates = i.Emirates,
                         CityId = i.CityId,
                         LocationId = i.LocationId,
                         POBoxNo = i.POBoxNo,
                         POBoxCityId = i.POBoxCityId,
                         MobilePhone = i.MobilePhone,
                         LandLine = i.LandLine,
                         EmailId = i.EmailId,
                         PrimaryContact = i.PrimaryContact,
                         UnifiedNumber = i.UnifiedNumber,
                         Notes = i.Notes,
                         IsDeleted1 = i.IsDeleted1,
                         MilitaryServiceBatch = i.MilitaryServiceBatch,
                         MilitaryServiceStatus = i.MilitaryServiceStatus,
                         MilitaryServiceFromDate = i.MilitaryServiceFromDate,
                         MilitaryServiceToDate = i.MilitaryServiceToDate,
                         id = i.id,
                         resumestatus = i.resumestatus,
                         Twitter = i.Twitter,
                         Linkedin = i.Linkedin,
                         Address = i.Address,
                         StatusTitle = getAllStatus.data.FirstOrDefault(z => z.Rid == i.resumestatus).Title,
                         StatusTitleAr = getAllStatus.data.FirstOrDefault(z => z.Rid == i.resumestatus).TitleAr,
                         CreatedDate = i.CreatedDate,
                         LastUpdateDate = i.LastUpdateDate
                         
                     });

                // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
                // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
                // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
                 loadOptions.PrimaryKey = new[] { "RID" };
                 loadOptions.PaginateViaPrimaryKey = true;
                
                var response = await DataSourceLoader.LoadAsync(jobseekerresumes, loadOptions);
                _uow.Connection.Close();
                _uow.Dispose();
                return Json(response);

            }
        }

        [HttpGet]
        public async Task<IActionResult> GetJobSeekerPreview(long resumeid)
        {
            var webresponse = await _iResumeRepository.Resume_Preview(resumeid);
            if (webresponse.status == ResumeBuilder.Models.APIStatus.success)
            {
               return Json(webresponse.data);
            }
            else
            {
                return Json(null);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetJobSeekerPhoto(long resumeid)
        {
            var webresponse = await _iResumeRepository.GetJobSeekerPhoto(resumeid);
            if(webresponse.status == ResumeBuilder.Models.APIStatus.success)
            {
                return Json(webresponse.data.Base64fileContentWithContentType);
            }
            else
            {
                return Json(null);
            }            
        }       

    }
}