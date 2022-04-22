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
namespace ResumeBuilder.XPO.Database
{

    public partial class Occupation : XPLiteObject
    {
        int fRId;
        [Key(true)]
        public int RId
        {
            get { return fRId; }
            set { SetPropertyValue<int>(nameof(RId), ref fRId, value); }
        }
        JobSeekerResume fResume_ID;
        [Indexed(@"OccupationId", Name = @"UQ_Occupation_Resume_ID_OccupationId", Unique = true)]
        [Association(@"OccupationReferencesJobSeekerResume")]
        public JobSeekerResume Resume_ID
        {
            get { return fResume_ID; }
            set { SetPropertyValue<JobSeekerResume>(nameof(Resume_ID), ref fResume_ID, value); }
        }
        int fOccupationId;
        public int OccupationId
        {
            get { return fOccupationId; }
            set { SetPropertyValue<int>(nameof(OccupationId), ref fOccupationId, value); }
        }
        int fSkillGroupId;
        public int SkillGroupId
        {
            get { return fSkillGroupId; }
            set { SetPropertyValue<int>(nameof(SkillGroupId), ref fSkillGroupId, value); }
        }
        bool fIsDeleted1;
        [Persistent(@"IsDeleted")]
        [ColumnDbDefaultValue("((0))")]
        public bool IsDeleted1
        {
            get { return fIsDeleted1; }
            set { SetPropertyValue<bool>(nameof(IsDeleted1), ref fIsDeleted1, value); }
        }
        DateTime fDeletedDate;
        [ColumnDbDefaultValue("(getdate())")]
        public DateTime DeletedDate
        {
            get { return fDeletedDate; }
            set { SetPropertyValue<DateTime>(nameof(DeletedDate), ref fDeletedDate, value); }
        }
        int fProficiency;
        public int Proficiency
        {
            get { return fProficiency; }
            set { SetPropertyValue<int>(nameof(Proficiency), ref fProficiency, value); }
        }
    }

}
