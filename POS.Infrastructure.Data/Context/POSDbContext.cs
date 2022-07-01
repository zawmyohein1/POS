using Microsoft.EntityFrameworkCore;
using POS.Domain.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace POS.Infrastructure.Data.Context
{
    public class POSDbContext : DbContext
    {
        public POSDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Audit_Trail> Audit_Trails { get; set; }
        public DbSet<User> Users { get; set; }

    }
}