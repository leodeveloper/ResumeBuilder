using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models.HraOpsModels
{
    public class HcResumeReferenceViewModel
    {
        public long ResumeID { get; set; }
        public long ReferenceTypeId { get; set; }
        public string LetterName { get; set; }
        public string LetterNumber { get; set; }
        public DateTime LetterDate { get; set; }
        public string MongoDbID { get; set; }
    }

    public class HcResumeReferenceViewModel_Get : HcResumeReferenceViewModel
    {
        public long Rid { get; set; }
        public string ReferenceType { get; set; }
        public long ReferenceCreatedUserId { get; set; }
        public DateTime? ReferenceCreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedUserid { get; set; }
    }
}
