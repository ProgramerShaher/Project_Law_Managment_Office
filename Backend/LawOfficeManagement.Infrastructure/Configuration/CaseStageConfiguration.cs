using LawOfficeManagement.Core.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class CaseStageConfiguration : IEntityTypeConfiguration<CaseStage>
    {
        public void Configure(EntityTypeBuilder<CaseStage> builder)
        {
            // اسم الجدول
            builder.ToTable("CaseStages", "Cases");

            builder.HasKey(cs => cs.Id);

            builder.Property(cs => cs.Stage)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(cs => cs.Priority)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(cs => cs.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(cs => cs.EndDateStage)
                .IsRequired(false);

            builder.HasOne(cs => cs.Case)
                .WithMany(c => c.CaseStages)
                .HasForeignKey(cs => cs.CaseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(cs => cs.CaseId);
            builder.HasIndex(cs => cs.IsActive);
            // لـ SQL Server
            // أو .HasDefaultValueSql("CURRENT_TIMESTAMP"); // لـ MySQL



        }
    }
}