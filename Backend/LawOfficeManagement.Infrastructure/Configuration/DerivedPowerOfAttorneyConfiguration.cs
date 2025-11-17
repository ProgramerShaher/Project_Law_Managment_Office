using LawOfficeManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class DerivedPowerOfAttorneyConfiguration : IEntityTypeConfiguration<DerivedPowerOfAttorney>
    {
        public void Configure(EntityTypeBuilder<DerivedPowerOfAttorney> builder)
        {
            builder.ToTable("DerivedPowerOfAttorneys");

            builder.HasKey(d => d.Id);

            // 🧱 الخصائص الأساسية
            builder.Property(d => d.DerivedNumber)
                .HasMaxLength(50);

            builder.Property(d => d.AuthorityScope)
                .HasMaxLength(300);

            builder.Property(d => d.Notes)
                .HasMaxLength(500);

            builder.Property(d => d.IssueDate)
                .IsRequired();

            builder.Property(d => d.ExpiryDate)
                .IsRequired(false);

            builder.Property(d => d.IsActive)
                .HasDefaultValue(true);

            builder.Property(d => d.Derived_Document_Agent_Url)
                .IsRequired()
                .HasMaxLength(500);

            // 👥 العلاقات

            // العلاقة مع الوكالة الأصلية
            builder.HasOne(d => d.ParentPowerOfAttorney)
                .WithMany(p => p.DerivedPowerOfAttorneys)
                .HasForeignKey(d => d.ParentPowerOfAttorneyId)
                .OnDelete(DeleteBehavior.Cascade);

            // العلاقة مع المحامي
            builder.HasOne(d => d.Lawyer)
                .WithMany()
                .HasForeignKey(d => d.LawyerId)
                .OnDelete(DeleteBehavior.Restrict);

            // بيانات الوقت
            builder.Property(d => d.CreatedAt).HasColumnType("datetime");
        }
    }
}
