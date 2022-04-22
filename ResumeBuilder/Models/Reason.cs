using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("Reason")]
    public partial class Reason
    {
        [Dapper.Contrib.Extensions.Key]
        [Column("RID")]
        public long Rid { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        public string TitleAr { get; set; }
        
    }

    public class LookupReason : Reason
    {
        public int Status_ID { get; set; }
        public string FullTitle { get { return $"{Title} -- {TitleAr}"; } }
    }
}
