using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeBuilder.Models
{
    [Table("JobSeekerAttachment")]
    public partial class JobSeekerAttachment
    {
        [Dapper.Contrib.Extensions.Key]
        [Column("RID")]
        public int Rid { get; set; }
        public long Resume_ID { get; set; }
        public string MongoDBUniqueId { get; set; }
        [Column("DocumentType_ID")]
        public int DocumentType_ID { get; set; }
        [Required]
        [StringLength(150)]
        public string FileName { get; set; }
        public DateTime FileDateTime { get; set; } = DateTime.Now;
        public string Description { get; set; }
    }

   

    public class Resume_Attachment
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string UnqiueId { get; set; }
        public string ResumeAttachmentBase64String { get; set; }
        public string FileName { get; set; }

    }

    public class Job_ApplicationAttachment
    {
        public long JobSeekerId { get; set; }
        public string FileId { get; set; }
        public string FileName { get; set; }
        public int DocumentType { get; set; }
        public string CollectionName { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class Job_ApplicationAttachmentMongoDB
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string FileId { get; set; }
       // public int EntityId { get; set; }
        public string DataBase64String { get; set; }
        public string FileName { get; set; }

    }
}
