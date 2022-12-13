using POS.Domain.IRepositories;
using POS.Domain.EntityModels;
using POS.Infrastructure.Data.Context;

namespace POS.Infrastructure.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(POSDbContext context) : base(context)
        {

        }
    }
}
