using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("HC_USERS")]
    public partial class HcUsers
    {
        [Dapper.Contrib.Extensions.Key]
        public long Rid { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }       
        [StringLength(100)]
        public string FirstName { get; set; }
        [StringLength(25)]
        public string LastName { get; set; }        
        [Column("Is_Coach")]
        public bool IsCoach { get; set; }
    }
}
