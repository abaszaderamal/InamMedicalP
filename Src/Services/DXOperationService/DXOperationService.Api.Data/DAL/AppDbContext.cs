using System.Security.Claims;
using Med.Shared.Abstracts;
using Med.Shared.Entities;
using Med.Shared.ModelConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DXOperationService.Api.Data.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //add-migration AppUser  -p DXOperationService.Api.Data -o DAL/Migrations

        private readonly string _username;
        private readonly string _userId;


        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            var claimsPrincipal = httpContextAccessor.HttpContext?.User;

            _userId = claimsPrincipal?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unauthenticated user";
            _username = claimsPrincipal?.FindFirst(ClaimTypes.Name)?.Value ?? "Unauthenticated user";
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Configuration

            builder.ApplyConfiguration(new AppUserConfig());
            builder.ApplyConfiguration(new ClinicConfig());
            builder.ApplyConfiguration(new ClinicDoctorConfig());
            builder.ApplyConfiguration(new DXOperationConfig());
            builder.ApplyConfiguration(new DXOperationMedicineConfig());
            builder.ApplyConfiguration(new DoctorConfig());
            builder.ApplyConfiguration(new EvaluationConfig());
            builder.ApplyConfiguration(new EvaluationRaitingConfig());
            builder.ApplyConfiguration(new MedicineConfig());
            builder.ApplyConfiguration(new RaitingConfig());
            builder.ApplyConfiguration(new SpecialityConfig());
            builder.ApplyConfiguration(new TagConfig());
            builder.ApplyConfiguration(new TodoConfig());
            builder.ApplyConfiguration(new UserMedicineConfig());
            builder.ApplyConfiguration(new MedCategoryConfig());
            builder.ApplyConfiguration(new RegionConfiguration());



            #endregion


            #region Audit
            builder.Entity<AuditEntry>().Property(ae => ae.Changes).HasConversion(
                value => JsonConvert.SerializeObject(value),
                serializedValue => JsonConvert.DeserializeObject<Dictionary<string, object>>(serializedValue));


            #endregion




            base.OnModelCreating(builder);

        }


        #region AutditConfig

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            // Get audit entries
            var auditEntries = OnBeforeSaveChanges();

            // Save current entity
            var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

            // Save audit entries
            await OnAfterSaveChangesAsync(auditEntries);
            return result;
        }

        private List<AuditEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var entries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {
                // Dot not audit entities that are not tracked, not changed, or not of type IAuditable
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged || !(entry.Entity is IAuditable))
                    continue;

                var auditEntry = new AuditEntry
                {
                    ActionType = entry.State == EntityState.Added ? "INSERT" : entry.State == EntityState.Deleted ? "DELETE" : "UPDATE",
                    EntityId = entry.Properties.Single(p => p.Metadata.IsPrimaryKey()).CurrentValue.ToString(),
                    EntityName = entry.Metadata.ClrType.Name,
                    Username = _username,
                    UserId = _userId,
                    TimeStamp = DateTime.UtcNow,
                    Changes = entry.Properties
                        .Select(p => new { p.Metadata.Name, p.CurrentValue })
                        .ToDictionary(i => i.Name, i => i.CurrentValue),

                    // TempProperties are properties that are only generated on save, e.g. ID's
                    // These properties will be set correctly after the audited entity has been saved
                    TempProperties = entry.Properties.Where(p => p.IsTemporary).ToList(),
                };

                entries.Add(auditEntry);
            }

            return entries;
        }

        private Task OnAfterSaveChangesAsync(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;

            // For each temporary property in each audit entry - update the value in the audit entry to the actual (generated) value
            foreach (var entry in auditEntries)
            {
                foreach (var prop in entry.TempProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        entry.EntityId = prop.CurrentValue.ToString();
                        entry.Changes[prop.Metadata.Name] = prop.CurrentValue;
                    }
                    else
                    {
                        entry.Changes[prop.Metadata.Name] = prop.CurrentValue;
                    }
                }
            }

            AuditEntries.AddRange(auditEntries);
            return SaveChangesAsync();
        }

        #endregion

        public DbSet<Region> Regions { get; set; }
        public DbSet<AuditEntry> AuditEntries { get; set; }
        public DbSet<Clinic> Clinic { get; set; }
        public DbSet<ClinicDoctor> ClinicDoctors { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<DXOperation> DXOperations { get; set; }
        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<EvaluationRating> EvaluationRatings { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<MedCategory> MedCategories { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Raiting> Raitings { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Todo> Todos { get; set; }
        public DbSet<UserMedicine> UserMedicines { get; set; }
    }
}
