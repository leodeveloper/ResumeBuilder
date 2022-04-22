using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("JobSeekerCoverLetter")]
    public partial class JobSeekerCoverLetter
    {
        [Dapper.Contrib.Extensions.Key]
        [Column("Resume_ID")]
        public long Resume_ID { get; set; }
        [Required]
        [StringLength(500)]
        public string CoverLetter { get; set; }
        [StringLength(500)]
        public string Accomplishments { get; set; }
    }
}
