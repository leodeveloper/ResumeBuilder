using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Dto.HraOpsDto
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_BANK")]
    public partial class HC_RESUME_BANK
    {
        [Dapper.Contrib.Extensions.Key]
       // [Column("RID")]
        public long Rid { get; set; }
        [Required]
        [StringLength(150)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(25)]
        public string LastName { get; set; }
        [Column("EmailID")]
        [StringLength(100)]
        public string EmailId { get; set; }
        [Column("AlternateEmailID")]
        [StringLength(100)]
        public string AlternateEmailId { get; set; }
        [StringLength(10)]
        public string PassportNo { get; set; }
        [StringLength(50)]
        public string Mobile { get; set; }
        [StringLength(50)]
        public string PhoneH { get; set; }
        [StringLength(50)]
        public string Phone2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PassportValidity { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ReleventExp { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalExp { get; set; }
        [Column("DOB", TypeName = "datetime")]
        public DateTime? Dob { get; set; }
        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
        [Column(TypeName = "ntext")]
        public string HotNotes { get; set; }
        public short Gender { get; set; }
        [Column("FunctionID")]
        public long FunctionId { get; set; }
        [Column("IndTypeID")]
        public long IndTypeId { get; set; }
        [Column(TypeName = "ntext")]
        public string SkillsText { get; set; }
        [Column(TypeName = "ntext")]
        public string EducationText { get; set; }
        [Column("DesignationID")]
        public long DesignationId { get; set; }
        public int Rating { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal RatingScore { get; set; }
        [Required]
        [StringLength(25)]
        public string UniqueNo { get; set; }
        [Column("LocationID")]
        public long LocationId { get; set; }
        [Column("CreatedUserID")]
        public long CreatedUserId { get; set; }
        public short ResumeStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "ntext")]
        public string ResumeConvertedText { get; set; }
        [StringLength(100)]
        public string ImageName { get; set; }
        [Column(TypeName = "image")]
        public byte[] ImgPhoto { get; set; }
        [Column(TypeName = "ntext")]
        public string PerferLocation { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdateDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DocModifiedDate { get; set; }
        [Column(TypeName = "image")]
        public byte[] DocumentOne { get; set; }
        public long ViewCount { get; set; }
        [Column("ResumeSourceID")]
        public long ResumeSourceId { get; set; }
        [StringLength(100)]
        public string ResumeSourceRef { get; set; }
        public short Confidential { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        [Column(TypeName = "ntext")]
        public string ExperienceSkillText { get; set; }
        [Column("EmployerID")]
        public long EmployerId { get; set; }
        [Column("PresentCTC", TypeName = "decimal(18, 2)")]
        public decimal PresentCtc { get; set; }
        [Column("ExpectedCTC", TypeName = "decimal(18, 2)")]
        public decimal ExpectedCtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WorkingFrom { get; set; }
        public long PresentCurrency { get; set; }
        public long PresentScale { get; set; }
        public long ExpectedCurrency { get; set; }
        public long ExpectedScale { get; set; }
        [Column("LevelID")]
        public long LevelId { get; set; }
        [Column(TypeName = "decimal(18, 10)")]
        public decimal PresentFactor { get; set; }
        [Column(TypeName = "decimal(18, 10)")]
        public decimal ExpectedFactor { get; set; }
        [StringLength(100)]
        public string PresentEmployer { get; set; }
        [Column("LastModifiedUserID")]
        public long LastModifiedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RefreshDate { get; set; }
        [Column(TypeName = "ntext")]
        public string FunctionText { get; set; }
        [Column(TypeName = "ntext")]
        public string IndustryText { get; set; }
        [Column(TypeName = "ntext")]
        public string SubFunctionText { get; set; }
        [Column("RefID")]
        public long RefId { get; set; }
        [Column("NationalityID")]
        public long NationalityId { get; set; }
        [Column("PANNo")]
        [StringLength(20)]
        public string Panno { get; set; }
        public int NoticePeriod { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ExpCompDate { get; set; }
        [Column(TypeName = "ntext")]
        public string ConfUsers { get; set; }
        [Column("ValidatedUserID")]
        public long ValidatedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ValidatedDate { get; set; }
        public short RefreshStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AvailableAfter { get; set; }
        public int WillingToChangeJob { get; set; }
        public long PresentFrequency { get; set; }
        public long ExpectedFrequency { get; set; }
        public long ReqCount { get; set; }
        public long ReqTotalCount { get; set; }
        public long ReqOpenCount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ResPrefJoiningDate { get; set; }
        [Column(TypeName = "ntext")]
        public string ResPrefLocation { get; set; }
        [Column(TypeName = "ntext")]
        public string ResWillingToTravel { get; set; }
        [Column(TypeName = "ntext")]
        public string ThumbNail { get; set; }
        [Column(TypeName = "ntext")]
        public string AlarmNotes { get; set; }
        [Required]
        [StringLength(25)]
        public string MiddleName { get; set; }
        [Column("LeaveTemplateID")]
        public long LeaveTemplateId { get; set; }
        public short ConsultantStatus { get; set; }
        [Column("CountryID")]
        public long CountryId { get; set; }
        [Column("StateID")]
        public long StateId { get; set; }
        [Column("UserGroupID")]
        public long UserGroupId { get; set; }
        public short PdfFile { get; set; }
        [Column(TypeName = "image")]
        public byte[] PdfFileData { get; set; }
        [StringLength(250)]
        public string PdfFileName { get; set; }
        [Column("EmployeeID")]
        [StringLength(25)]
        public string EmployeeId { get; set; }
        [Column("ExJD")]
        public long? ExJd { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        [Column("V3ResID")]
        public long V3resId { get; set; }
        [Column("V3CCID")]
        public long V3ccid { get; set; }
        [Column("V3CAID")]
        public long V3caid { get; set; }
        public short QcCheck { get; set; }
        public short Salutation { get; set; }
        [Column(TypeName = "ntext")]
        public string EmployerText { get; set; }
        [Column(TypeName = "ntext")]
        public string DesignationText { get; set; }
        [Column(TypeName = "xml")]
        public string Resume4LevelFunction { get; set; }
        [Column(TypeName = "ntext")]
        public string SubIndustryText { get; set; }
        [Column(TypeName = "ntext")]
        public string VisaText { get; set; }
        [Column("QC1Status")]
        public short Qc1status { get; set; }
        [Column("QC2Status")]
        public short Qc2status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ContactedDate { get; set; }
        [Column("ContactedUserID")]
        public short ContactedUserId { get; set; }
        [StringLength(4000)]
        public string ContactedRemars { get; set; }
        [Column("CTCInfo", TypeName = "ntext")]
        public string Ctcinfo { get; set; }
        [StringLength(250)]
        public string Address1 { get; set; }
        [StringLength(250)]
        public string Address2 { get; set; }
        [Column("V3JobsStatus")]
        public short V3jobsStatus { get; set; }
        [Column("V3ImporterStatus")]
        public short V3importerStatus { get; set; }
        public long LastViewedUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastViewedDate { get; set; }
        [Column("RecruitmentCategoryID")]
        public long RecruitmentCategoryId { get; set; }
        [Required]
        [Column("EmpID")]
        [StringLength(25)]
        public string EmpId { get; set; }
        [Required]
        [Column("MName")]
        [StringLength(25)]
        public string Mname { get; set; }
        [Column("IsPrevEmpERF")]
        public short IsPrevEmpErf { get; set; }
        [Column("IsFamilyERF")]
        public short IsFamilyErf { get; set; }
        [Required]
        [StringLength(10)]
        public string PinCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceDate { get; set; }
        public int AttachmentCount { get; set; }
        [Column(TypeName = "ntext")]
        public string LinkUrl { get; set; }
        [Required]
        [StringLength(100)]
        public string BankName { get; set; }
        [Required]
        [StringLength(100)]
        public string BranchName { get; set; }
        [Required]
        [StringLength(50)]
        public string AccountNumber { get; set; }
        [Column("CurrentLocationID")]
        public long CurrentLocationId { get; set; }
        [Column("CurrentStateID")]
        public long CurrentStateId { get; set; }
        [Column("CurrentCountryID")]
        public long CurrentCountryId { get; set; }
        [Column("CompanyTypeID")]
        public short CompanyTypeId { get; set; }
        [Required]
        [Column("CompanyEmployerID")]
        [StringLength(25)]
        public string CompanyEmployerId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CompanyEmpJoined { get; set; }
        public short OffOnShore { get; set; }
        [Required]
        [StringLength(100)]
        public string OnshoreLocation { get; set; }
        [Required]
        [Column("NSRNumber")]
        [StringLength(20)]
        public string Nsrnumber { get; set; }
        public short CriteriaIndex { get; set; }
        [Column("CurrentAddressPINNo")]
        [StringLength(500)]
        public string CurrentAddressPinno { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EstimatedReleasedDate { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtRestrictions { get; set; }
        [Column("ExtICDLStatus", TypeName = "ntext")]
        public string ExtIcdlstatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExtTransferDate { get; set; }
        [StringLength(200)]
        public string ExtArabicDesignation { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtAge { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtMaritialStatus { get; set; }
        [Column(TypeName = "ntext")]
        public string Extcomp { get; set; }
        [Column("ExtNOChild", TypeName = "ntext")]
        public string ExtNochild { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtProg { get; set; }
        [Column("ExtADUID", TypeName = "ntext")]
        public string ExtAduid { get; set; }
        [Column("ExtSU", TypeName = "ntext")]
        public string ExtSu { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtUniversityid { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtStatus { get; set; }
        [Column("ACTDocAllMigration")]
        public short ActdocAllMigration { get; set; }
        [Column("ACTMigration")]
        public short Actmigration { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtActJobTitle { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtProgram { get; set; }
        [Column("ExtAssociateJD")]
        [StringLength(500)]
        public string ExtAssociateJd { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExtEmploymentDate { get; set; }
        [Column(TypeName = "ntext")]
        public string Extlevel4 { get; set; }
        [Column("ReligionID")]
        public short ReligionId { get; set; }
        [Column("SourcingChannelID")]
        public long SourcingChannelId { get; set; }
        [Column("SourcingCentreID")]
        public long SourcingCentreId { get; set; }
        [Column("ResumeEStatus")]
        public short ResumeEstatus { get; set; }
        public short SourceChannelAssing { get; set; }
        public short RegistrationMode { get; set; }
        [StringLength(50)]
        public string OfficePhExt { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal FixedSalaryAmt { get; set; }
        public long FixedSalaryCurrency { get; set; }
        public long FixedSalaryScale { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal VarSalaryAmt { get; set; }
        public long VarSalaryCurrency { get; set; }
        public long VarSalaryScale { get; set; }
        [StringLength(50)]
        public string OfficePh { get; set; }
        [Column(TypeName = "ntext")]
        public string ExFullNameAr { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtFirstNameAr { get; set; }
        [Column(TypeName = "ntext")]
        public string ExSecondNameAr { get; set; }
        [Column(TypeName = "ntext")]
        public string ExLastNameAr { get; set; }
        [Column("ExPPPlaceOfIssue", TypeName = "ntext")]
        public string ExPpplaceOfIssue { get; set; }
        [Column("ExPPPlaceOfIssueAr", TypeName = "ntext")]
        public string ExPpplaceOfIssueAr { get; set; }
        [Column("ExKAQNo", TypeName = "ntext")]
        public string ExKaqno { get; set; }
        [Column("ExKAQPageNo", TypeName = "ntext")]
        public string ExKaqpageNo { get; set; }
        [Column("ExNatioalID", TypeName = "ntext")]
        public string ExNatioalId { get; set; }
        [Column(TypeName = "ntext")]
        public string ExGenderAr { get; set; }
        [Column(TypeName = "ntext")]
        public string ExMaritalStatus { get; set; }
        [Column(TypeName = "ntext")]
        public string ExAddressAr { get; set; }
        [Column(TypeName = "ntext")]
        public string ExPoBoxCity { get; set; }
        [Column(TypeName = "ntext")]
        public string ExPoBoxCityAr { get; set; }
        [StringLength(200)]
        public string ExAlternateEmail { get; set; }
        [Column(TypeName = "ntext")]
        public string ExMobile1 { get; set; }
        [Column(TypeName = "ntext")]
        public string ExLandLine1 { get; set; }
        public string ExEducationAr { get; set; }
        [Column(TypeName = "ntext")]
        public string ExEmpStatus { get; set; }
        [Column(TypeName = "ntext")]
        public string ExEmpStatusAr { get; set; }
        [Column(TypeName = "ntext")]
        public string ExSplNeed { get; set; }
        [Column(TypeName = "ntext")]
        public string ExCreatedDate { get; set; }
        [Column("ExPortalID", TypeName = "ntext")]
        public string ExPortalId { get; set; }
        [Column(TypeName = "ntext")]
        public string ExAddressLocation { get; set; }
        [Column(TypeName = "ntext")]
        public string ExThirdName { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtPlaceofBirth { get; set; }
        [StringLength(250)]
        public string ExtDrivingLicense { get; set; }
        [Column("ExtIsCVAvailable", TypeName = "ntext")]
        public string ExtIsCvavailable { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtDrivingLicenseAr { get; set; }
        [Column("ExtKAQFamilyNo", TypeName = "ntext")]
        public string ExtKaqfamilyNo { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtTownNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExtCardIssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExtCardExpiryDate { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtObjective { get; set; }
        [Column(TypeName = "ntext")]
        public string ExDisability { get; set; }
        [Column(TypeName = "ntext")]
        public string ExDisabilityAr { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtMobile2 { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtKeySkillsAr { get; set; }
        [Column("document", TypeName = "image")]
        public byte[] Document { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtLetterNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExtLetterDate { get; set; }
        [Column("CVERIFICATION")]
        public short Cverification { get; set; }
        //[Column("PIF_Flag")]
        //public short PifFlag { get; set; }
        [Column(TypeName = "ntext")]
        public string ExPassport { get; set; }
        [Column("ATTMigration")]
        public short Attmigration { get; set; }
        [Column("NCPartnerID")]
        public long NcpartnerId { get; set; }
        [Column("CVCompleteFlag")]
        [StringLength(50)]
        public string CvcompleteFlag { get; set; }
        [StringLength(10)]
        public string ExSecretQ { get; set; }
        [StringLength(10)]
        public string ExSecretAns { get; set; }
        [Column(TypeName = "ntext")]
        public string ExCardIssueDate { get; set; }
        [Column(TypeName = "ntext")]
        public string ExCardExpiryDate { get; set; }
        [Column(TypeName = "ntext")]
        public string ExFullNameEng { get; set; }
        [Column(TypeName = "ntext")]
        public string ExThirdNameEng { get; set; }
        [Column(TypeName = "ntext")]
        public string ExFamilyNameEng { get; set; }
        [Column(TypeName = "ntext")]
        public string ExArabicTitleAr { get; set; }
        [Column(TypeName = "ntext")]
        public string ExAgeMin { get; set; }
        [Column("ExAForceMilitryNo", TypeName = "ntext")]
        public string ExAforceMilitryNo { get; set; }
        [Column(TypeName = "ntext")]
        public string ExWorkPolice { get; set; }
        [Column(TypeName = "ntext")]
        public string ExPloiceMilitryNo { get; set; }
        [Column(TypeName = "ntext")]
        public string ExOccupation { get; set; }
        [Column("ExWorkAForce", TypeName = "ntext")]
        public string ExWorkAforce { get; set; }
        [Column(TypeName = "ntext")]
        public string ExSpecialNeedOption { get; set; }
        [Column(TypeName = "ntext")]
        public string ExSpecialNotes { get; set; }
        [Column(TypeName = "ntext")]
        public string ExMonthlySalaryFrom { get; set; }
        [Column(TypeName = "ntext")]
        public string ExMonthlySalaryTo { get; set; }
        [Column(TypeName = "ntext")]
        public string ExDrivingLicenseStatus { get; set; }
        [Column(TypeName = "ntext")]
        public string ExDrivingLicenceText { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtTownPageNo { get; set; }
        [Column(TypeName = "ntext")]
        public string ExFamilyNameAr { get; set; }
        [Column("ExKAQFamilyNo", TypeName = "ntext")]
        public string ExKaqfamilyNo { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtPensionEmpStatus { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtPensionRecordAvailabilty { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtPensionEmployerName { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtPensionVerificationDate { get; set; }
        [Column("ExtDOFResult", TypeName = "ntext")]
        public string ExtDofresult { get; set; }
        [Column("ExtDOFVerificationDate", TypeName = "ntext")]
        public string ExtDofverificationDate { get; set; }
        [StringLength(100)]
        public string ExtResumeStatus { get; set; }
        [StringLength(100)]
        public string ExEmpResponse { get; set; }
        [Column(TypeName = "ntext")]
        public string ExTownNo { get; set; }
        [Column(TypeName = "ntext")]
        public string ExMaritialStatus { get; set; }
        [Column(TypeName = "ntext")]
        public string ExPlaceofBirth { get; set; }
        [Column(TypeName = "ntext")]
        public string ExFirstNameAr { get; set; }
        [Column(TypeName = "ntext")]
        public string ExThirdNameAr { get; set; }
        [Column(TypeName = "ntext")]
        public string ExAge { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtPensionDataAccuracy { get; set; }
        [Column(TypeName = "ntext")]
        public string ExPrimaryContact { get; set; }
        [StringLength(200)]
        public string ExFaxNo { get; set; }
        [Column(TypeName = "ntext")]
        public string ExAddress3 { get; set; }
        public short MaritialstatusVal { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExtPortalRegistrationDate { get; set; }
        [Column("tempAttach")]
        [StringLength(15)]
        public string TempAttach { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string ExtIsPartTimeStudent { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtSponsorAmount { get; set; }
        [Column(TypeName = "ntext")]
        public string ExtSponsorName { get; set; }
        [Column("ExtADNOCNCR", TypeName = "ntext")]
        public string ExtAdnocncr { get; set; }
        [Column("ActivatedUserID")]
        public long ActivatedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActivatedDate { get; set; }
        public short WorkShop { get; set; }
        public short Assessment { get; set; }
        public short InterviewConducted { get; set; }
        public short WorkShopStatus { get; set; }
        public short AssessmentStatus { get; set; }
        public short InterviewConductedStatus { get; set; }
        [Column("ExtHCTReturnValue", TypeName = "ntext")]
        public string ExtHctreturnValue { get; set; }
        [Column("ExtHCTStatus", TypeName = "ntext")]
        public string ExtHctstatus { get; set; }
        [Column("ExtHCTBatchType", TypeName = "ntext")]
        public string ExtHctbatchType { get; set; }
        [Column("ExtHCTRegistrationDate", TypeName = "ntext")]
        public string ExtHctregistrationDate { get; set; }
        [Column("ExtHCTVerificationDate", TypeName = "ntext")]
        public string ExtHctverificationDate { get; set; }
        [Column("ExtMOFResult", TypeName = "ntext")]
        public string ExtMofresult { get; set; }
        [Column("ExtMOFEmployer", TypeName = "ntext")]
        public string ExtMofemployer { get; set; }
        [Column("ExtMOFVerificationDate", TypeName = "ntext")]
        public string ExtMofverificationDate { get; set; }
        [Column("TargetDesignationID")]
        public long TargetDesignationId { get; set; }
        [Required]
        [StringLength(300)]
        public string FatherName { get; set; }
        public short SourceReparser { get; set; }
        [Column("SourceGroupID")]
        public long SourceGroupId { get; set; }
        [Column("MaritalStatusID")]
        public long MaritalStatusId { get; set; }
        //[Column("CREDITCHECK_RESULT")]
        //public short CreditcheckResult { get; set; }
        //[Column("CREDITCHECK_INTEGRATIONSTATUS")]
        //public short CreditcheckIntegrationstatus { get; set; }
        //[Column("CreditCheck_LastModifiedUser")]
        //public long CreditCheckLastModifiedUser { get; set; }
        //[Column("CreditCheck_LastModifiedDate", TypeName = "datetime")]
        //public DateTime? CreditCheckLastModifiedDate { get; set; }
        [Column("IdentificationCountryID")]
        public long IdentificationCountryId { get; set; }
        [Required]
        [StringLength(20)]
        public string IdentificationNumber { get; set; }
        [Column("RegionID")]
        public long RegionId { get; set; }
        [Column("VendorCloudEmpTypeID")]
        public long VendorCloudEmpTypeId { get; set; }
        [Column("VCResumeID")]
        public long VcresumeId { get; set; }
        public long HiredByClient { get; set; }
        public short EmpType { get; set; }
        public short QuitType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JoinedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastWorkingDay { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ConfirmationDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ResignedDate { get; set; }
        [Column("GradeID")]
        public long GradeId { get; set; }
        public short LastModifiedUserType { get; set; }
        [Column(TypeName = "ntext")]
        public string ExIsAllowToEdit { get; set; }
        [Column("eRequest")]
        public short ERequest { get; set; }
        public string ExJobPreference { get; set; }
        public string ExUniversity { get; set; }
        public string ExJobType { get; set; }
        public string ExRetiredStatus { get; set; }
        [Column("eRequestStatus")]
        public string ERequestStatus { get; set; }
        [Column("StatusReasonID")]
        public long StatusReasonId { get; set; }
        public short IsPasswordEncrpted { get; set; }
        public long CleansingCycle { get; set; }
        [Column("CleansingSMS")]
        public short CleansingSms { get; set; }
        public short CleansingEmail { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExtPassportExpiry { get; set; }
        public short EmailSentflag { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EmailSentDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EmailConfirmedDate { get; set; }
        public long LetterSourceRank { get; set; }
        public long EndorsementLetterDateRank { get; set; }
        public long RegistrationDateRank { get; set; }
        public long RegionRank { get; set; }
        public long LastLoginRank { get; set; }
        public long AcitivityPeriodRank { get; set; }
        public long AgeRank { get; set; }
        public long EducationRank { get; set; }
        public long OfferRejectionsRank { get; set; }
        public long ResignationsRank { get; set; }
        public long TotalRank { get; set; }
        public string ExtSocialProfile { get; set; }
        public string ExtSpecificqualification { get; set; }
        public string ExtSkillDetailed { get; set; }
        public string ExtAccessibility { get; set; }
        public string ExtWorkplacemodification { get; set; }
        public string ExtOrientation { get; set; }
        [Column("ExtSpeciallyabledIT")]
        public string ExtSpeciallyabledIt { get; set; }
        public string ExtSignLanguage { get; set; }
        public string ExtSpeciallyOthers { get; set; }
        public string ExtWillingToTrained { get; set; }
        public string ExtSpeciallyabledworkingHrs { get; set; }
        public string ExtSpecificTransport { get; set; }
        public string ExtInterruption { get; set; }
        public string ExBeneficiaryStatus { get; set; }
        public string ExBeneficiaryAmount { get; set; }
        public string ExBeneficiaryCategory { get; set; }
        [Column("ExMSAVerficiationDate", TypeName = "datetime")]
        public DateTime? ExMsaverficiationDate { get; set; }
        public string ExAppNum { get; set; }
        public short? PushedToTanmia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PushedToTanmiaOn { get; set; }
        public string TanmiaIntgRespSts { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExtDateofDeath { get; set; }
        [StringLength(50)]
        public string DeathDataUpdatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastOfferRejectionDateRank { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastResignationdaterank { get; set; }
        [Column("ExGPSSAEmpName")]
        public string ExGpssaempName { get; set; }
        [Column("ExGPSSAStatus")]
        public string ExGpssastatus { get; set; }
        [Column("ExGPSSACriteria")]
        public string ExGpssacriteria { get; set; }
        [Column("ExGPSSACode")]
        public string ExGpssacode { get; set; }
        [Column("ExGPSSAVerificationDate")]
        public string ExGpssaverificationDate { get; set; }
        [Column("ExMSACaseID")]
        public string ExMsacaseId { get; set; }
        [Column("ExMSARecordAvailability")]
        public string ExMsarecordAvailability { get; set; }
        [Column("ExDEDLicenseCount")]
        public string ExDedlicenseCount { get; set; }
        [Column("ExDEDLicenseNumber")]
        public string ExDedlicenseNumber { get; set; }
        [Column("ExDEDVerificationDate", TypeName = "datetime")]
        public DateTime? ExDedverificationDate { get; set; }
        public string ExtPensionReason { get; set; }
        [Required]
        public string ExtMilitaryService { get; set; }
        public string ExtMilitaryServiceBatch { get; set; }
        public string ExMilitaryServiceStatus { get; set; }
        public string ExtTrainingConfirmation { get; set; }
        [Column("iscvcompletion")]
        public bool? Iscvcompletion { get; set; }
        [Column("iscvsearchable")]
        public bool? Iscvsearchable { get; set; }
        [Column("isspeciallyabled")]
        public bool? Isspeciallyabled { get; set; }

        public int JPCAssessment { get; set; }
        public string JPCAssessmentstatus { get; set; }

        public int JPCAssessmentStatusID { get; set; }

        public string ChallangesNotes { get; set; }

        public long IsMilitaryCompleted { get; set; }
        public DateTime? LastResumeStatusUpdate { get; set; }
        public bool IsSSABeneficiary { get; set; }
    }
}
