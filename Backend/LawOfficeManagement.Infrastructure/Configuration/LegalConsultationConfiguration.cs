using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class LegalConsultationConfiguration : IEntityTypeConfiguration<LegalConsultation>
    {
        public void Configure(EntityTypeBuilder<LegalConsultation> builder)
        {
            // Table name
            builder.ToTable("LegalConsultations");

            // Primary Key
            builder.HasKey(lc => lc.Id);

            // Properties Configuration
            builder.Property(lc => lc.CustomerName)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(lc => lc.MobileNumber)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(lc => lc.MobileNumber2)
                .HasMaxLength(15)
                .IsRequired(false);

            builder.Property(lc => lc.Email)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(lc => lc.Subject)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(lc => lc.Details)
                .IsRequired()
                .HasColumnType("nvarchar(MAX)");

            builder.Property(lc => lc.ConsultationType)
                .HasMaxLength(50)
                .IsRequired()
                .HasDefaultValue("مكتبية");

            builder.Property(lc => lc.Notes)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(lc => lc.UrlLegalConsultation)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(lc => lc.UrlLegalConsultationInvoice)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(lc => lc.Status)
                .HasMaxLength(50)
                .IsRequired(false)
                .HasDefaultValue("مكتملة");

            // Relationships
            // Relationship with Lawyer
            builder.HasOne(lc => lc.Lawyer)
                .WithMany(l => l.LegalConsultations)
                .HasForeignKey(lc => lc.LawyerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship with ServiceOffice
            builder.HasOne(lc => lc.ServiceOffice)
                .WithMany(so => so.legalConsultations)
                .HasForeignKey(lc => lc.ServiceOfficeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(lc => lc.LawyerId);
            builder.HasIndex(lc => lc.ServiceOfficeId);
            builder.HasIndex(lc => lc.ConsultationType);
            builder.HasIndex(lc => lc.Status);
            builder.HasIndex(lc => lc.CustomerName);
            builder.HasIndex(lc => lc.MobileNumber);

            // Timestamps
            builder.Property(lc => lc.CreatedAt)
                .HasDefaultValueSql("GETDATE()");

           
        }
    }
}