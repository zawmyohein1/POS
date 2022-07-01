using POS.Domain.ViewModels;
using System.Threading.Tasks;

namespace POS.Domain.IServices
{
    public interface IUserservice
    {
        public Task<UserModelList> GetAllUsers();
        public Task<UserModel> CreateUser(UserModel model);
        public Task<UserModel> GetUserById(int Id);
        public Task<UserModel> UpdateUser(UserModel model);
        public Task<UserModel> DeleteUser(int Id, string userName);
        public Task<UserModel> LoginUser(UserModel model);
    }
}
