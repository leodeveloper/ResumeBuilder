using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_REFERENCE")]
    public partial class HcResumeReference
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Column("ResumeID")]
        public long ResumeId { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Company { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        [StringLength(100)]
        public string ContactNo { get; set; }
        [Column(TypeName = "ntext")]
        public string HowdoYouKnow { get; set; }
        public short Verified { get; set; }
        public long AssessmentSheet { get; set; }
        [Column("ReferenceCreatedUserID")]
        public long ReferenceCreatedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ReferenceCreatedDate { get; set; }
        [Required]
        [StringLength(100)]
        public string Designation { get; set; }
        [StringLength(500)]
        public string Address2 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public long Occupation { get; set; }
        [Column("DesignationID")]
        public long DesignationId { get; set; }
        [StringLength(100)]
        public string EmailId { get; set; }
        public short Status { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
        [Column("ReferenceTypeID")]
        public long ReferenceTypeId { get; set; }
        [Required]
        [StringLength(100)]
        public string ReferenceLetterNumber { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ReferenceLetterDate { get; set; }       
        public bool Is_Deleted { get; set; }
        public string MongoDbID { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedUserid { get; set; }
    }
}
