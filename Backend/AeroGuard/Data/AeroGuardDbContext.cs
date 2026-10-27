using AeroGuard.Models;
using Microsoft.EntityFrameworkCore;

namespace AeroGuard.Data
{
    public class AeroGuardDbContext : DbContext
    {
        public AeroGuardDbContext(
        DbContextOptions<AeroGuardDbContext> options)
        : base(options)
        {
        }

        public DbSet<Survey> Surveys => Set<Survey>();

        public DbSet<SurveyImage> SurveyImages
            => Set<SurveyImage>();

        public DbSet<AIAnalysisRun> AIAnalysisRuns
            => Set<AIAnalysisRun>();

        public DbSet<AIFinding> AIFindings
            => Set<AIFinding>();

        protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Survey>(entity =>
            {
                entity.HasKey(x => x.SurveyID);

                entity.Property(x => x.SurveyType)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(x => x.AreaName)
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(x => x.SurveyStatus)
                    .HasMaxLength(30)
                    .IsRequired();
            });

            modelBuilder.Entity<SurveyImage>(entity =>
            {
                entity.HasKey(x => x.ImageID);

                entity.Property(x => x.OriginalFileName)
                    .HasMaxLength(255);

                entity.Property(x => x.FilePath)
                    .HasMaxLength(500);

                entity.HasOne(x => x.Survey)
                    .WithMany(x => x.Images)
                    .HasForeignKey(x => x.SurveyID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // New AIAnalysisRun configuration
            modelBuilder.Entity<AIAnalysisRun>(entity =>
            {
                entity.HasKey(x => x.AnalysisRunID);

                entity.Property(x => x.AnalysisType)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(x => x.ResultSource)
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(x => x.Status)
                    .HasMaxLength(30)
                    .IsRequired();

                entity.Property(x => x.ModelVersion)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasOne(x => x.Image)
                    .WithMany()
                    .HasForeignKey(x => x.ImageID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // New AIFinding configuration
            modelBuilder.Entity<AIFinding>(entity =>
            {
                entity.HasKey(x => x.FindingID);

                entity.Property(x => x.ClassName)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasOne(x => x.AnalysisRun)
                    .WithMany(x => x.Findings)
                    .HasForeignKey(x => x.AnalysisRunID)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
