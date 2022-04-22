using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
//using KeyAttribute = Dapper.Contrib.Extensions.KeyAttribute;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("Pensionfund")]
    public partial class Pensionfund
    {
        
        [Dapper.Contrib.Extensions.Key]
        [Column("RID")]
        public int Rid { get; set; }

        [StringLength(25)]
        public string NationId { get; set; }
        [StringLength(75)]
        public string DataAccuracy { get; set; }
        [StringLength(75)]
        public string EmployerName { get; set; }
        [StringLength(75)]
        public string EmploymentStatus { get; set; }
        [StringLength(75)]
        public string PersonRecordAvailability { get; set; }
        [Column("StringDateOFBirth")]
        [StringLength(75)]
        public string StringDateOfbirth { get; set; }
        [Column("unifiedNumber")]
        [StringLength(75)]
        public string UnifiedNumber { get; set; }
        public string Reason { get; set; }
        public bool IsDeleted { get; set; }
    }
}
