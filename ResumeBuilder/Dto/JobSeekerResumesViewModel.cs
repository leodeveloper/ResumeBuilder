using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Dto
{
    // If you need to use Data Annotation attributes, attach them to this view model instead of an XPO data model.
    public class JobSeekerResumesViewModel
    {
        public long Rid { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public string ThridName { get; set; }
        public string LastName { get; set; }     
        public string FirstNameAr { get; set; }
        public string MiddleNameAr { get; set; }
        public string ThridNameAr { get; set; }
        public string LastNameAr { get; set; }
        public string FamilyNameAr { get; set; }
        public string FamilyName { get; set; }
        public int Salutation { get; set; }
        public string FullName { get { return $" {FirstName} {LastName} {FamilyName}"; } }
        public string FullNameAr { get { return $" {FirstNameAr} {LastNameAr} {FamilyNameAr}"; } }
        public int GenderId { get; set; }
        public DateTime DOB { get; set; }
        public int PlaceOfBirth { get; set; }
        public int MartialStatus { get; set; }
        public string KAQNo { get; set; }
        public string FamilyNo { get; set; }
        public string TownNo { get; set; }
        public string KAQPageNo { get; set; }
        public string EmiratesId { get; set; }
        public DateTime EmiratesIdExpiryDate { get; set; }
        public string PassportNumber { get; set; }
        public int PassportPlaceOfIssue { get; set; }
        public string JobSeekerId { get; set; }
        public int Emirates { get; set; }
        public int CityId { get; set; }
        public int LocationId { get; set; }
        public string POBoxNo { get; set; }
        public int POBoxCityId { get; set; }
        public string MobilePhone { get; set; }
        public string LandLine { get; set; }
        public string EmailId { get; set; }
        public string PrimaryContact { get; set; }
        public string UnifiedNumber { get; set; }
        public string Notes { get; set; }
        public bool IsDeleted1 { get; set; }
        public string MilitaryServiceBatch { get; set; }
        public int MilitaryServiceStatus { get; set; }
        public DateTime? MilitaryServiceFromDate { get; set; }
        public DateTime? MilitaryServiceToDate { get; set; }
        public int id { get; set; }
        public int resumestatus { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public string Address { get; set; }
        public string StatusTitle { get; set; }
        public string StatusTitleAr { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
