using LawOfficeManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class ClientRoleConfiguration : IEntityTypeConfiguration<ClientRole>
    {
        public void Configure(EntityTypeBuilder<ClientRole> builder)
        {
            builder.ToTable("ClientRoles");
            builder.HasIndex(e => e.Name);
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
