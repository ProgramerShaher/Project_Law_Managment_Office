using LawOfficeManagement.Core.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class CourtConfiguration : IEntityTypeConfiguration<Court>
    {
        public void Configure(EntityTypeBuilder<Court> builder)
        {
            builder.ToTable("Courts");
            builder.HasKey(x => x.Id);
            // Unique court name excluding soft-deleted rows
            builder.HasIndex(x => x.Name)
                   .IsUnique()
                   .HasFilter("[IsDeleted] = 0");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Address).IsRequired().HasMaxLength(300);

            builder.HasOne(x => x.CourtType)
                   .WithMany(t => t.Courts)
                   .HasForeignKey(x => x.CourtTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.Divisions)
                   .WithOne(d => d.Court)
                   .HasForeignKey(d => d.CourtId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
