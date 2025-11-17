using LawOfficeManagement.Core.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class CaseWitnessConfiguration : IEntityTypeConfiguration<CaseWitness>
    {
        public void Configure(EntityTypeBuilder<CaseWitness> builder)
        {
            // اسم الجدول
            builder.ToTable("CaseWitnesses", "Cases");

            // المفتاح الأساسي
            builder.HasKey(cw => cw.Id);

            // الخصائص المطلوبة
            builder.Property(cw => cw.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(cw => cw.CaseId)
                .IsRequired();

            // الخصائص الاختيارية مع أطوال قصوى
            builder.Property(cw => cw.NationalId)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(cw => cw.PhoneNumber)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(cw => cw.Address)
                .HasMaxLength(200)
                .IsRequired(false);

            builder.Property(cw => cw.TestimonySummary)
                .HasMaxLength(2000)
                .IsRequired(false);

            builder.Property(cw => cw.Notes)
                .HasMaxLength(500)
                .IsRequired(false);

            // الخصائص المنطقية مع قيم افتراضية
            builder.Property(cw => cw.IsAttended)
                .IsRequired()
                .HasDefaultValue(false);

            // التواريخ الاختيارية
            builder.Property(cw => cw.TestimonyDate)
                .IsRequired(false);

            builder.Property(cw => cw.CaseSessionId)
                .IsRequired(false);

            // العلاقات
            builder.HasOne(cw => cw.Case)
                .WithMany(c => c.CaseWitnesses)
                .HasForeignKey(cw => cw.CaseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cw => cw.CaseSession)
                .WithMany(cs => cs.CaseWitnesses)
                .HasForeignKey(cw => cw.CaseSessionId)
                .OnDelete(DeleteBehavior.SetNull);

            // الفهارس
            builder.HasIndex(cw => cw.CaseId);
            builder.HasIndex(cw => cw.CaseSessionId);
            builder.HasIndex(cw => cw.NationalId);
            builder.HasIndex(cw => cw.FullName);
            builder.HasIndex(cw => cw.IsAttended);
            builder.HasIndex(cw => cw.TestimonyDate);
            builder.HasIndex(cw => new { cw.CaseId, cw.IsAttended });
            builder.HasIndex(cw => new { cw.CaseSessionId, cw.IsAttended });
            builder.HasIndex(cw => new { cw.NationalId, cw.CaseId }).IsUnique();



        }
    }
}