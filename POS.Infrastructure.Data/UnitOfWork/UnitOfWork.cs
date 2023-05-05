using Microsoft.EntityFrameworkCore.Storage;
using POS.Domain.IRepositories;
using POS.Infrastructure.Data.Context;
using POS.Infrastructure.Data.Repository;
using System;

namespace POS.Infrastructure.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly POSDbContext _context;     

        private IDepartmentRepository _department;
        private IUserRepository _user;
        private IDBSupplierrRepository _dbsupplier;
        private IDbContextTransaction _transaction;

        public UnitOfWork(POSDbContext context)
        {
            _context = context;
        }
        public IDepartmentRepository Department
        {
            get
            {
                if (_department == null)
                {
                    _department = new DepartmentReposity(_context);
                }
                return _department;
            }
        }
        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_context);
                }
                return _user;
            }
        }
        public IDBSupplierrRepository DbSupplier
        {
            get
            {
                if (_dbsupplier == null)
                {
                    _dbsupplier = new DbSupplierReposity(_context);
                }
                return _dbsupplier;
            }
        }
        public void StartTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }


        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
            }

            _context.Dispose();
        }
    }
}
