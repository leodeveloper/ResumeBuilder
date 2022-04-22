using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeBuilder.Models
{ 

    public class AnswerResults
    {
        public int Id { get; set; }
        public string Emirates_Id { get; set; }
        public string Email { get; set; }
        public int Thriving_Index_Confidence { get; set; }

        public string Thriving_Index_Confidence_Value { get; set; }

        public int Thriving_Index_Optimism { get; set; }
        public string Thriving_Index_Optimism_Value { get; set; }
        public int Thriving_Index_Growth_Mindset { get; set; }
        public string Thriving_Index_Growth_Mindset_Value { get; set; }

        public int Thriving_Index_Achievement { get; set; }
        public string Thriving_Index_Achievement_Value { get; set; }
        public int Thriving_Index_Grit { get; set; }
        public string Thriving_Index_Grit_Value { get; set; }

        public int Thriving_Index_Resilience { get; set; }
        public string Thriving_Index_Resilience_Value { get; set; }
        public decimal Thriving_Index_Overall { get; set; }
        public int Cognitive_Ability_Reasonify { get; set; }
        public string Cognitive_Ability_Reasonify_Value { get; set; }
        public int Cognitive_Ability_Detectify { get; set; }
        public string Cognitive_Ability_Detectify_Value { get; set; }
        public int Cognitive_Ability_Numerify { get; set; }
        public string Cognitive_Ability_Numerify_Value { get; set; }
        public int Cognitive_Ability_Agile_Overall { get; set; }
        public string Cognitive_Ability_Agile_Overall_Value { get; set; }
        public int Agile_Verbify_English_Assessment { get; set; }
        public string Agile_Verbify_English_Assessment_Value { get; set; }
        public int Microsoft_Office_Word { get; set; }
        public string Microsoft_Office_Word_Value { get; set; }
        public int Microsoft_Office_Excel { get; set; }
        public string Microsoft_Office_Excel_Value { get; set; }
        public int Microsoft_Office_Outlook { get; set; }
        public string Microsoft_Office_Outlook_Value { get; set; }
        public int Microsoft_Office_PowerPoint { get; set; }
        public string Microsoft_Office_PowerPoint_Value { get; set; }

        public decimal Microsoft_Office_Overall { get; set; }
        public decimal Overall_Score_Per_Candidate { get; set; }
        public string Overall_Rating_Matrix_Grid { get; set; }
        public string Career_Top_01 { get; set; }
        public int Career_Stars_01 { get; set; }
        public string Career_Top_02 { get; set; }
        public int Career_Stars_02 { get; set; }
        public string Career_Top_03 { get; set; }
        public int Career_Stars_03 { get; set; }
        public string Career_Top_04 { get; set; }
        public int Career_Stars_04 { get; set; }
        public string Career_Top_05 { get; set; }
        public int Career_Stars_05 { get; set; }
        public bool IsActive { get; set; }
    }
}
