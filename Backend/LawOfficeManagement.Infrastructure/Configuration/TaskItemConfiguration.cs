using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
    {
        public void Configure(EntityTypeBuilder<TaskItem> builder)
        {
            // اسم الجدول
            builder.ToTable("TaskItems");

            // المفتاح الأساسي
            builder.HasKey(t => t.Id);

            // الخصائص المطلوبة
            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            // الخصائص الاختيارية
            builder.Property(t => t.Description)
                .IsRequired(false)
                .HasMaxLength(2000);

            builder.Property(t => t.StartDate)
                .IsRequired(false)
                .HasColumnType("datetime2");

            builder.Property(t => t.DueDate)
                .IsRequired(false)
                .HasColumnType("datetime2");

            builder.Property(t => t.CompletedAt)
                .IsRequired(false)
                .HasColumnType("datetime2");

            builder.Property(t => t.CreatedById)
                .IsRequired(false);

            builder.Property(t => t.CaseTeamId)
                .IsRequired(false);

            builder.Property(t => t.CaseId)
                .IsRequired(false);

            // تحويل الأنماط
            builder.Property(t => t.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired()
                .HasDefaultValue(TaskStatu.Pending);

            builder.Property(t => t.Priority)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired()
                .HasDefaultValue(TaskPriority.Normal);

            // العلاقات
            builder.HasOne(t => t.CreatedBy)
                .WithMany()
                .HasForeignKey(t => t.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.CaseTeam)
                .WithMany()
                .HasForeignKey(t => t.CaseTeamId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Case)
                .WithMany()
                .HasForeignKey(t => t.CaseId)
                .OnDelete(DeleteBehavior.Restrict);

            // العلاقات مع المجموعات
            builder.HasMany(t => t.Comments)
                .WithOne(tc => tc.TaskItem)
                .HasForeignKey(tc => tc.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(t => t.Documents)
                .WithOne(td => td.TaskItem)
                .HasForeignKey(td => td.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // الفهارس
            builder.HasIndex(t => t.CreatedById);
            builder.HasIndex(t => t.CaseTeamId);
            builder.HasIndex(t => t.CaseId);
            builder.HasIndex(t => t.Status);
            builder.HasIndex(t => t.Priority);
            builder.HasIndex(t => t.StartDate);
            builder.HasIndex(t => t.DueDate);
            builder.HasIndex(t => t.CompletedAt);
            builder.HasIndex(t => new { t.CaseId, t.Status });
            builder.HasIndex(t => new { t.CaseTeamId, t.DueDate });
            builder.HasIndex(t => new { t.Status, t.DueDate });

            // القيم الافتراضية
          
        }
    }
}