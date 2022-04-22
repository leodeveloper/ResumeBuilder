using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    public class HcResumeCoverLetterViewModel
    {
        public long ResumeID { get; set; }
        public string ConverLetterName { get; set; }
        public string Content { get; set; }
    }

    public class HcResumeCoverLetterViewModel_Get : HcResumeCoverLetterViewModel
    {
        public long Rid { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedUserId { get; set; }
    }
}
