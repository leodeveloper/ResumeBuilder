using System;
using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("JobSeekerPod")]
    public partial class JobSeekerPod
    {
        [Key]
        [Column("RId")]
        public int Rid { get; set; }
        [Column("Resume_ID")]
        public long Resume_ID { get; set; }
        [Column("SpeicalNeeds_ID")]
        public int SpeicalNeeds_ID { get; set; }
        [Column("Is_Deleted")]
        public bool Is_Deleted { get; set; }
    }
    public class JobSeekerPodViewModel
    {
        public long Resume_ID { get; set; }
        public int[] SpeicalNeeds_ID { get; set; }
    }
}
