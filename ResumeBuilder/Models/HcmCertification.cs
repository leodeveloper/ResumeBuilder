using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_CERTIFICATION")]
    public partial class HcmCertification
    {
        [Dapper.Contrib.Extensions.Key]
        [Column("RID")]
        public long Rid { get; set; }
        [StringLength(100)]
        public string Title { get; set; }
        public short LanguageType { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
    }
}
