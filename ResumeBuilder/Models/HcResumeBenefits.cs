using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_Benefits")]
    public partial class HcResumeBenefits
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        public long? Resumeid { get; set; }
        public long? Benefitid { get; set; }
        [Column("createddate", TypeName = "datetime")]
        public DateTime Createddate { get; set; }
        [Column("createduserid")]
        public long? Createduserid { get; set; }
        [Column("modifieddate", TypeName = "datetime")]
        public DateTime? Modifieddate { get; set; }
        [Column("modifieduserid")]
        public long? Modifieduserid { get; set; }
        [Column("isdeleted")]
        public short? Isdeleted { get; set; }
    }
}
