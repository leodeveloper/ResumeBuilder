using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models
{
    public class HcResumeBankViewModel
    {
        public long RID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string PassportNo { get; set; }
        public string Mobile { get; set; }
        public DateTime PassportValidity { get; set; }
        public decimal TotalExp { get; set; }
        public DateTime DOB { get; set; }
        public string Notes { get; set; }
        public short Gender { get; set; }
        public string UniqueNo { get; set; }
        public long LocationID { get; set; }
        public long CreatedUserID { get; set; }
        public short ResumeStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public long LastModifiedUserID { get; set; }
        public long CountryID { get; set; }
        public long StateID { get; set; }
        public short Salutation { get; set; }
        public string DesignationText { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string MName { get; set; }
        public string NSRNumber { get; set; }
        public string ExtMaritialStatus { get; set; }
        public string ExFullNameAr { get; set; }
        public string ExtFirstNameAr { get; set; }
        public string ExSecondNameAr { get; set; }
        public string ExLastNameAr { get; set; }
        public string ExPPPlaceOfIssue { get; set; }
        public string ExPPPlaceOfIssueAr { get; set; }
        public string ExKAQNo { get; set; }
        public string ExKAQPageNo { get; set; }
        public string ExGenderAr { get; set; }
        public string ExMaritalStatus { get; set; }
        public string ExAddressAr { get; set; }
        public string ExPoBoxCity { get; set; }
        public string ExPoBoxCityAr { get; set; }
        public string ExAlternateEmail { get; set; }
        public string ExThirdName { get; set; }
        public string ExtPlaceofBirth { get; set; }
        public string ExtTownNo { get; set; }
        public DateTime ExtCardIssueDate { get; set; }
        public DateTime ExtCardExpiryDate { get; set; }
        public string ExPassport { get; set; }
        public string ExFullNameEng { get; set; }
        public string ExThirdNameEng { get; set; }
        public string ExFamilyNameEng { get; set; }
        public string ExArabicTitleAr { get; set; }
        public string ExtTownPageNo { get; set; }
        public string ExFamilyNameAr { get; set; }
        public string ExKAQFamilyNo { get; set; }
        public string ExtResumeStatus { get; set; }
        public string ExMaritialStatus { get; set; }
        public string ExPlaceofBirth { get; set; }
        public string ExFirstNameAr { get; set; }
        public string ExThirdNameAr { get; set; }
        public string ExFaxNo { get; set; }
        public short MaritialstatusVal { get; set; }
        public long MaritalStatusID { get; set; }
        public long RegionID { get; set; }
        public string ExMilitaryServiceStatus { get; set; }
        public string ExtMilitaryServiceBatch { get; set; }
    }
}
