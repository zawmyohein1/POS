using POS.Domain.ViewModels;
using System.Threading.Tasks;

namespace POS.Domain.IServices
{
    public interface IDbSupplierservice
    {
        public Task<DbSupplierModelList> GetAllDbSuppliers();
        public Task<DbSupplierModel> CreateDbSupplier(DbSupplierModel model);
        public Task<DbSupplierModel> GetDbSupplierById(int Id);
        public Task<DbSupplierModel> UpdateDbSupplier(DbSupplierModel model);
        public Task<DbSupplierModel> DeleteDbSupplier(int Id, string DbSupplierName);      
    }
}
