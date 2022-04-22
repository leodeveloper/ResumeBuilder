using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_SKILL_TYPE")]
    public partial class HcResumeSkillType
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Column("SkillID")]
        public long SkillId { get; set; }
        [Column("ResumeID")]
        public long ResumeId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Experience { get; set; }
        public short? Status { get; set; }
        public short SkillType { get; set; }
        [Column("SkillCategoryID")]
        public short SkillCategoryId { get; set; }
        public int? YearLastUsed { get; set; }
        public int? YearAcquired { get; set; }
        public short SkillOccurance { get; set; }
        [Required]
        [StringLength(100)]
        public string SkillText { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Createddate { get; set; }
        [Column("createduserid")]
        public long? Createduserid { get; set; }
        public long? Modifieduserid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Modifieddate { get; set; }
    }
}
