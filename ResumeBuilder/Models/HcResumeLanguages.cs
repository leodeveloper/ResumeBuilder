using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_LANGUAGES")]
    public partial class HcResumeLanguages
    {
        [Dapper.Contrib.Extensions.Key]       
        public long Rid { get; set; }
        [Column("ResumeID")]
        public long ResumeId { get; set; }
        [Column("LanguageID")]
        public long LanguageId { get; set; }
        public short ProficiencyLevel { get; set; }
        public short ReadLevel { get; set; }
        public short WriteLevel { get; set; }
        public short SpeakLevel { get; set; }
        [Required]
        [StringLength(250)]
        public string Language { get; set; }
        [StringLength(10)]
        public string NativeLanguage { get; set; }
        public long ProficiencyType { get; set; }
        public short UnderstandingLevel { get; set; }
        public short? PushedToTanmia { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PushedToTanmiaOn { get; set; }
        public string TanmiaIntgRespSts { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column("CreatedUserID")]
        public long? CreatedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        [Column("ModifiedUserID")]
        public long? ModifiedUserId { get; set; }
        [Column("Is_Deleted")]
        public bool Is_Deleted { get; set; }
    }
}
