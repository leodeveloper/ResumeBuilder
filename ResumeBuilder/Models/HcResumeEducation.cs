using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_EDUCATION")]
    public partial class HcResumeEducation
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsEducationAbroad { get; set; }
        public bool Is_Deleted { get; set; }
        public long? EducationAbroadCountryId { get; set; }
        [Column("EducationID")]
        public long EducationId { get; set; }
        [Column("ResumeID")]
        public long ResumeId { get; set; }
        [StringLength(500)]
        public string Institute { get; set; }
        public int? Year { get; set; }
        public int GradeId { get; set; }
        [StringLength(4000)]
        public string Grade { get; set; }
        [Column("SpecializationID")]
        public long SpecializationId { get; set; }
        public short SerialNo { get; set; }
        [Column("InstituteGradeID")]
        public long InstituteGradeId { get; set; }
        public short Type { get; set; }
        [Column("UniversityID")]
        public long UniversityId { get; set; }
        [Column("UniversityTypeID")]
        public long UniversityTypeId { get; set; }
        public long Specialization2 { get; set; }
        public int Month { get; set; }
        [Required]
        [StringLength(200)]
        public string Notes { get; set; }
        [Required]
        [StringLength(200)]
        public string Organization { get; set; }
        [Column("CountryID")]
        public long CountryId { get; set; }
        [StringLength(40)]
        public string Department { get; set; }
        public int Duration { get; set; }
        public short MeasurementUnit { get; set; }
        public short MarksObtainedUnit { get; set; }
        [Column("InstituteID")]
        public long InstituteId { get; set; }
        public short Certification { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? IssueDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpireDate { get; set; }
        [StringLength(50)]
        public string CertificationNo { get; set; }
        [Column(TypeName = "ntext")]
        public string TechnologyNotes { get; set; }
        [StringLength(100)]
        public string IssuedBy { get; set; }
        [Column("StateID")]
        public long StateId { get; set; }
        public short EduLevel { get; set; }
        [StringLength(100)]
        public string ExamName { get; set; }
        [StringLength(50)]
        public string RollNumber { get; set; }
        [Column("EducationGroupID")]
        public long EducationGroupId { get; set; }
        public int? FromYear { get; set; }
        [Required]
        [StringLength(200)]
        public string DeviationReason { get; set; }
        public bool IsHighestEducation { get; set; }
        [StringLength(100)]
        public string SpecializationText { get; set; }
        [Required]
        [StringLength(200)]
        public string UniversityText { get; set; }
        [Required]
        [StringLength(200)]
        public string LocationText { get; set; }
        [Column("EducationID_O")]
        public long EducationId_O { get; set; }
        [Column("EducationGroupID_O")]
        public long EducationGroupId_O { get; set; }
        [Column("SpecializationID_O")]
        public long SpecializationId_O { get; set; }
        [Column("Specialization_O")]
        public long Specialization_O { get; set; }
        public bool? Synced { get; set; }
        [Column("CDDate", TypeName = "datetime")]
        public DateTime Cddate { get; set; }
        public short? PushedToTanmia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PushedToTanmiaOn { get; set; }
        public string TanmiaIntgRespSts { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Createddate { get; set; }
        [Column("createduserid")]
        public long? Createduserid { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        [StringLength(300)]
        public string ModifiedUser { get; set; }
    }
}
