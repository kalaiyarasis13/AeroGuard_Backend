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
        }
    }
}
