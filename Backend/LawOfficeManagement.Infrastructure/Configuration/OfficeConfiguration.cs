using LawOfficeManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class OfficeConfiguration : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            builder.ToTable("Offices");

            builder.Property(o => o.OfficeName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(o => o.ManagerName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(o => o.Address)
                .IsRequired()
                .HasMaxLength(500); 

            builder.Property(o => o.WebSitUrl)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(o => o.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(o => o.Email)
                .HasMaxLength(200);

            builder.Property(o => o.LicenseNumber)
                .HasMaxLength(100);
        }
    }
}
//using LawOfficeManagement.Core.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace LawOfficeManagement.Infrastructure.Configurations
//{
//    public class OfficeConfiguration : IEntityTypeConfiguration<Office>
//    {
//        public void Configure(EntityTypeBuilder<Office> builder)
//        {
//            builder.ToTable("Offices");

//            builder.Property(o => o.OfficeName)
//                .IsRequired()
//                .HasMaxLength(200);

//            builder.Property(o => o.Address)
//                .HasMaxLength(300);

//            builder.Property(o => o.PhoneNumber)
//                .HasMaxLength(20);

//            builder.Property(o => o.Email)
//                .HasMaxLength(150);

//            builder.Property(o => o.LicenseNumber)
//                .HasMaxLength(100);

//            builder.Property(o => o.EstablishmentDate)
//                .IsRequired(false);
//        }
//    }
//}
