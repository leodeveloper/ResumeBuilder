using System;
using System.Collections.Generic;
using System.Text;

namespace IntegrationApiClassLibrary.Model
{
    public class PensionfundDto
    {
        public string dataAccuracy { get; set; }
        public string employerName { get; set; }
        public string employmentStatus { get; set; }
        public string nationalId { get; set; }
        public string personRecordAvailability { get; set; }
        public string stringDateOFBirth { get; set; }
        public string unifiedNumber { get; set; }
        public string reason { get; set; }
    }


    //this for integration api post request
    public class PensionfundPost
    {
        public string NationalId { get; set; }
        public string UnifiedNumber { get; set; }
        public string FullArabicName { get; set; }
        public string DateOfBirth { get; set; }
        public string StringDateOFBirth { get; set; }
    }
}
