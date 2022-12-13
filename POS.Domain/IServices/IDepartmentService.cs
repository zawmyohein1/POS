using POS.Domain.ViewModels;
using System.Threading.Tasks;

namespace POS.Domain.IServices
{
    public interface IDepartmentservice
    {
        public Task<DepartmentModelList> GetAllDepartments();
        public Task<DepartmentModel> CreateDepartment(DepartmentModel model);
        public Task<DepartmentModel> GetDepartmentById(int id);
        public Task<DepartmentModel> UpdateDepartment(DepartmentModel model);
        public Task<DepartmentModel> DeleteDepartment(int id, string name);
    }
}
