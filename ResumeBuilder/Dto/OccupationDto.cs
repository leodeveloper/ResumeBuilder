using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Dto
{
    public class OccupationDto
    {
        public int OccupationId { get; set; }
        public string Occupation { get; set; }
        public string OccupationAr { get; set; }
        public int SkillGroupId { get; set; }
        public string SkillGroup { get; set; }
        public string SkillGroupAr { get; set; }
    }
}
