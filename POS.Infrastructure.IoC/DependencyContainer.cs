using POS.Application.Services;
using POS.Domain.IRepositories;
using POS.Domain.IServices;
using POS.Infrastructure.Common.WebApi;
using POS.Infrastructure.Data.Repository;
using POS.Infrastructure.Logger;
using Microsoft.Extensions.DependencyInjection;
using POS.Infrastructure.Data.UnitOfWork;

namespace POS.Infrastructure.IoC
{
    public class DependencyContainer
    {
        public static IServiceCollection _service;

        public static void RegisterServices(IServiceCollection services)
        {
            _service = services;

            //POS.Infrasture           
            _service.AddSingleton<ILoggerHelper>(LoggerHelper.Instance);
            _service.AddScoped<IWebApiClient, WebApiClient>();
            _service.AddAutoMapper(typeof(Mapper.AutoMapper));

        }

            _service.AddSingleton<ILoggerHelper>(LoggerHelper.Instance);
            _service.AddScoped<IWebApiClient, WebApiClient>();
        }
    }
}
