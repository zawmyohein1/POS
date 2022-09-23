using POS.Domain.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS.Domain.IRepositories
{
    public interface IDepartmentRepository
    {
        public Task<List<Department>> GetAllDepartmentsAsync();
        public Task<Department> CreateDepartmentAsync(Department Department);
        public Task<Department> GetDepartmentByIdAsync(int Id);
        public Task<Department> UpdateDepartmentAsync(Department Department);
        public Task<Department> DeleteDepartmentAsync(Department Department);
        public Task<Department> CheckDuplicate(Department Department);
    }
}
