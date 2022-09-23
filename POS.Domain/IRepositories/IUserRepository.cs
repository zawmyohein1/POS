using POS.Domain.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Domain.IRepositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUsersAsync();
        public Task<User> CreateUserAsync(User user);
        public Task<User> GetUserByIdAsync(int Id);
        public Task<User> UpdateUserAsync(User user);
        public Task<User> DeleteUserAsync(User user);
        public Task<User> LoginUserAsync(string email, string password);
        public Task<User> CheckDuplicate(User user);
    }
}
