using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_DRIVING_LICENSE")]
    public partial class HcmDrivingLicense
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        public short LanguageType { get; set; }
        [Column("ERefID")]
        public long ErefId { get; set; }
        [Column("EngkeyID")]
        public long EngkeyId { get; set; }
    }
}
