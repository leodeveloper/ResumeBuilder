using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("Certification")]
    public partial class Certification
    {
        [Dapper.Contrib.Extensions.Key]
        [Column("RID")]
        public int Rid { get; set; }
        [Column("Resume_ID")]
        public long Resume_ID { get; set; }
        [Required]
        [StringLength(200)]
        public string CertificationName { get; set; }
        public int? CertificateType { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? InstituteId { get; set; }
        [StringLength(200)]
        public string Institute { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        public decimal? Score { get; set; }
        public bool IsDeleted { get; set; }
    }
}
