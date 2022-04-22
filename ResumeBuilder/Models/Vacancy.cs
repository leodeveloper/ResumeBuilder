using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        public string AutoGenerateJobCode { get; set; }
        public string ReferenceCode { get; set; }
        public string EnTitle { get; set; }
        public string ArTitle { get; set; }
        public int EmployerId { get; set; }
        public bool AppliedVacancyStatus { get; set; }
    }

    public class PostVacancy
    {
        public PostVacancy()
        {
            this.listVacancy = new List<int>();
        }
        public string UserId { get; set; }
        public int JobSeekerId { get; set; }

        public IList<int> listVacancy { get; set; }
    }
}
