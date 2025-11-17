using LawOfficeManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    /// <summary>
    /// إعدادات تكوين الجدول (Lawyers) في قاعدة البيانات.
    /// </summary>
    public class LawyerConfiguration : IEntityTypeConfiguration<Lawyer>
    {
        public void Configure(EntityTypeBuilder<Lawyer> builder)
        {
            builder.ToTable("Lawyers");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(l => l.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasMany(l => l.CaseTeams)
    .WithOne(ct => ct.Lawyer)
    .HasForeignKey(ct => ct.LawyerId)
    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(l => l.Address)
                .HasMaxLength(300);

            builder.Property(l => l.IdentityImagePath)
                .HasMaxLength(300);

            builder.Property(l => l.QualificationDocumentsPath)
                .HasMaxLength(300);

            builder.Property(l => l.Email)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(l => l.Type)
                .HasConversion<int>(); // يخزن النوع كقيمة رقمية في قاعدة البيانات

          
        }
    }
}
