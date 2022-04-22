﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_RESUME_PREFFERED_JOBTITLE")]
    public partial class HcResumePrefferedJobtitle
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        public long? Resumeid { get; set; }
        [Column("jobtitleid")]
        public long? Jobtitleid { get; set; }
        [Column("createddate", TypeName = "datetime")]
        public DateTime Createddate { get; set; }
        [Column("createduserid")]
        public long? Createduserid { get; set; }
        [Column("modifieddate", TypeName = "datetime")]
        public DateTime? Modifieddate { get; set; }
        [Column("modifieduserid")]
        public long? Modifieduserid { get; set; }
        [Column("isdeleted")]
        public short? Isdeleted { get; set; }
    }
}
