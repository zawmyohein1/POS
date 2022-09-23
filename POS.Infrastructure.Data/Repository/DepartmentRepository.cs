using POS.Domain.IRepositories;
using POS.Domain.EntityModels;
using POS.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Infrastructure.Data.Repository
{
    public class DepartmentReposity : IDepartmentRepository
    {
        protected readonly DbContextOptions _option;
        public DepartmentReposity(DbContextOptions option)
        {
            _option = option;
        }
        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            using (var _dbContext = new POSDbContext(_option))
            {
                using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var userList = _dbContext.Departments.Where(x => x.IsDeleted == false).ToList();
                        return await Task.FromResult<List<Department>>(userList);
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public async Task<Department> CreateDepartmentAsync(Department entity)
        {
            using (var _dbContext = new POSDbContext(_option))
            {
                using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        _dbContext.Departments.Add(entity);
                        _dbContext.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
                return await Task.FromResult<Department>(entity);
            }
        }
        public async Task<Department> GetDepartmentByIdAsync(int departmentId)
        {
            using (var _dbContext = new POSDbContext(_option))
            {
                using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var entity = _dbContext.Departments.Where(x => x.Department_ID == departmentId).FirstOrDefault();
                        return await Task.FromResult<Department>(entity);
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public async Task<Department> UpdateDepartmentAsync(Department entity)
        {
            using (var _dbContext = new POSDbContext(_option))
            {
                using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        _dbContext.Update(entity);
                        _dbContext.SaveChanges();
                        dbContextTransaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
            }
            return await Task.FromResult<Department>(entity);
        }
        public async Task<Department> DeleteDepartmentAsync(Department entity)
        {
            using (var _dbContext = new POSDbContext(_option))
            {
                using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        _dbContext.Update(entity);
                        _dbContext.SaveChanges();
                        dbContextTransaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
            }
            return await Task.FromResult<Department>(entity);
        }
        public async Task<Department> CheckDuplicate(Department department)
        {
            using (var _dbContext = new POSDbContext(_option))
            {
                using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var entity = _dbContext.Departments.Where(x => x.Department_Name == department.Department_Name && x.Department_ID != department.Department_ID && x.IsDeleted == false).FirstOrDefault();
                        return await Task.FromResult<Department>(entity);
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
    }
}
