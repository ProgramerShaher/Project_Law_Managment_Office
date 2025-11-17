using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace LawOfficeManagement.Infrastructure.Data
{
    // هذه الفئة مخصصة فقط لأدوات وقت التصميم (مثل Add-Migration, Update-Database)
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // 1. بناء كائن IConfiguration للوصول إلى appsettings.json
            IConfigurationRoot configuration = new ConfigurationBuilder()
                // تحديد المسار إلى مشروع الـ API حيث يوجد ملف appsettings.json
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../LawOfficeManagement.API"))
                .AddJsonFile("appsettings.json")
                .Build();

            // 2. إنشاء DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

            // 3. قراءة سلسلة الاتصال من ملف الإعدادات
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);

            // 4. إنشاء نسخة من DbContext
            // ملاحظة: نحن نمرر null لـ IHttpContextAccessor لأننا لا نحتاجه في وقت التصميم
            return new ApplicationDbContext(builder.Options, null);
        }
    }
}
