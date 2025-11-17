using LawOfficeManagement.Core.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class TaskDocumentConfiguration : IEntityTypeConfiguration<TaskDocument>
    {
        public void Configure(EntityTypeBuilder<TaskDocument> builder)
        {
            // اسم الجدول
            builder.ToTable("TaskDocuments");

            // المفتاح الأساسي
            builder.HasKey(td => td.Id);

            // الخصائص المطلوبة
            builder.Property(td => td.TaskItemId)
                .IsRequired();

            builder.Property(td => td.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(td => td.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            // الخصائص الاختيارية
            builder.Property(td => td.FileType)
                .IsRequired(false)
                .HasMaxLength(100);

            // العلاقات
            builder.HasOne(td => td.TaskItem)
                .WithMany(t => t.Documents)
                .HasForeignKey(td => td.TaskItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // الفهارس
            builder.HasIndex(td => td.TaskItemId);
            builder.HasIndex(td => td.FileType);
            builder.HasIndex(td => new { td.TaskItemId, td.FileType });

     
        }
    }
}