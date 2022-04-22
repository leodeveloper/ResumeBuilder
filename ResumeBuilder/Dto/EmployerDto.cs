using ResumeBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Dto
{
    public class EmployerDto
    {
        public EmployerDto()
        {
            this.Employer = new Employer();
        }
        public Employer Employer { get; set; }
        public string Particular { get; set; }
        public string ParticularAr { get; set; }
        public string EmployerName { get; set; }
        public string EmployerNameAr { get; set; }
        public string Employer_Emirates { get; set; }
        public string Employer_EmiratesAr { get; set; }
        public string Designation { get; set; }
        public string DesignationAr { get; set; }
        public bool IsThisMyCurrentRole { get; set; }

    }
}
