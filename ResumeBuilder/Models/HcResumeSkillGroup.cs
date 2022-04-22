using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_SKILL_GROUP")]
    public partial class HcResumeSkillGroup
    {
        [Dapper.Contrib.Extensions.Key]        
        public long Rid { get; set; }
        [Column("ResumeID")]
        public long ResumeId { get; set; }
        [Column("SkillGroupID")]
        public long SkillGroupId { get; set; }
        [Column("OccupationID")]
        public long? OccupationId { get; set; }
        [Column("levelID")]
        public short? LevelId { get; set; }
        public long? Createduserid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Createddate { get; set; }
        public long? Modifieduserid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Modifieddate { get; set; }
        public bool Is_Deleted { get; set; }
    }
}
