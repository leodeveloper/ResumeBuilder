using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_EDUCATION_SPECIALIZATION_TYPES")]
    public partial class HcmEducationSpecializationTypes
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [Required]
        [StringLength(100)]
        public string SpecializationTitle { get; set; }
        [Column(TypeName = "ntext")]
        public string Notes { get; set; }
        [Column(TypeName = "ntext")]
        public string IncludeList { get; set; }
        [Column(TypeName = "ntext")]
        public string ExcludeList { get; set; }
        [Required]
        [StringLength(10)]
        public string MajorCode { get; set; }
        public short LanguageType { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
        [Column("SpecializationGroupID")]
        public long SpecializationGroupId { get; set; }
        public short Status { get; set; }
        [Column("MCreatedUser")]
        public long McreatedUser { get; set; }
        [Column("MCreatedDate", TypeName = "datetime")]
        public DateTime McreatedDate { get; set; }
        [Column("MModifiedUser")]
        public long MmodifiedUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedDate { get; set; }
    }
}
