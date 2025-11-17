using LawOfficeManagement.Core.Entities;
using LawOfficeManagement.Core.Entities.Cases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LawOfficeManagement.Infrastructure.Configuration
{
    public class AgentClientConfiguration : IEntityTypeConfiguration<AgentClient>
    {
        public void Configure(EntityTypeBuilder<AgentClient> builder)
        {
            // اسم الجدول في قاعدة البيانات
            builder.ToTable("AgentClients");

            // المفتاح الأساسي (من BaseEntity أو تضيفه هنا)
            builder.HasKey(ac => ac.Id);

            // خصائص الحقول الأساسية
            builder.Property(ac => ac.ClientId)
                   .IsRequired();

            builder.Property(ac => ac.PowerOfAttorneyId)
                   .IsRequired();

            // العلاقات (اختياري — أضفها إذا كانت موجودة في الكيانات الأخرى)
            builder.HasOne<Client>() // الكيان Client
                   .WithMany()       // أو .WithMany(c => c.AgentClients) إن وُجدت قائمة في Client
                   .HasForeignKey(ac => ac.ClientId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<PowerOfAttorney>() // الكيان PowerOfAttorney
                   .WithMany()                 // أو .WithMany(p => p.AgentClients)
                   .HasForeignKey(ac => ac.PowerOfAttorneyId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
