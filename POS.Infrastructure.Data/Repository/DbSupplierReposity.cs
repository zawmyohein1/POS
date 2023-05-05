using POS.Domain.IRepositories;
using POS.Domain.EntityModels;
using POS.Infrastructure.Data.Context;

namespace POS.Infrastructure.Data.Repository
{
    public class DbSupplierReposity : GenericRepository<DbSupplier>, IDBSupplierrRepository
    {
        public DbSupplierReposity(POSDbContext context) : base(context)
        {

        }
    }
}
