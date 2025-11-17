using LawOfficeManagement.Core.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class CaseTeamConfiguration : IEntityTypeConfiguration<CaseTeam>
    {
        [Obsolete]
        public void Configure(EntityTypeBuilder<CaseTeam> builder)
        {
            // اسم الجدول
            builder.ToTable("CaseTeams");

            // المفتاح الأساسي
            builder.HasKey(ct => ct.Id);

            // الخصائص المطلوبة
            builder.Property(ct => ct.LawyerId)
                .IsRequired();

            builder.Property(ct => ct.CaseId)
                .IsRequired();

            builder.Property(ct => ct.Role)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValue("مساعد");

            builder.Property(ct => ct.StartDate)
                .IsRequired()
                .HasColumnType("datetime2")
                .HasDefaultValueSql("GETDATE()");

          

            builder.Property(ct => ct.EndDate)
                .IsRequired(false)
                .HasColumnType("datetime2");

            // الخصائص المنطقية
            builder.Property(ct => ct.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // العلاقات
            builder.HasOne(ct => ct.Lawyer)
                .WithMany()
                .HasForeignKey(ct => ct.LawyerId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(ct => ct.DerivedPowerOfAttorney)
            //    .WithMany()
            //    .HasForeignKey(ct => ct.DerivedPowerOfAttorneyId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ct => ct.Case)
                .WithMany(c => c.CaseTeams)
                .HasForeignKey(ct => ct.CaseId)
                .OnDelete(DeleteBehavior.Restrict);


            // العلاقات مع المجموعات
            builder.HasMany(ct => ct.TaskItems)
                .WithOne(ti => ti.CaseTeam)
                .HasForeignKey(ti => ti.CaseTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            // الفهرس الفريد (محامي واحد لكل قضية في نفس الوقت)
            builder.HasIndex(ct => new { ct.LawyerId, ct.CaseId })
                .IsUnique();

            // الفهارس الأخرى
            builder.HasIndex(ct => ct.CaseId);
            builder.HasIndex(ct => ct.LawyerId);
            builder.HasIndex(ct => ct.Role);
            builder.HasIndex(ct => ct.IsActive);
            builder.HasIndex(ct => ct.StartDate);
            builder.HasIndex(ct => ct.EndDate);
            builder.HasIndex(ct => new { ct.CaseId, ct.IsActive });
            builder.HasIndex(ct => new { ct.LawyerId, ct.IsActive });

          

            // القيم الافتراضية
          

            // قيود التحقق
            builder.HasCheckConstraint("CK_CaseTeam_EndDate",
                "[EndDate] IS NULL OR [EndDate] > [StartDate]");

            // بيانات أولية (Seed Data) - اختياري
        
        }
    }
}