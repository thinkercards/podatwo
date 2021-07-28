using CCMERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CCMERP.Persistence
{
    public class TransactionDbContext : DbContext, ITransactionDbContext
    {
        // This constructor is used of runit testing
        public TransactionDbContext()
        {

        }
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }


        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrganizationCustomerMapping> organizationCustomerMappings { get; set; }
        public DbSet<UOMMaster> uOMMaster { get; set; }
        public DbSet<ItemMaster> itemmaster { get; set; }
        public DbSet<KeyMaster> keymaster { get; set; }
        public DbSet<SalesOrderDtl> salesorderdtl { get; set; }
        public DbSet<SalesOrderHeader> salesorderheader { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("ccmtransdb");
            modelBuilder.Entity<Customer>().HasKey(o => new { o.CustomerID});
            modelBuilder.Entity<ItemMaster>().HasKey(o => new { o.ItemId});
            modelBuilder.Entity<UOMMaster>().HasKey(o => new { o.UOMId});
            modelBuilder.Entity<SalesOrderDtl>().HasKey(o => new { o.SODtlId});
            modelBuilder.Entity<SalesOrderHeader>().HasKey(o => new { o.SOHdrId});
            modelBuilder.Entity<OrganizationCustomerMapping>().HasKey(o => new { o.Org_ID,o.CustomerID});
            modelBuilder.Entity<KeyMaster>().HasKey(o => new { o.KeyId,o.OrgId});
        }

     //   protected override void OnConfiguring(DbContextOptionsBuilder options)
     //=> options.UseMySQL("DataSource=app.db");

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
