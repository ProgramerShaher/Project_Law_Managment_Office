using LawOfficeManagement.Core.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class CourtDivisionConfiguration : IEntityTypeConfiguration<CourtDivision>
    {
        public void Configure(EntityTypeBuilder<CourtDivision> builder)
        {
            builder.ToTable("CourtDivisions");
            builder.HasKey(x => x.Id);

            // Unique division name within a court (exclude soft-deleted rows)
            //builder.HasIndex(x => new { x.CourtId, x.Name })
            //       .IsUnique() 
            //       .HasFilter("[IsDeleted] = 0");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.JudgeName).IsRequired(false).HasMaxLength(150);
            // ÇáÚáÇÞÉ ãÚ Court
            builder.HasOne(cd => cd.Court)
                .WithMany(c => c.Divisions)
                .HasForeignKey(cd => cd.CourtId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            // ÇáÚáÇÞÉ ãÚ Cases
            builder.HasMany(cd => cd.Cases)
                .WithOne(c => c.CourtDivision)
                .HasForeignKey(c => c.CourtDivisionId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            // ÇáÚáÇÞÉ ãÚ CaseSessions
            builder.HasMany(cd => cd.CaseSessions)
                .WithOne(cs => cs.CourtDivision)
                .HasForeignKey(cs => cs.CourtDivisionId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        }
    }
}
