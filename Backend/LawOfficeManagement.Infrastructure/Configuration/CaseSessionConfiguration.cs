using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class CaseSessionConfiguration : IEntityTypeConfiguration<CaseSession>
    {
        public void Configure(EntityTypeBuilder<CaseSession> builder)
        {
            // المفتاح الأساسي
            builder.HasKey(cs => cs.Id);

            // الخصائص المطلوبة
            builder.Property(cs => cs.SessionDate)
                .IsRequired();

            builder.Property(cs => cs.SessionStatus)
                .IsRequired() // ✅ هذا صحيح لأن SessionStatus ليس nullable
                .HasConversion<string>() // تحويل الـ enum إلى string في DB
                .HasMaxLength(50);

            // الخصائص الاختيارية
            builder.Property(cs => cs.SessionTime)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(cs => cs.SessionNumber)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(cs => cs.SessionType)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(cs => cs.Location)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(cs => cs.Notes)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(cs => cs.Decision)
                .HasMaxLength(2000)
                .IsRequired(false);

            // العلاقات
            builder.HasOne(cs => cs.Case)
    .WithMany(c => c.CaseSessions)
    .HasForeignKey(cs => cs.CaseId)
    .OnDelete(DeleteBehavior.Restrict)
    .IsRequired(false);

            builder.HasOne(cs => cs.Court)
                .WithMany(c => c.CaseSessions)
                .HasForeignKey(cs => cs.CourtId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cs => cs.CourtDivision)
                .WithMany(cd => cd.CaseSessions)
                .HasForeignKey(cs => cs.CourtDivisionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cs => cs.AssignedLawyer)
                .WithMany(l => l.CaseSessions)
                .HasForeignKey(cs => cs.AssignedLawyerId)
                .OnDelete(DeleteBehavior.Restrict);


            // العلاقات مع الأدلة والشهود
            builder.HasMany(cs => cs.CaseEvidences)
                .WithOne(ce => ce.CaseSession)
                .HasForeignKey(ce => ce.CaseSessionId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder.HasMany(cs => cs.CaseWitnesses)
                .WithOne(cw => cw.CaseSession)
                .HasForeignKey(cw => cw.CaseSessionId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            // الفهرس على تاريخ الجلسة
            builder.HasIndex(cs => cs.SessionDate);

            // الفهرس على حالة الجلسة
            builder.HasIndex(cs => cs.SessionStatus);
        }
    }
}