using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace ResumeBuilder.XPOHireCraft.Database
{

    public partial class HC_RESUME_BANK
    {
        public HC_RESUME_BANK(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
