using LawOfficeManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Metadata.Ecma335;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasIndex(e => e.FullName);
            builder.HasIndex(e => e.UrlImageNationalId);

            // Unique phone number excluding nulls and soft-deleted rows
            builder.HasIndex(e => e.PhoneNumber)
                   .IsUnique()
                   .HasFilter("[PhoneNumber] IS NOT NULL AND [IsDeleted] = 0");

            // Unique email excluding nulls and soft-deleted rows
            builder.HasIndex(e => e.Email)
                   .IsUnique()
                   .HasFilter("[Email] IS NOT NULL AND [IsDeleted] = 0");

            // ... إعدادات أخرى لـ Client

            // تكوين Address ككائن مملوك (Owned)
            //builder.OwnsOne(c => c.Address, addressBuilder =>
            //{
            //    // يمكن تحديد أسماء الأعمدة في قاعدة البيانات إذا أردت
            //    addressBuilder.Property(a => a.Country).HasColumnName("Address_Country");
            //    addressBuilder.Property(a => a.City).HasColumnName("Address_City");
            //    addressBuilder.Property(a => a.Street).HasColumnName("Address_Street");
            //    addressBuilder.Property(a => a.PostalCode).HasColumnName("Address_PostalCode");
            //});
        }

    }

}

