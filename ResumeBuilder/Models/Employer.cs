using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Table("Employer")]
    public partial class Employer
    {
        [Dapper.Contrib.Extensions.Key]
        [Column("RID")]
        public long Rid { get; set; }
        [Column("Resume_ID")]
        public long Resume_ID { get; set; }
        [Column("Particular_ID")]
        public int Particular_ID { get; set; }
        [Column("Employer_ID")]
        public long Employer_ID { get; set; }
        public string EmployerAddress { get; set; }
        [StringLength(25)]
        public string Phone { get; set; }
        [Column("Employer_Code_No")]
        [StringLength(15)]
        public string Employer_Code_No { get; set; }
        [Column("Desigination_ID")]
        public long Desigination_ID { get; set; }
        [StringLength(75)]
        public string Department { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        public bool IsThisMyCurrentRole { get; set; }
        [StringLength(2000)]
        public string RolesAndResposibilites { get; set; }
        [StringLength(800)]
        public string Achievements { get; set; }
        public int? Employer_Location_ID { get; set; }
        public int? Employment_Type_ID { get; set; }       
        public int? Job_Industry_Type_ID { get; set; }       
        public int? Job_Role_ID { get; set; }
        public int? Job_Location_ID { get; set; }
    }
}
