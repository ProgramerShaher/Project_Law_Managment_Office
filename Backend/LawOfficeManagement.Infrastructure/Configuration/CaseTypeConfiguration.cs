using LawOfficeManagement.Core.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class CaseTypeConfiguration : IEntityTypeConfiguration<CaseType>
    {
        public void Configure(EntityTypeBuilder<CaseType> builder)
        {
            builder.ToTable("CaseTypes", "Cases");

            builder.HasKey(ct => ct.Id);

            builder.Property(ct => ct.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(ct => ct.Description)
                   .HasMaxLength(500);

        }
    }
}