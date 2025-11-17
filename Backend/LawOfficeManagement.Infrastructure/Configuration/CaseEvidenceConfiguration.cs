using LawOfficeManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class CaseEvidenceConfiguration : IEntityTypeConfiguration<CaseEvidence>
    {
        public void Configure(EntityTypeBuilder<CaseEvidence> builder)
        {
            // اسم الجدول
            builder.ToTable("CaseEvidences", "Cases");

            // المفتاح الأساسي
            builder.HasKey(ce => ce.Id);

            // الخصائص المطلوبة
            builder.Property(ce => ce.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(ce => ce.CaseId)
                .IsRequired();

            // الخصائص الاختيارية مع أطوال قصوى
            builder.Property(ce => ce.EvidenceType)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(ce => ce.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(ce => ce.Source)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(ce => ce.CourtNotes)
                .HasMaxLength(500)
                .IsRequired(false);

            // الخصائص المنطقية مع قيم افتراضية
            builder.Property(ce => ce.IsAccepted)
                .IsRequired()
                .HasDefaultValue(false);

            // التواريخ الاختيارية
            builder.Property(ce => ce.SubmissionDate)
                .IsRequired(false);

            builder.Property(ce => ce.CaseSessionId)
                .IsRequired(false);

            // العلاقات
            builder.HasOne(ce => ce.CaseSession)
            .WithMany(cs => cs.CaseEvidences)
             .HasForeignKey(ce => ce.CaseSessionId)
            .OnDelete(DeleteBehavior.SetNull) // إذا حذفت الجلسة، اجعل CaseSessionId = null
            .IsRequired(false);


            builder.HasOne(ce => ce.CaseSession)
                .WithMany(cs => cs.CaseEvidences)
                .HasForeignKey(ce => ce.CaseSessionId)
                .OnDelete(DeleteBehavior.Restrict);

            // الفهارس
            builder.HasIndex(ce => ce.CaseId);
            builder.HasIndex(ce => ce.CaseSessionId);
            builder.HasIndex(ce => ce.EvidenceType);
            builder.HasIndex(ce => ce.IsAccepted);
            builder.HasIndex(ce => ce.SubmissionDate);
            builder.HasIndex(ce => new { ce.CaseId, ce.IsAccepted });
            builder.HasIndex(ce => new { ce.CaseSessionId, ce.EvidenceType });

           

           
        }
    }
}