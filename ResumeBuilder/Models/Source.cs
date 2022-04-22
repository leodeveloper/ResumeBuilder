using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("Source")]
    public partial class Source
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Column("Resume_ID")]
        public long Resume_ID { get; set; }
        [Required]
        [StringLength(45)]
        public string SourceName { get; set; }
        [StringLength(50)]
        public string Relationship { get; set; }
        [Column("SourceType_ID")]
        public int? SourceType_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime SourceLetterDate { get; set; }

        [StringLength(2000)]
        public string Reference_Type { get; set; }
        public bool IsPriority { get; set; }
        public string CreatedUserName { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string UpdateUserName { get; set; }
        //[ForeignKey(nameof(Resume_ID))]
        //[InverseProperty(nameof(JobSeekerResume.Source))]
        //public virtual JobSeekerResume Resume { get; set; }
    }
}
