using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ResumeBuilder.Models
{
    [Dapper.Contrib.Extensions.Table("Notes")]
    public class Notes
    {
        [Dapper.Contrib.Extensions.Key]
        [Column("RID")]
        public long Rid { get; set; }
        [Column("Resume_ID")]
        public long Resume_ID { get; set; }
        [Required]
        public string Note { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime Lastupdate { get; set; } = DateTime.Now;
        [Required]
        public string Title { get; set; }
        public int NoteType { get; set; }
        public string UserName { get; set; }
    }
}
