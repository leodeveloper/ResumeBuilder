using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_OCCUPATION")]
    public partial class HcmOccupation
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [Column("RefID")]
        [StringLength(20)]
        public string RefId { get; set; }
        [StringLength(200)]
        public string Alias { get; set; }
        public short Critical { get; set; }
        public short OccupationalStatus { get; set; }
        public long Grade { get; set; }
        public short LocalLanguageDependency { get; set; }
        [Column(TypeName = "decimal(16, 2)")]
        public decimal SalaryAmount { get; set; }
        public long SalaryCurrency { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
        public short LanguageType { get; set; }
        [Column("MCreatedUser")]
        public long McreatedUser { get; set; }
        [Column("MCreatedDate", TypeName = "datetime")]
        public DateTime? McreatedDate { get; set; }
        [Column("MModifiedUser")]
        public long MmodifiedUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        [Column(TypeName = "ntext")]
        public string KeyWords2 { get; set; }
        public short Isnonstandard { get; set; }
    }
}
