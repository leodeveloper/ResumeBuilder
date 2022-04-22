using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_SKILL_OCCUPATION")]
    public partial class HcmSkillOccupation
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Column("SkillGroupID")]
        public long SkillGroupId { get; set; }
        [Column("OccupationsID")]
        public long OccupationsId { get; set; }
        [Column("CreatedUserID")]
        public long CreatedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDateTime { get; set; }
        [Column("LastUpdatedUserID")]
        public long LastUpdatedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdatedDateTime { get; set; }
    }
}
