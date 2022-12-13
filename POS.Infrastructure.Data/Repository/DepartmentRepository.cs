using POS.Domain.IRepositories;
using POS.Domain.EntityModels;
using POS.Infrastructure.Data.Context;

namespace POS.Infrastructure.Data.Repository
{
    public class DepartmentReposity : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentReposity(POSDbContext context) : base(context)
        {

        }
    }
}
