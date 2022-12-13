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
    public class DepartmentReposity : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentReposity(POSDbContext context) : base(context)
        {

        }
    }
}
