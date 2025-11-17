using LawOfficeManagement.Core.Entities.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        [Obsolete]
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            // اسم الجدول
            builder.ToTable("Contracts" );

            // المفتاح الأساسي
            builder.HasKey(c => c.Id);

            // الخصائص المطلوبة
            builder.Property(c => c.ContractNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(c => c.ContractDescription)
                .IsRequired();

            builder.Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.StartDate)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(c => c.ClientId)
                .IsRequired();

            builder.Property(c => c.CaseId)
                .IsRequired();

            // الخصائص الاختيارية
            builder.Property(c => c.EndDate)
                .IsRequired(false)
                .HasColumnType("datetime2");

            builder.Property(c => c.TotalCaseAmount)
                .IsRequired(false)
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Percentage)
                .IsRequired(false);

            builder.Property(c => c.FinalAgreedAmount)
                .IsRequired(false)
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.ContractDocumentUrl)
                .IsRequired(false)
                .HasMaxLength(500);

            // تحويل الأنماط
            builder.Property(c => c.Status)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired()
                .HasDefaultValue(ContractStatus.Active);

            builder.Property(c => c.FinancialAgreementType)
                .HasConversion<string>()
                .HasMaxLength(50)
                .IsRequired();

            // العلاقات
            builder.HasOne(c => c.Client)
                .WithMany()
                .HasForeignKey(c => c.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // علاقة واحد إلى واحد مع القضية
            builder.HasOne(c => c.Case)
                .WithOne(c => c.Contract)
                .HasForeignKey<Contract>(c => c.CaseId)
                .OnDelete(DeleteBehavior.Restrict);

            // تجاهل الخاصية المحسوبة
            builder.Ignore(c => c.CalculatedAmount);

            // الفهارس
            builder.HasIndex(c => c.ContractNumber).IsUnique();
            builder.HasIndex(c => c.ClientId);
            builder.HasIndex(c => c.CaseId).IsUnique(); // فريد لأنها علاقة واحد لواحد
            builder.HasIndex(c => c.Status);
            builder.HasIndex(c => c.FinancialAgreementType);
            builder.HasIndex(c => c.StartDate);
            builder.HasIndex(c => c.EndDate);
            builder.HasIndex(c => new { c.Status, c.StartDate });

            
            // قيود التحقق
            builder.HasCheckConstraint("CK_Contract_EndDate",
                "[EndDate] IS NULL OR [EndDate] > [StartDate]");

            builder.HasCheckConstraint("CK_Contract_Percentage",
                "[Percentage] IS NULL OR ([Percentage] >= 0 AND [Percentage] <= 100)");

         
        }
    }
}