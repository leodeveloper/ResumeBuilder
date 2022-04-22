using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace ResumeBuilder.XPO.Database
{

    public partial class Pensionfund
    {
        public Pensionfund(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
