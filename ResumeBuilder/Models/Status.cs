using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("Status")]
    public partial class Status
    {
        [Dapper.Contrib.Extensions.Key]
        [Column("RID")]
        public long Rid { get; set; }
        public string Title { get; set; }
        public string TitleAr { get; set; }
        public string FullTitle { get { return $"{this.Title} -- {this.TitleAr}"; } }
        public short? StatusType { get; set; }
        public short? StatusImageIndex { get; set; }
        public short Value { get; set; }
    }
}
