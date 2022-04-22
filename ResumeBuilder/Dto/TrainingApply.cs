using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Dto
{
    public class TrainingApply
    {

        public TrainingApply()
        {
            this.Batch_ID = new List<string>();
        }

        public long Resume_ID { get; set; }
        public IList<string> Batch_ID { get; set; }
    }
}
