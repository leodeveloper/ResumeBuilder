using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    public class HcResumeChallangesViewModel
    {
        public HcResumeChallangesViewModel()
        {
            this.ChallangeIds = new List<int>();
        }
        public long ResumeId { get; set; }
        public string Notes { get; set; }
        public IList<int> ChallangeIds { get; set; }
    }
}
