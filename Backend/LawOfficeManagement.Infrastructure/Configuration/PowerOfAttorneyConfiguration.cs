using LawOfficeManagement.Core.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class PowerOfAttorneyConfiguration : IEntityTypeConfiguration<PowerOfAttorney>
    {
        public void Configure(EntityTypeBuilder<PowerOfAttorney> builder)
        {
            builder.ToTable("PowerOfAttorneys");

            builder.HasKey(p => p.Id);

            // 🧱 الخصائص الأساسية
            builder.Property(p => p.AgencyNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.IssueDate)
                .IsRequired();

            builder.Property(p => p.ExpiryDate)
                .IsRequired(false);

            builder.Property(p => p.IssuingAuthority)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.AgencyType)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.DerivedPowerOfAttorney)
                .HasDefaultValue(false);

            builder.Property(p => p.Status)
                .HasConversion<string>() // يخزن النص بدلاً من القيمة الرقمية
                .HasDefaultValue(AgencyStatus.Active)
                .IsRequired();

            builder.Property(p => p.Document_Agent_Url)
                .IsRequired()
                .HasMaxLength(500);

            // 👥 العلاقات

            // العلاقة مع العميل
            builder.HasOne(p => p.Client)
                .WithMany()
                .HasForeignKey(p => p.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // العلاقة مع المكتب
            builder.HasOne(p => p.Office)
                .WithMany()
                .HasForeignKey(p => p.OfficeID)
                .OnDelete(DeleteBehavior.Restrict);

            // العلاقة مع المحامي
            builder.HasOne(p => p.Lawyer)
                .WithMany()
                .HasForeignKey(p => p.LawyerID)
                .OnDelete(DeleteBehavior.Restrict);

            // العلاقة مع الوكالات المشتقة
            builder.HasMany(p => p.DerivedPowerOfAttorneys)
                .WithOne(d => d.ParentPowerOfAttorney)
                .HasForeignKey(d => d.ParentPowerOfAttorneyId)
                .OnDelete(DeleteBehavior.Cascade);

            // بيانات الوقت
            builder.Property(p => p.CreatedAt).HasColumnType("datetime");
        }
    }
}
