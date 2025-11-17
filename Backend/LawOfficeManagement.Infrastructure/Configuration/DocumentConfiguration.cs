using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Documents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");

            builder.Property(d => d.DocumentName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(d => d.FileExtension)
                .HasMaxLength(10);

            builder.Property(d => d.FileUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(d => d.DocumentType)
                .IsRequired();

            builder.Property(d => d.EntityOwnerType)
                .IsRequired();
        }
    }
}
