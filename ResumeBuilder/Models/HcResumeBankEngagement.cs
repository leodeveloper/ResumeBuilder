using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_BANK_ENGAGEMENT")]
    public partial class HcResumeBankEngagement
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime StartAppointmentDate { get; set; }
        [Required]
        [StringLength(20)]
        public string StartAppointmentTime { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime AppointmentDate { get; set; }
        [Required]
        [StringLength(20)]
        public string AppointmentTime { get; set; }
        public int AppointmentType { get; set; }
        public int AppointmentMethod { get; set; }
        public int Advisor { get; set; }
        [Required]
        [StringLength(3500)]
        public string Address { get; set; }
        [StringLength(3500)]
        public string Notes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserId { get; set; }
        public bool Is_Deleted { get; set; }
        public bool Send_Reminder { get; set; }
        public int Status { get; set; }
    }
}
