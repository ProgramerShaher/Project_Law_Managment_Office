using LawOfficeManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class OpponentConfiguration : IEntityTypeConfiguration<Opponent>
    {
        public void Configure(EntityTypeBuilder<Opponent> builder)
        {
            builder.ToTable("Opponents");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.OpponentName)
                .HasMaxLength(200)
                .IsRequired(false)
                .HasComment("اسم الخصم");

            builder.Property(o => o.OpponentMobile) // تم التصحيح
                .HasMaxLength(20)
                .IsRequired(false)
                .HasComment("رقم جوال الخصم");

            builder.Property(o => o.OpponentAddress)
                .HasMaxLength(500)
                .IsRequired(false)
                .HasComment("عنوان الخصم");

            builder.Property(o => o.Type) // تم التصحيح
                .IsRequired()
                .HasConversion<int>()
                .HasComment("نوع الخصم");

            builder.Property(o => o.OpponentLawyer)
                .HasMaxLength(200)
                .IsRequired(false)
                .HasComment("محامي الخصم");

           

            builder.Property(o => o.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            // Indexes
            builder.HasIndex(o => o.OpponentName)
                .HasDatabaseName("IX_Opponents_Name");

            builder.HasIndex(o => o.OpponentMobile) 
                .HasDatabaseName("IX_Opponents_Mobile");

            builder.HasIndex(o => o.Type) 
                .HasDatabaseName("IX_Opponents_Type");

        }

    }
}