using LawOfficeManagement.Core.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class ServiceOfficeConfiguration : IEntityTypeConfiguration<ServiceOffice>
    {
        public void Configure(EntityTypeBuilder<ServiceOffice> builder)
        {
            // Table name
            builder.ToTable("ServiceOffices");

            // Primary Key
            builder.HasKey(so => so.Id);

            // Properties Configuration
            builder.Property(so => so.ServiceName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(so => so.ServicePrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(so => so.Notes)
                .HasMaxLength(500)
                .IsRequired(false);

            // Navigation Property
            builder.HasMany(so => so.legalConsultations)
                .WithOne(lc => lc.ServiceOffice)
                .HasForeignKey(lc => lc.ServiceOfficeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(so => so.ServiceName)
                .IsUnique();

            builder.HasIndex(so => so.ServicePrice);

            // Timestamps
            builder.Property(so => so.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

          
        }
    }
}