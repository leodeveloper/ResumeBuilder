
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("Reference")]
    public partial class Reference
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Column("Resume_ID")]
        public long Resume_ID { get; set; }
        [Column("Reference_Name")]
        [Required]
        [StringLength(55)]
        public string Reference_Name { get; set; }
        [Column("Company_Name")]
        [Required]
        [StringLength(55)]
        public string Company_Name { get; set; }
        [StringLength(80)]
        public string Address1 { get; set; }
        [StringLength(80)]
        public string Address2 { get; set; }
        [Required]
        [StringLength(35)]
        public string ContactNumber { get; set; }
        [StringLength(55)]
        public string Department { get; set; }
        [StringLength(55)]
        public string Occupation { get; set; }
        [Required]
        [StringLength(35)]
        public string EmailId { get; set; }
        [StringLength(35)]
        public string Username { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReferenceCreatedDate { get; set; }

        //[ForeignKey(nameof(ResumeId))]
        //[InverseProperty(nameof(JobSeekerResume.Reference))]
        //public virtual JobSeekerResume Resume { get; set; }
    }
}
