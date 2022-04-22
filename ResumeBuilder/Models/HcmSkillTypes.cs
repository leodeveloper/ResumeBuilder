using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_SKILL_TYPES")]
    public partial class HcmSkillTypes
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Required]
        [StringLength(100)]
        public string SkillType { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string SkillDescription { get; set; }
        [Column(TypeName = "ntext")]
        public string SkillAlias { get; set; }
        [Column(TypeName = "ntext")]
        public string ExcludeAlias { get; set; }
        [Column("V3JobsID")]
        public long V3jobsId { get; set; }
        public short SkillTypeIndex { get; set; }
        [Required]
        [StringLength(20)]
        public string SkillCode { get; set; }
        public short LanguageType { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        public long CreatedUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public long ModifiedUser { get; set; }
        public long SkillCategory { get; set; }
    }
}
