using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models
{
    public class Contact
    {
        public long Id { get; set; }
        public int? Companyid { get; set; }
        public string Nameen { get; set; }
        public string Namear { get; set; }
        public string Emailid { get; set; }
        public string Phoneno { get; set; }
        public string Mobileno { get; set; }
        public int? Designationid { get; set; }
        public int? Statusid { get; set; }
        public int? Reasonid { get; set; }
        public string Detail { get; set; }
        public int? hcuser_rid { get; set; }
    }
}
