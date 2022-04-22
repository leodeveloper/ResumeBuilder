using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_COUNTRIES")]
    public partial class HcmCountries
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [StringLength(100)]
        public string CountryTitle { get; set; }
        [Column("MCreatedUser")]
        public long McreatedUser { get; set; }
        [Column("MCreatedDate", TypeName = "datetime")]
        public DateTime? McreatedDate { get; set; }
        [Column("MModifiedUser")]
        public long MmodifiedUser { get; set; }
        [Column("MModifiedDate", TypeName = "datetime")]
        public DateTime? MmodifiedDate { get; set; }
        public int ProbationPeriod { get; set; }
        [Required]
        [StringLength(10)]
        public string CountryCode { get; set; }
        [Required]
        [Column("ISDCode")]
        [StringLength(6)]
        public string Isdcode { get; set; }
        public short LanguageType { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
    }
}
