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

    public partial class JobSeekerCoverLetter : XPLiteObject
    {
        long fResume_ID;
        [Key]
        public long Resume_ID
        {
            get { return fResume_ID; }
            set { SetPropertyValue<long>(nameof(Resume_ID), ref fResume_ID, value); }
        }
        string fCoverLetter;
        [Size(500)]
        public string CoverLetter
        {
            get { return fCoverLetter; }
            set { SetPropertyValue<string>(nameof(CoverLetter), ref fCoverLetter, value); }
        }
        string fAccomplishments;
        [Size(500)]
        public string Accomplishments
        {
            get { return fAccomplishments; }
            set { SetPropertyValue<string>(nameof(Accomplishments), ref fAccomplishments, value); }
        }
    }

}
