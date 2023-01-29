using Microsoft.EntityFrameworkCore.Storage;
using POS.Domain.IRepositories;
using System;
using System.Threading.Tasks;

namespace POS.Infrastructure.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {     
        IUserRepository User { get; }
        IDepartmentRepository Department { get; }
        public void StartTransaction();
        public void Commit();
        public void Rollback();
        public void SaveChanges();
    }
}
