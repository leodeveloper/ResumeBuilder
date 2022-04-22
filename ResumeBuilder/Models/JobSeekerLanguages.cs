using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("JobSeekerLanguages")]
    public partial class JobSeekerLanguages
    {
        [Dapper.Contrib.Extensions.Key]
        [Column("RID")]
        public int Rid { get; set; }
        [Column("Resume_ID")]
        public long Resume_ID { get; set; }
        [Column("Language_ID")]
        public int Language_ID { get; set; }
        public bool LanguageRead { get; set; }
        public bool LanguageWrite { get; set; }
        public bool LanguageSpeak { get; set; }
        public bool Languagecomprehand { get; set; }
        [Column("Is_Deleted")]
        public bool Is_Deleted { get; set; }
    }
}
