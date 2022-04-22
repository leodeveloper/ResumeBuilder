using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HCM_RESUME_LOCATIONS")]
    public partial class HcmResumeLocations
    {
        [Dapper.Contrib.Extensions.Key]       
        public long Rid { get; set; }
        [Required]
        [StringLength(100)]
        public string LocationTitle { get; set; }
        [StringLength(100)]
        public string Pincode { get; set; }
        [Column(TypeName = "ntext")]
        public string LocationAlias { get; set; }
        [Column(TypeName = "ntext")]
        public string LocationExcludeAlias { get; set; }
        [Column("StateID")]
        public long StateId { get; set; }
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
        [Column("GroupID")]
        public long GroupId { get; set; }
        public short ReqLocationStatus { get; set; }
        public short IsMetro { get; set; }
        [Required]
        [Column("RefID")]
        [StringLength(15)]
        public string RefId { get; set; }
        [Column("ZoneID")]
        public long ZoneId { get; set; }
        [Column("CategoryID")]
        public long CategoryId { get; set; }
        [Required]
        [Column("STDCode")]
        [StringLength(6)]
        public string Stdcode { get; set; }
        public short LanguageType { get; set; }
        [Column("EngKeyID")]
        public long EngKeyId { get; set; }
        [Column("BaseLocationID")]
        public long BaseLocationId { get; set; }
        public short Status { get; set; }
        [Column("PTApplicable")]
        public short Ptapplicable { get; set; }
        [Column("ESIApplicable")]
        public short Esiapplicable { get; set; }
        public short HardShipLocation { get; set; }
        public short SerialNo { get; set; }
    }
}
