using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_CERTIFICATIONS")]
    public partial class HcResumeCertifications
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        public long? Resumeid { get; set; }
        [Column("title")]
        [StringLength(200)]
        public string Title { get; set; }
        [Column("typeid")]
        public int? Typeid { get; set; }
        [Column("frommonth")]
        public int? Frommonth { get; set; }
        [Column("fromyear")]
        public int? Fromyear { get; set; }
        [Column("dohaveexpriy")]
        public bool? Dohaveexpriy { get; set; }
        [Column("tomonth")]
        public int? Tomonth { get; set; }
        [Column("toyear")]
        public int? Toyear { get; set; }
        [Column("score")]
        [StringLength(50)]
        public string Score { get; set; }
        [Column("universityid")]
        public long? Universityid { get; set; }
        [Column("instituteid")]
        public long? Instituteid { get; set; }
        [Column("countryid")]
        public long? Countryid { get; set; }
        [Column("cityid")]
        public long? Cityid { get; set; }
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
        public int? GradeId { get; set; }
        public int? CampusTypeId { get; set; }
    }
}
