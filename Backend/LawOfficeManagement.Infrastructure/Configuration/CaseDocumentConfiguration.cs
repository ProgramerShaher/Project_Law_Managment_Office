using LawOfficeManagement.Core.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class CaseDocumentConfiguration : IEntityTypeConfiguration<CaseDocument>
    {
        public void Configure(EntityTypeBuilder<CaseDocument> builder)
        {
            // اسم الجدول في قاعدة البيانات
            builder.ToTable("CaseDocuments", "Cases");

            // المفتاح الأساسي (من BaseEntity غالباً هو Id)
            builder.HasKey(cd => cd.Id);

            // العلاقة: كل مرفق مرتبط بقضية واحدة
            builder.HasOne(cd => cd.Case)
                   .WithMany(c => c.CaseDocuments) // تأكد أن لديك ICollection<CaseDocument> في كيان Case
                   .HasForeignKey(cd => cd.CaseId)
                   .OnDelete(DeleteBehavior.Cascade);

            // الحقول المطلوبة والقيود
            builder.Property(cd => cd.FileName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(cd => cd.FilePath)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(cd => cd.FileType)
                   .HasMaxLength(100);

          // تعيين التاريخ الافتراضي في SQL Server
        }
    }
}
