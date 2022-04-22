using Dapper.Contrib.Extensions;
using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("Education")]
    public partial class Education
    {
        [Key] 
        public long Rid { get; set; }
        [Column("Resume_ID")]
        public long Resume_ID { get; set; }
        [Column("Education_Group_ID")]
        public int Education_Group_Id { get; set; }
        [Column("Education_Type_ID")]
        public int Education_Type_Id { get; set; }
        [Column("University_Type_ID")]
        public int? University_Type_Id { get; set; }
        [Column("University_ID")]
        public int? University_Id { get; set; }
        [Column("Course_ID")]
        public int? Course_Id { get; set; }
        [Column("Education_Major_ID")]
        public int? Education_Major_Id { get; set; }
        [Column("Emirate_ID")]
        public int? Emirate_Id { get; set; }
        
        public string Grade { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToYear { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromYear { get; set; }
        [System.ComponentModel.DataAnnotations.StringLength(75)]
        public string Institute { get; set; }

        public bool IsThisHigestQualication { get; set; }

        [Column("Category_ID")]
        public int? Category_ID { get; set; }
        [Column("Grade_Category_ID")]
        public int? Grade_Category_ID { get; set; }
        [Column("Location_ID")]
        public int? Location_ID { get; set; }

    }
}
