using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_EDUCATION_TYPES")]
    public partial class HcmEducationTypes
    {
        [Dapper.Contrib.Extensions.Key]      
        public long Rid { get; set; }
        [Required]
        [StringLength(1000)]
        public string EducationType { get; set; }
        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        [Column(TypeName = "ntext")]
        public string EducationAlias { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal YearsOfEducation { get; set; }
        [Column(TypeName = "ntext")]
        public string ExcludeAlias { get; set; }
        [Required]
        public byte[] MyCount { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
        [Column("V3JobsID")]
        public long V3jobsId { get; set; }
        [Column("MCreatedUser")]
        public long McreatedUser { get; set; }
        [Column("MCreatedDate", TypeName = "datetime")]
        public DateTime? McreatedDate { get; set; }
        [Column("MModifiedUser")]
        public long MmodifiedUser { get; set; }
        public short TypeIndex { get; set; }
        [Required]
        [StringLength(10)]
        public string EducationCode { get; set; }
        public short LanguageType { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
        public short Status { get; set; }
    }
}
