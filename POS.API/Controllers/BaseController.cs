using POS.Infrastructure.IoC;
using POS.Infrastructure.Logger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.API.Controllers
{
    public class BaseController : ControllerBase
    {
       protected ILoggerHelper _logger = null;
       protected IServiceProvider _serviceProvider = null;

        public BaseController()
        {
            _serviceProvider = DependencyContainer._service.BuildServiceProvider();
            _logger = _serviceProvider.GetService<ILoggerHelper>();     
        }
    }
}
