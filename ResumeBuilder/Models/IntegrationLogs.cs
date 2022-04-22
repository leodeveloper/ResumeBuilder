using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("IntegrationLogs")]
    public partial class IntegrationLogs
    {
        [Dapper.Contrib.Extensions.Key]
        [Column("RID")]
        public Guid Rid { get; set; }
        [Required]
        [StringLength(75)]
        public string IntegrationName { get; set; }
        [Required]
        [StringLength(15)]
        public string Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastUpdateDateTime { get; set; }
        [Required]
        [StringLength(95)]
        public string UserName { get; set; }
        public bool IsSuccessfull { get; set; }
        public long RowID { get; set; }
    }
}
