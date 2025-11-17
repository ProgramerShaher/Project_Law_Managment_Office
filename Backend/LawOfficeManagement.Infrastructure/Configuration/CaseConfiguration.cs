using LawOfficeManagement.Core.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class CaseConfiguration : IEntityTypeConfiguration<Case>
    {
        public void Configure(EntityTypeBuilder<Case> builder)
        {
            // اسم الجدول
            builder.ToTable("Cases", "Cases");

            // المفتاح الأساسي
            builder.HasKey(c => c.Id);

            // الخصائص المطلوبة
            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.CaseNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.FilingDate)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(c => c.ClientId)
                .IsRequired();

            builder.Property(c => c.OpponentId)
                .IsRequired();

            builder.Property(c => c.CourtId)
                .IsRequired();

            builder.Property(c => c.CourtDivisionId)
                .IsRequired();

            builder.Property(c => c.CaseTypeId)
                .IsRequired();

            builder.Property(c => c.CourtTypeId)
                .IsRequired();

            // Enum Conversion
            var converter = new EnumToStringConverter<CaseStatus>();
            builder.Property(c => c.Status)
                .HasConversion(converter)
                .HasMaxLength(50)
                .IsRequired();

            // الخصائص الاختيارية مع أطوال قصوى
            builder.Property(c => c.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(c => c.CaseNumberProsecution)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(c => c.CaseNumberInPolice)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(c => c.InternalReference)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(c => c.Notes)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(c => c.Outcome)
                .HasMaxLength(200)
                .IsRequired(false);

            // التواريخ الاختيارية
            builder.Property(c => c.FirstSessionDate)
                .IsRequired(false)
                .HasColumnType("datetime2");

            builder.Property(c => c.PowerOfAttorneyId)
                .IsRequired(false);

            // الخصائص المنطقية مع قيم افتراضية
            builder.Property(c => c.IsArchived)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(c => c.IsConfidential)
                .IsRequired()
                .HasDefaultValue(false);

            // العلاقات
            builder.HasOne(c => c.Client)
                .WithMany()
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Opponents)
                .WithMany()
                .HasForeignKey(c => c.OpponentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Court)
                .WithMany()
                .HasForeignKey(c => c.CourtId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.CourtDivision)
                .WithMany()
                .HasForeignKey(c => c.CourtDivisionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.CourtType)
                .WithMany()
                .HasForeignKey(c => c.CourtTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.CaseType)
                .WithMany(ct => ct.Cases)
                .HasForeignKey(c => c.CaseTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.PowerOfAttorney)
                .WithMany()
                .HasForeignKey(c => c.PowerOfAttorneyId)
                .OnDelete(DeleteBehavior.SetNull);

            // العلاقات مع المجموعات
            builder.HasMany(c => c.CaseStages)
                .WithOne(cs => cs.Case)
                .HasForeignKey(cs => cs.CaseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.CaseSessions)
                .WithOne(cs => cs.Case)
                .HasForeignKey(cs => cs.CaseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.CaseEvidences)
                .WithOne(ce => ce.Case)
                .HasForeignKey(ce => ce.CaseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.CaseWitnesses)
                .WithOne(cw => cw.Case)
                .HasForeignKey(cw => cw.CaseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.CaseTeams)
                .WithOne(ct => ct.Case)
                .HasForeignKey(ct => ct.CaseId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.CaseDocuments)
                .WithOne(cd => cd.Case)
                .HasForeignKey(cd => cd.CaseId)
                .OnDelete(DeleteBehavior.Cascade);

            // الفهارس
            builder.HasIndex(c => c.CaseNumber).IsUnique();
            builder.HasIndex(c => c.ClientId);
            builder.HasIndex(c => c.OpponentId);
            builder.HasIndex(c => c.CourtId);
            builder.HasIndex(c => c.CourtDivisionId);
            builder.HasIndex(c => c.CaseTypeId);
            builder.HasIndex(c => c.CourtTypeId);
            builder.HasIndex(c => c.Status);
            builder.HasIndex(c => c.FilingDate);
            builder.HasIndex(c => c.FirstSessionDate);
            builder.HasIndex(c => c.IsArchived);
            builder.HasIndex(c => c.IsConfidential);
            builder.HasIndex(c => c.PowerOfAttorneyId);
            builder.HasIndex(c => new { c.Status, c.IsArchived });
            builder.HasIndex(c => new { c.CaseTypeId, c.FilingDate });
            builder.HasIndex(c => new { c.ClientId, c.Status });

            // الفهرس الفريد للرقم الداخلي إذا كان موجوداً
            builder.HasIndex(c => c.InternalReference)
                .IsUnique()
                .HasFilter("[InternalReference] IS NOT NULL");

          

            // Ignore computed property
            builder.Ignore(c => c.PrincipalMandator);

            // بيانات أولية (Seed Data) - اختياري
          
        }
    }
}