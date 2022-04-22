using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    public class HcResumeEngagementViewModel
    {
        [Required]
        public long[] ResumeId { get; set; }
        [Required]
        public DateTime StartAppointmentDate { get; set; }
        [Required]
        public string StartAppointmentTime { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        [Required]
        public string AppointmentTime { get; set; }
        [Required]
        public int AppointmentType { get; set; }
        [Required]
        public int AppointmentMethod { get; set; }
        [Required]
        public int Advisor { get; set; }
        [Required]
        public string Address { get; set; }
        public string Notes { get; set; }

        public bool Send_Reminder { get; set; }
        public int Status { get; set; }
    }
    public class HcResumeEngagementViewModel_Get : HcResumeEngagementViewModel
    {
        public HcResumeEngagementViewModel_Get()
        {
            this.EngagementJobSeekersInfo = new List<EngagementJobSeeker>();
        }
        public long Rid { get; set; }
        public string AdvisorName { get; set; }
        public string StatusEn { get; set; }
        public string StatusAe { get; set; }       
        public DateTime CreatedDate { get; set; }
        public long CreatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedUserId { get; set; }
        public IList<EngagementJobSeeker> EngagementJobSeekersInfo { get; set; }
    }

    public class EngagementJobSeeker
    {
        public EngagementJobSeeker()
        {
            this.JobSeekerNote = new List<HcResumeEngagementStatusNotes_Get>();
        }
        public long Rid { get; set; }
        public long ResumeId { get; set; }
        public string JobSeekerId { get; set; }
        public string FullName { get; set; }
        public string FullNameAr { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedUserId { get; set; }
        public string JobSeekerNotes { get; set; }
        public int Status { get; set; }
        public string StatusEn { get; set; }
        public string StatusAe { get; set; }

        public IList<HcResumeEngagementStatusNotes_Get> JobSeekerNote { get; set; }

    }

    public class HcResumeEngagementViewModel_GetByAdvisor
    {
        public int advisor { get; set; }
        public DateTime? startDateTime { get; set; }
        public DateTime? endDateTime { get; set; }
    }

    public class HcResumeEngagementStatusNotes
    {
        public string jobseekernotes { get; set; }
        public int statusId { get; set; }
        public long rid { get; set; }
        public int userid { get; set; }
    }

    public class HcResumeEngagementStatusNotes_Get
    {
        public long Rid { get; set; }
        public string JobSeekerNotes { get; set; }
        public string StatusEn { get; set; }
        public string StatusAe { get; set; }
        public string FullName { get; set; }
        public string FullNameAr { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedUserId { get; set; }
    }
}