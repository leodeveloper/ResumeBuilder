using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_MILITARYBATCHNO")]
    public partial class HcmMilitarybatchno
    {
        [Dapper.Contrib.Extensions.Key]
        public int Rid { get; set; }
        [Column("EngKeyID")]
        public int EngKeyId { get; set; }
        [Required]
        [StringLength(75)]
        public string Title { get; set; }
        [Column("MCreatedUser")]
        public long McreatedUser { get; set; }
        [Column("MCreatedDate", TypeName = "datetime")]
        public DateTime McreatedDate { get; set; }
        [Column("MModifiedUser")]
        public long? MmodifiedUser { get; set; }
        [Column("MModifiedDate", TypeName = "datetime")]
        public DateTime? MmodifiedDate { get; set; }
        public short LanguageType { get; set; }
    }
}
