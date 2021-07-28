using CCMERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CCMERP.Persistence
{
    public interface ITransactionDbContext
    {
        DbSet<OrganizationCustomerMapping> organizationCustomerMappings { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<UOMMaster> uOMMaster { get; set; }
        DbSet<ItemMaster> itemmaster { get; set; }
        DbSet<KeyMaster> keymaster { get; set; }
        DbSet<SalesOrderDtl> salesorderdtl { get; set; }
        DbSet<SalesOrderHeader> salesorderheader { get; set; }

        Task<int> SaveChangesAsync();
    }
}
