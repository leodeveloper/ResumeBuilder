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

    public partial class Reason : XPLiteObject
    {
        long fRID;
        [Key]
        public long RID
        {
            get { return fRID; }
            set { SetPropertyValue<long>(nameof(RID), ref fRID, value); }
        }
        string fTitle;
        public string Title
        {
            get { return fTitle; }
            set { SetPropertyValue<string>(nameof(Title), ref fTitle, value); }
        }
        string fTitleAr;
        public string TitleAr
        {
            get { return fTitleAr; }
            set { SetPropertyValue<string>(nameof(TitleAr), ref fTitleAr, value); }
        }
    }

}
