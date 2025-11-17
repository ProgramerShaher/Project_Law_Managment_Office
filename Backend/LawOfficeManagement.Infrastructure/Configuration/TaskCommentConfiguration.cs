using LawOfficeManagement.Core.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class TaskCommentConfiguration : IEntityTypeConfiguration<TaskComment>
    {
        public void Configure(EntityTypeBuilder<TaskComment> builder)
        {
            // اسم الجدول
            builder.ToTable("TaskComments");

            // المفتاح الأساسي
            builder.HasKey(tc => tc.Id);

            // الخصائص المطلوبة
            builder.Property(tc => tc.Content)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(tc => tc.CreatedById)
                .IsRequired();

            builder.Property(tc => tc.TaskItemId)
                .IsRequired();

            // العلاقات
            builder.HasOne(tc => tc.TaskItem)
                .WithMany(t => t.Comments)
                .HasForeignKey(tc => tc.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tc => tc.CreatedBy)
                .WithMany()
                .HasForeignKey(tc => tc.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // الفهارس
            builder.HasIndex(tc => tc.TaskItemId);
            builder.HasIndex(tc => tc.CreatedById);
            builder.HasIndex(tc => tc.CreatedAt);

            // القيم الافتراضية
            builder.Property(tc => tc.CreatedAt)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}