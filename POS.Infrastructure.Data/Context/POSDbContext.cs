using Microsoft.EntityFrameworkCore;
using POS.Domain.EntityModels;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace POS.Infrastructure.Data.Context
{
    public class POSDbContext : DbContext
    {
        public POSDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Audit_Trail> Audit_Trails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<Occupation> Occupations { get; set; }

    }
}