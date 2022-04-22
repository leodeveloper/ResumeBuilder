using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ResumeBuilder.Models
{
    public partial class JobSeekerContext : DbContext
    {
        public JobSeekerContext()
        {
        }

        public JobSeekerContext(DbContextOptions<JobSeekerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<Employer> Employer { get; set; }
        public virtual DbSet<JobSeekerResume> JobSeekerResume { get; set; }
        public virtual DbSet<Reference> Reference { get; set; }
        public virtual DbSet<Source> Source { get; set; }
        public virtual DbSet<Training> Training { get; set; }
        public virtual DbSet<Notes> Notes { get; set; }
        public virtual DbSet<JobsSeekerStatus> JobsSeekerStatus { get; set; }
        public virtual DbSet<Reason> Reason { get; set; }
        public virtual DbSet<ReasonStatus> ReasonStatus { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<JobSeekerAttachment> JobSeekerAttachment { get; set; }
        public virtual DbSet<Certification> Certification { get; set; }
        public virtual DbSet<Occupation> Occupation { get; set; }
        public virtual DbSet<ToolsKnowledge> ToolsKnowledge { get; set; }
        public virtual DbSet<Pensionfund> Pensionfund { get; set; }
        public virtual DbSet<JobSeekerLanguages> JobSeekerLanguages { get; set; }
        public virtual DbSet<JobSeekerPod> JobSeekerPod { get; set; }
        public virtual DbSet<JobSeekerCoverLetter> JobSeekerCoverLetter { get; set; }
        public virtual DbSet<JobSeekerGrieveance> JobSeekerGrieveance { get; set; }
        public virtual DbSet<JobSeekerCvphoto> JobSeekerCvphoto { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Education>(entity =>
            //{
            //    entity.Property(e => e.Rid).ValueGeneratedNever();

            //    entity.HasOne(d => d.Resume)
            //        .WithMany(p => p.Education)
            //        .HasForeignKey(d => d.Resume_Id)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Education_JobSeekerResume");
            //});

            //modelBuilder.Entity<Employer>(entity =>
            //{
            //    entity.HasOne(d => d.Resume)
            //        .WithMany(p => p.Employer)
            //        .HasForeignKey(d => d.ResumeId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Employer_JobSeekerResume");
            //});

            //modelBuilder.Entity<JobSeekerResume>(entity =>
            //{
            //    entity.HasKey(e => e.Rid)
            //        .HasName("PK_JobSeeker_Resume");

            //    entity.Property(e => e.EmailId).IsUnicode(false);

            //    entity.Property(e => e.FamilyName).IsUnicode(false);

            //    entity.Property(e => e.FirstName).IsUnicode(false);

            //    entity.Property(e => e.LastName).IsUnicode(false);

            //    entity.Property(e => e.MiddleName).IsUnicode(false);

            //    entity.Property(e => e.Salutation).IsUnicode(false);

            //    entity.Property(e => e.ThridName).IsUnicode(false);

            //    entity.Property(e => e.UnifiedNumber).IsUnicode(false);
            //});

            //modelBuilder.Entity<Reference>(entity =>
            //{
            //    entity.Property(e => e.ReferenceCreatedDate).HasDefaultValueSql("(getdate())");

            //    entity.HasOne(d => d.Resume)
            //        .WithMany(p => p.Reference)
            //        .HasForeignKey(d => d.ResumeId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Reference_JobSeekerResume");
            //});

            //modelBuilder.Entity<Source>(entity =>
            //{
            //    entity.Property(e => e.Rid).ValueGeneratedNever();

            //    entity.Property(e => e.SourceName).IsUnicode(false);

            //    entity.HasOne(d => d.Resume)
            //        .WithMany(p => p.Source)
            //        .HasForeignKey(d => d.ResumeId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Source_JobSeekerResume");
            //});

            //modelBuilder.Entity<Training>(entity =>
            //{
            //    entity.Property(e => e.Rid).ValueGeneratedNever();

            //    entity.HasOne(d => d.Resume)
            //        .WithMany(p => p.Training)
            //        .HasForeignKey(d => d.ResumeId)
            //        .OnDelete(DeleteBehavior.ClientSetNull)
            //        .HasConstraintName("FK_Training_Training");
            //});

            //OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
