using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("DocumentType")]
    public partial class DocumentType
    {
        [Dapper.Contrib.Extensions.Key]
        [Column("RID")]
        public int Rid { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [Required]
        [StringLength(150)]
        public string TitleAr { get; set; }

        public string FullTitle { get { return $"{Title} -- {TitleAr}"; } }
    }
}
