using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Table("JobSeekerCVPhoto")]
    public partial class JobSeekerCvphoto
    {
        [Key]
        [Column("RID")]
        public long Rid { get; set; }
        [Column(TypeName = "ntext")]
        public string PhotoContent { get; set; }
        [StringLength(75)]
        public string Filename { get; set; }
        [Column("Resume_ID")]
        public long? ResumeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDateTime { get; set; }
        [Column("id")]
        public int? Id { get; set; }
    }

    public class ChildJobSeekerCvphoto: JobSeekerCvphoto
    {
        public string Base64fileContentWithContentType { get; set; }
    }
}
