using LawOfficeManagement.Core.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class CourtTypeConfiguration : IEntityTypeConfiguration<CourtType>
    {
        public void Configure(EntityTypeBuilder<CourtType> builder)
        {
            builder.ToTable("CourtTypes");
            builder.HasKey(x => x.Id);

            // Unique court type name excluding soft-deleted rows
            builder.HasIndex(x => x.Name)
                   .IsUnique()
                   .HasFilter("[IsDeleted] = 0");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Notes).HasMaxLength(500);
        }
    }
}
