using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("Training")]
    public partial class Training
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Column("Resume_ID")]
        public long Resume_ID { get; set; }
        [Column("TrainingName_ID")]
        public int? TrainingName_ID { get; set; }
        [Column("InstituteName_ID")]
        public int? InstituteName_ID { get; set; }
        [Column("TrainingCenter_ID")]
        public int? TrainingCenter_ID { get; set; }
        [Column("Batch_ID")]
        public int? Batch_ID { get; set; }
        [StringLength(20)]
        public string TrainingCode { get; set; }
        [Column(TypeName = "float")]
        public double? TrainingGrade { get; set; }
        [StringLength(35)]
        public string CourseTitle { get; set; }
        [Column("TrainingCategory_ID")]
        public int? TrainingCategory_ID { get; set; }
        [Column("TrainingVenue_ID")]
        public int? TrainingVenue_ID { get; set; }
        [Column("Stage_ID")]
        public int? Stage_ID { get; set; }
        [Column(TypeName = "float")]
        public double? TrainingScore { get; set; }
        [Column("ProgrameName_ID")]
        public int? ProgrameName_ID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? StartDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public string Comments { get; set; }

        //[ForeignKey(nameof(Resume_ID))]
        //[InverseProperty(nameof(JobSeekerResume.Training))]
        //public virtual JobSeekerResume Resume { get; set; }
    }
}
