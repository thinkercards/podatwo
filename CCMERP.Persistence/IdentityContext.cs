using CCMERP.Domain.Auth;
using CCMERP.Domain.Entities;
using CCMERP.Persistence.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CCMERP.Persistence
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }
        public DbSet<Organization> Organization { get; set; } 
        public DbSet<CountryMaster> country_master { get; set; }
        public DbSet<CurrencyMaster> currency_master { get; set; }
        public DbSet<OrganizationUserMapping> OrganizationUserMapping { get; set; }
        public DbSet<Useauthrtokens> Useauthrtokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("ccmadmin_db");
            modelBuilder.Entity<Useauthrtokens>().HasKey(o => new { o.UserId });
            modelBuilder.Entity<Organization>().HasKey(o => new { o.Org_ID });
            modelBuilder.Entity<CountryMaster>().HasKey(o => new { o.CountryID });
            modelBuilder.Entity<CurrencyMaster>().HasKey(o => new { o.CurrencyID });
            modelBuilder.Entity<OrganizationUserMapping>().HasKey(o => new { o.Org_ID ,o.User_ID,o.Role_ID});
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "User");
            });

            modelBuilder.Entity<IdentityRole<int>>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            modelBuilder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            //modelBuilder.Seed();
        }
    }
}
