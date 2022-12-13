using Microsoft.EntityFrameworkCore.Storage;
using POS.Domain.IRepositories;
using POS.Infrastructure.Data.Context;
using POS.Infrastructure.Data.Repository;
using System;
using System.Net;
using System.Threading.Tasks;

namespace POS.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private POSDbContext _context;
        private bool _disposed;
        public IDepartmentRepository2 Department { get; private set; }
        public UnitOfWork(IDepartmentRepository2 department)
        {
            Department = department;
        }
        public UnitOfWork(POSDbContext context)
        {
            _context = context;
            Department = new DepartmentReposity2(this._context);
        }  

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
        public void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }
        public void Dispose()
        {

            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public int Save()
        {
            return _context.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
