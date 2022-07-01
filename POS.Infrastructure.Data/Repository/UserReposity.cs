using POS.Domain.IRepositories;
using POS.Domain.Models;
using POS.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Infrastructure.Data.Repository
{
    public class UserReposity : IUserRepository
    {
        protected readonly DbContextOptions _option;

        public UserReposity(DbContextOptions option)
        {
            _option = option;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            using (var _dbContext = new POSDbContext(_option))
            {
                using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var userList = _dbContext.Users.Where(x => x.IsDeleted == false).ToList();
                        return await Task.FromResult<List<User>>(userList);
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<User> CreateUserAsync(User entity)
        {
            using (var _dbContext = new POSDbContext(_option))
            {
                using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        _dbContext.Users.Add(entity);
                        _dbContext.SaveChanges();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
                return await Task.FromResult<User>(entity);
            }
        }

        public async Task<User> GetUserByIdAsync(int Id)
        {
            using (var _dbContext = new POSDbContext(_option))
            {
                using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var entity = _dbContext.Users.Where(x => x.Id == Id).FirstOrDefault();
                        return await Task.FromResult<User>(entity);
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<User> UpdateUserAsync(User entity)
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
            return await Task.FromResult<User>(entity);
        }

        public async Task<User> DeleteUserAsync(User entity)
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
            return await Task.FromResult<User>(entity);
        }

        public async Task<User> LoginUserAsync(string email, string password)
        {
            using (var _dbContext = new POSDbContext(_option))
            {
                using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var entity = _dbContext.Users.Where(x => x.Email == email && x.Password == password).FirstOrDefault();
                        return await Task.FromResult<User>(entity);
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        public async Task<User> CheckDuplicate(User user)
        {
            using (var _dbContext = new POSDbContext(_option))
            {
                using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var entity = _dbContext.Users.Where(x => x.Email == user.Email && x.Id != user.Id && x.IsDeleted == false).FirstOrDefault();
                        return await Task.FromResult<User>(entity);
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
