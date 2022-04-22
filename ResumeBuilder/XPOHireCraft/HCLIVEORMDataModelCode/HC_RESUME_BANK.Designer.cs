﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace ResumeBuilder.XPOHireCraft.Database
{

    [Indices(@"Gender;RID;ResumeStatus;DOB", "RID;UniqueNo;NSRNumber")]
    public partial class HC_RESUME_BANK : XPLiteObject
    {
        long fRID;
        [Indexed(Name = @"_dta_index_HC_RESUME_BANK_7_1602417078__K1D_K129_K223_K39")]
        [Key(true)]
        public long RID
        {
            get { return fRID; }
            set { SetPropertyValue<long>(nameof(RID), ref fRID, value); }
        }
        string fFirstName;
        [Size(150)]
        [ColumnDbDefaultValue("('')")]
        public string FirstName
        {
            get { return fFirstName; }
            set { SetPropertyValue<string>(nameof(FirstName), ref fFirstName, value); }
        }
        string fLastName;
        [Size(25)]
        [ColumnDbDefaultValue("('')")]
        public string LastName
        {
            get { return fLastName; }
            set { SetPropertyValue<string>(nameof(LastName), ref fLastName, value); }
        }
        string fEmailID;
        public string EmailID
        {
            get { return fEmailID; }
            set { SetPropertyValue<string>(nameof(EmailID), ref fEmailID, value); }
        }
        string fPassportNo;
        [Size(10)]
        public string PassportNo
        {
            get { return fPassportNo; }
            set { SetPropertyValue<string>(nameof(PassportNo), ref fPassportNo, value); }
        }
        string fMobile;
        [Size(50)]
        [ColumnDbDefaultValue("('')")]
        public string Mobile
        {
            get { return fMobile; }
            set { SetPropertyValue<string>(nameof(Mobile), ref fMobile, value); }
        }
        DateTime fPassportValidity;
        [ColumnDbDefaultValue("(getutcdate())")]
        public DateTime PassportValidity
        {
            get { return fPassportValidity; }
            set { SetPropertyValue<DateTime>(nameof(PassportValidity), ref fPassportValidity, value); }
        }
        decimal fTotalExp;
        [ColumnDbDefaultValue("((0.0))")]
        public decimal TotalExp
        {
            get { return fTotalExp; }
            set { SetPropertyValue<decimal>(nameof(TotalExp), ref fTotalExp, value); }
        }
        DateTime fDOB;
        [ColumnDbDefaultValue("(getutcdate())")]
        public DateTime DOB
        {
            get { return fDOB; }
            set { SetPropertyValue<DateTime>(nameof(DOB), ref fDOB, value); }
        }
        string fNotes;
        [Size(SizeAttribute.Unlimited)]
        public string Notes
        {
            get { return fNotes; }
            set { SetPropertyValue<string>(nameof(Notes), ref fNotes, value); }
        }
        short fGender;
        [Indexed(Name = @"IN_HC_RES_BAN_Gender")]
        [ColumnDbDefaultValue("((0))")]
        public short Gender
        {
            get { return fGender; }
            set { SetPropertyValue<short>(nameof(Gender), ref fGender, value); }
        }
        string fUniqueNo;
        [Size(25)]
        [ColumnDbDefaultValue("('')")]
        public string UniqueNo
        {
            get { return fUniqueNo; }
            set { SetPropertyValue<string>(nameof(UniqueNo), ref fUniqueNo, value); }
        }
        long fLocationID;
        [Indexed(Name = @"IN_HC_RES_BAN_LocationID")]
        [ColumnDbDefaultValue("((0))")]
        public long LocationID
        {
            get { return fLocationID; }
            set { SetPropertyValue<long>(nameof(LocationID), ref fLocationID, value); }
        }
        long fCreatedUserID;
        [Indexed(Name = @"IN_HC_RES_BAN_CreatedUserID")]
        [ColumnDbDefaultValue("((0))")]
        public long CreatedUserID
        {
            get { return fCreatedUserID; }
            set { SetPropertyValue<long>(nameof(CreatedUserID), ref fCreatedUserID, value); }
        }
        short fResumeStatus;
        [Indexed(Name = @"IN_HC_RES_BAN_ResumeStatus")]
        [ColumnDbDefaultValue("((0))")]
        public short ResumeStatus
        {
            get { return fResumeStatus; }
            set { SetPropertyValue<short>(nameof(ResumeStatus), ref fResumeStatus, value); }
        }
        DateTime fCreatedDate;
        [ColumnDbDefaultValue("(getutcdate())")]
        public DateTime CreatedDate
        {
            get { return fCreatedDate; }
            set { SetPropertyValue<DateTime>(nameof(CreatedDate), ref fCreatedDate, value); }
        }
        DateTime fLastUpdateDate;
        [ColumnDbDefaultValue("(getutcdate())")]
        public DateTime LastUpdateDate
        {
            get { return fLastUpdateDate; }
            set { SetPropertyValue<DateTime>(nameof(LastUpdateDate), ref fLastUpdateDate, value); }
        }
        long fLastModifiedUserID;
        [Indexed(Name = @"IN_HC_RES_BAN_LastModifiedUserID")]
        [ColumnDbDefaultValue("((0))")]
        public long LastModifiedUserID
        {
            get { return fLastModifiedUserID; }
            set { SetPropertyValue<long>(nameof(LastModifiedUserID), ref fLastModifiedUserID, value); }
        }
        long fCountryID;
        [Indexed(Name = @"IN_HC_RES_BAN_CountryID")]
        [ColumnDbDefaultValue("((0))")]
        public long CountryID
        {
            get { return fCountryID; }
            set { SetPropertyValue<long>(nameof(CountryID), ref fCountryID, value); }
        }
        long fStateID;
        [Indexed(Name = @"IN_HC_RES_BAN_StateID")]
        [ColumnDbDefaultValue("((0))")]
        public long StateID
        {
            get { return fStateID; }
            set { SetPropertyValue<long>(nameof(StateID), ref fStateID, value); }
        }
        short fSalutation;
        [Indexed(Name = @"IN_HC_RES_BAN_Salutation")]
        [ColumnDbDefaultValue("((-1))")]
        public short Salutation
        {
            get { return fSalutation; }
            set { SetPropertyValue<short>(nameof(Salutation), ref fSalutation, value); }
        }
        string fDesignationText;
        [Size(SizeAttribute.Unlimited)]
        public string DesignationText
        {
            get { return fDesignationText; }
            set { SetPropertyValue<string>(nameof(DesignationText), ref fDesignationText, value); }
        }
        string fAddress1;
        [Size(250)]
        public string Address1
        {
            get { return fAddress1; }
            set { SetPropertyValue<string>(nameof(Address1), ref fAddress1, value); }
        }
        string fAddress2;
        [Size(250)]
        public string Address2
        {
            get { return fAddress2; }
            set { SetPropertyValue<string>(nameof(Address2), ref fAddress2, value); }
        }
        string fMName;
        [Size(25)]
        [ColumnDbDefaultValue("('')")]
        public string MName
        {
            get { return fMName; }
            set { SetPropertyValue<string>(nameof(MName), ref fMName, value); }
        }
        string fNSRNumber;
        [Indexed(Name = @"IN_HC_RESUME_BANK_NSRNumber>")]
        [Size(20)]
        [ColumnDbDefaultValue("('')")]
        public string NSRNumber
        {
            get { return fNSRNumber; }
            set { SetPropertyValue<string>(nameof(NSRNumber), ref fNSRNumber, value); }
        }
        string fExtMaritialStatus;
        [Size(SizeAttribute.Unlimited)]
        public string ExtMaritialStatus
        {
            get { return fExtMaritialStatus; }
            set { SetPropertyValue<string>(nameof(ExtMaritialStatus), ref fExtMaritialStatus, value); }
        }
        string fExFullNameAr;
        [Size(SizeAttribute.Unlimited)]
        public string ExFullNameAr
        {
            get { return fExFullNameAr; }
            set { SetPropertyValue<string>(nameof(ExFullNameAr), ref fExFullNameAr, value); }
        }
        string fExtFirstNameAr;
        [Size(SizeAttribute.Unlimited)]
        public string ExtFirstNameAr
        {
            get { return fExtFirstNameAr; }
            set { SetPropertyValue<string>(nameof(ExtFirstNameAr), ref fExtFirstNameAr, value); }
        }
        string fExSecondNameAr;
        [Size(SizeAttribute.Unlimited)]
        public string ExSecondNameAr
        {
            get { return fExSecondNameAr; }
            set { SetPropertyValue<string>(nameof(ExSecondNameAr), ref fExSecondNameAr, value); }
        }
        string fExLastNameAr;
        [Size(SizeAttribute.Unlimited)]
        public string ExLastNameAr
        {
            get { return fExLastNameAr; }
            set { SetPropertyValue<string>(nameof(ExLastNameAr), ref fExLastNameAr, value); }
        }
        string fExPPPlaceOfIssue;
        [Size(SizeAttribute.Unlimited)]
        public string ExPPPlaceOfIssue
        {
            get { return fExPPPlaceOfIssue; }
            set { SetPropertyValue<string>(nameof(ExPPPlaceOfIssue), ref fExPPPlaceOfIssue, value); }
        }
        string fExPPPlaceOfIssueAr;
        [Size(SizeAttribute.Unlimited)]
        public string ExPPPlaceOfIssueAr
        {
            get { return fExPPPlaceOfIssueAr; }
            set { SetPropertyValue<string>(nameof(ExPPPlaceOfIssueAr), ref fExPPPlaceOfIssueAr, value); }
        }
        string fExKAQNo;
        [Size(SizeAttribute.Unlimited)]
        public string ExKAQNo
        {
            get { return fExKAQNo; }
            set { SetPropertyValue<string>(nameof(ExKAQNo), ref fExKAQNo, value); }
        }
        string fExKAQPageNo;
        [Size(SizeAttribute.Unlimited)]
        public string ExKAQPageNo
        {
            get { return fExKAQPageNo; }
            set { SetPropertyValue<string>(nameof(ExKAQPageNo), ref fExKAQPageNo, value); }
        }
        string fExGenderAr;
        [Size(SizeAttribute.Unlimited)]
        public string ExGenderAr
        {
            get { return fExGenderAr; }
            set { SetPropertyValue<string>(nameof(ExGenderAr), ref fExGenderAr, value); }
        }
        string fExMaritalStatus;
        [Size(SizeAttribute.Unlimited)]
        public string ExMaritalStatus
        {
            get { return fExMaritalStatus; }
            set { SetPropertyValue<string>(nameof(ExMaritalStatus), ref fExMaritalStatus, value); }
        }
        string fExAddressAr;
        [Size(SizeAttribute.Unlimited)]
        public string ExAddressAr
        {
            get { return fExAddressAr; }
            set { SetPropertyValue<string>(nameof(ExAddressAr), ref fExAddressAr, value); }
        }
        string fExPoBoxCity;
        [Size(SizeAttribute.Unlimited)]
        public string ExPoBoxCity
        {
            get { return fExPoBoxCity; }
            set { SetPropertyValue<string>(nameof(ExPoBoxCity), ref fExPoBoxCity, value); }
        }
        string fExPoBoxCityAr;
        [Size(SizeAttribute.Unlimited)]
        public string ExPoBoxCityAr
        {
            get { return fExPoBoxCityAr; }
            set { SetPropertyValue<string>(nameof(ExPoBoxCityAr), ref fExPoBoxCityAr, value); }
        }
        string fExAlternateEmail;
        [Size(200)]
        public string ExAlternateEmail
        {
            get { return fExAlternateEmail; }
            set { SetPropertyValue<string>(nameof(ExAlternateEmail), ref fExAlternateEmail, value); }
        }
        string fExThirdName;
        [Size(SizeAttribute.Unlimited)]
        public string ExThirdName
        {
            get { return fExThirdName; }
            set { SetPropertyValue<string>(nameof(ExThirdName), ref fExThirdName, value); }
        }
        string fExtPlaceofBirth;
        [Size(SizeAttribute.Unlimited)]
        public string ExtPlaceofBirth
        {
            get { return fExtPlaceofBirth; }
            set { SetPropertyValue<string>(nameof(ExtPlaceofBirth), ref fExtPlaceofBirth, value); }
        }
        string fExtTownNo;
        [Size(SizeAttribute.Unlimited)]
        public string ExtTownNo
        {
            get { return fExtTownNo; }
            set { SetPropertyValue<string>(nameof(ExtTownNo), ref fExtTownNo, value); }
        }
        DateTime fExtCardIssueDate;
        public DateTime ExtCardIssueDate
        {
            get { return fExtCardIssueDate; }
            set { SetPropertyValue<DateTime>(nameof(ExtCardIssueDate), ref fExtCardIssueDate, value); }
        }
        DateTime fExtCardExpiryDate;
        public DateTime ExtCardExpiryDate
        {
            get { return fExtCardExpiryDate; }
            set { SetPropertyValue<DateTime>(nameof(ExtCardExpiryDate), ref fExtCardExpiryDate, value); }
        }
        string fExPassport;
        [Size(SizeAttribute.Unlimited)]
        public string ExPassport
        {
            get { return fExPassport; }
            set { SetPropertyValue<string>(nameof(ExPassport), ref fExPassport, value); }
        }
        string fExFullNameEng;
        [Size(SizeAttribute.Unlimited)]
        public string ExFullNameEng
        {
            get { return fExFullNameEng; }
            set { SetPropertyValue<string>(nameof(ExFullNameEng), ref fExFullNameEng, value); }
        }
        string fExThirdNameEng;
        [Size(SizeAttribute.Unlimited)]
        public string ExThirdNameEng
        {
            get { return fExThirdNameEng; }
            set { SetPropertyValue<string>(nameof(ExThirdNameEng), ref fExThirdNameEng, value); }
        }
        string fExFamilyNameEng;
        [Size(SizeAttribute.Unlimited)]
        public string ExFamilyNameEng
        {
            get { return fExFamilyNameEng; }
            set { SetPropertyValue<string>(nameof(ExFamilyNameEng), ref fExFamilyNameEng, value); }
        }
        string fExArabicTitleAr;
        [Size(SizeAttribute.Unlimited)]
        public string ExArabicTitleAr
        {
            get { return fExArabicTitleAr; }
            set { SetPropertyValue<string>(nameof(ExArabicTitleAr), ref fExArabicTitleAr, value); }
        }
        string fExtTownPageNo;
        [Size(SizeAttribute.Unlimited)]
        public string ExtTownPageNo
        {
            get { return fExtTownPageNo; }
            set { SetPropertyValue<string>(nameof(ExtTownPageNo), ref fExtTownPageNo, value); }
        }
        string fExFamilyNameAr;
        [Size(SizeAttribute.Unlimited)]
        public string ExFamilyNameAr
        {
            get { return fExFamilyNameAr; }
            set { SetPropertyValue<string>(nameof(ExFamilyNameAr), ref fExFamilyNameAr, value); }
        }
        string fExKAQFamilyNo;
        [Size(SizeAttribute.Unlimited)]
        public string ExKAQFamilyNo
        {
            get { return fExKAQFamilyNo; }
            set { SetPropertyValue<string>(nameof(ExKAQFamilyNo), ref fExKAQFamilyNo, value); }
        }
        string fExtResumeStatus;
        public string ExtResumeStatus
        {
            get { return fExtResumeStatus; }
            set { SetPropertyValue<string>(nameof(ExtResumeStatus), ref fExtResumeStatus, value); }
        }
        string fExMaritialStatus;
        [Size(SizeAttribute.Unlimited)]
        public string ExMaritialStatus
        {
            get { return fExMaritialStatus; }
            set { SetPropertyValue<string>(nameof(ExMaritialStatus), ref fExMaritialStatus, value); }
        }
        string fExPlaceofBirth;
        [Size(SizeAttribute.Unlimited)]
        public string ExPlaceofBirth
        {
            get { return fExPlaceofBirth; }
            set { SetPropertyValue<string>(nameof(ExPlaceofBirth), ref fExPlaceofBirth, value); }
        }
        string fExFirstNameAr;
        [Size(SizeAttribute.Unlimited)]
        public string ExFirstNameAr
        {
            get { return fExFirstNameAr; }
            set { SetPropertyValue<string>(nameof(ExFirstNameAr), ref fExFirstNameAr, value); }
        }
        string fExThirdNameAr;
        [Size(SizeAttribute.Unlimited)]
        public string ExThirdNameAr
        {
            get { return fExThirdNameAr; }
            set { SetPropertyValue<string>(nameof(ExThirdNameAr), ref fExThirdNameAr, value); }
        }
        string fExFaxNo;
        [Size(200)]
        public string ExFaxNo
        {
            get { return fExFaxNo; }
            set { SetPropertyValue<string>(nameof(ExFaxNo), ref fExFaxNo, value); }
        }
        short fMaritialstatusVal;
        [ColumnDbDefaultValue("((0))")]
        public short MaritialstatusVal
        {
            get { return fMaritialstatusVal; }
            set { SetPropertyValue<short>(nameof(MaritialstatusVal), ref fMaritialstatusVal, value); }
        }
        long fMaritalStatusID;
        [ColumnDbDefaultValue("((0))")]
        public long MaritalStatusID
        {
            get { return fMaritalStatusID; }
            set { SetPropertyValue<long>(nameof(MaritalStatusID), ref fMaritalStatusID, value); }
        }
        long fRegionID;
        [ColumnDbDefaultValue("((0))")]
        public long RegionID
        {
            get { return fRegionID; }
            set { SetPropertyValue<long>(nameof(RegionID), ref fRegionID, value); }
        }
        string fExMilitaryServiceStatus;
        [Size(SizeAttribute.Unlimited)]
        [ColumnDbDefaultValue("('Not Joined')")]
        public string ExMilitaryServiceStatus
        {
            get { return fExMilitaryServiceStatus; }
            set { SetPropertyValue<string>(nameof(ExMilitaryServiceStatus), ref fExMilitaryServiceStatus, value); }
        }
        string fExtMilitaryServiceBatch;
        [Size(SizeAttribute.Unlimited)]
        public string ExtMilitaryServiceBatch
        {
            get { return fExtMilitaryServiceBatch; }
            set { SetPropertyValue<string>(nameof(ExtMilitaryServiceBatch), ref fExtMilitaryServiceBatch, value); }
        }
    }

}
