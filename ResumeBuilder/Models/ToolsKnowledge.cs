using System;
using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("ToolsKnowledge")]
    public partial class ToolsKnowledge
    {
        [Key]
        [Column("RId")]
        public int Rid { get; set; }
        [Column("Resume_ID")]
        public long Resume_ID { get; set; }
        public int ToolsKnowledgeId { get; set; }
        public bool IsDeleted { get; set; }
        public int Proficiency { get; set; }
    }


    public class ToolsKnowledgeViewModel
    {
        public long Resume_ID { get; set; }
        public int[] ToolsKnowledgeId { get; set; }
    }
}
