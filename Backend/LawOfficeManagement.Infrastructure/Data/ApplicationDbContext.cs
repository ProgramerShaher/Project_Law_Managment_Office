using LawOfficeManagement.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims; 
using Microsoft.AspNetCore.Http;
using LawOfficeManagement.Core.Entities.Cases;
using LawOfficeManagement.Core.Entities.Documents;
using LawOfficeManagement.Core.Entities.Contracts;

namespace LawOfficeManagement.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor; // حقن للوصول إلى المستخدم الحالي

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            IHttpContextAccessor httpContextAccessor) : base(options) // أضف IHttpContextAccessor
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientRole> ClientRoles { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<CourtType> CourtTypes { get; set; }
        public DbSet<CourtDivision> CourtDivisions { get; set; }
        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<Office> Office { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<PowerOfAttorney> PowerOfAttorneys { get; set; }
        public DbSet<DerivedPowerOfAttorney> DerivedPowerOfAttorneys { get; set; }
        public DbSet<Opponent> opponents { get; set; }
        public DbSet<CaseTeam> caseTeams { get; set; }
        public DbSet<Case> cases { get; set; }
        public DbSet<CaseStage> caseStages { get; set; }
        public DbSet<CaseType> caseTypes { get; set; }
        public DbSet<CaseDocument> caseDocuments { get; set; }
        public DbSet<LegalConsultation> legalConsultations { get; set; }
        public DbSet<ServiceOffice> serviceOffices { get; set; }
        private DbSet<Contract> contracts { get; set; }
        private DbSet<TaskDocument > taskDocuments { get; set; }
        private DbSet<TaskItem> taskItems { get; set; }
        private DbSet<TaskComment> taskComments { get; set; }
        private DbSet<CaseSession> caseSessions { get; set; }
        private DbSet<CaseEvidence> caseEvidences { get; set; }
        private DbSet<CaseWitness> caseWitnesses { get; set; }


      

        //   public DbSet<AgentClient> AgentClient { get; set; }



        // переопределение метода сохранения для автоматического заполнения полей аудита
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var currentUserId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow;
                        entry.Entity.CreatedBy = currentUserId ?? "System"; // تعيين المستخدم الحالي أو "System"
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedAt = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = currentUserId ?? "System"; // تعيين المستخدم الحالي
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
