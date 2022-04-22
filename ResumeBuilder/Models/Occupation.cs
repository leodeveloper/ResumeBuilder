using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("Occupation")]
    public partial class Occupation
    {
        [Key]
        [Column("RId")]
        public int Rid { get; set; }
        [Column("Resume_ID")]
        public long Resume_ID { get; set; }
        public int OccupationId { get; set; }
        public int SkillGroupId { get; set; }
        public bool IsDeleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeletedDate { get; set; }
        public int? Proficiency { get; set; }
    }
}
