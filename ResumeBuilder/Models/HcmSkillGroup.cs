using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_SKILL_GROUP")]
    public partial class HcmSkillGroup
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [StringLength(150)]
        public string SkillGroupTitle { get; set; }
        [Column("CreatedUserID")]
        public long CreatedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDateTime { get; set; }
        [Column("LastUpdatedUserID")]
        public long LastUpdatedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdatedDateTime { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
        public short LanguageType { get; set; }
        [Column(TypeName = "ntext")]
        public string KeyWords { get; set; }
        public short Isnonstandard { get; set; }
        [Column("iscompetences")]
        public short? Iscompetences { get; set; }
    }
}
