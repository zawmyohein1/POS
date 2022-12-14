using Microsoft.Extensions.DependencyInjection;
using POS.Application.Services;
using POS.Domain.IRepositories;
using POS.Domain.IServices;
using POS.Infrastructure.Common.WebApi;
using POS.Infrastructure.Logger;
using POS.Infrastructure.Data.UnitOfWork;
using POS.Infrastructure.Data.Repository;

namespace POS.Infrastructure.IoC
{
    public static class DependencyContainer
    {
        public static IServiceCollection _service;

        public static IServiceCollection RegisterServices(IServiceCollection services)
        {
            _service = services;

            AddRepositories(services);
            AddServices(services);
            AddInfrastructureSevice(services);            

            return services;

        }

        public static void UIRegisterServices(IServiceCollection services)
        {
            _service = services;
            _service.AddSingleton<ILoggerHelper>(LoggerHelper.Instance);
            _service.AddScoped<IWebApiClient, WebApiClient>();
        }

        public static IServiceCollection AddRepositories(IServiceCollection services)
        {
            _service.AddTransient<IUnitOfWork, UnitOfWork>();  
            return services;
        }

        public static IServiceCollection AddServices(IServiceCollection services)
        {
            _service.AddScoped<IUserservice, Userservice>();
            _service.AddScoped<IDepartmentservice, DepartmentService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureSevice(IServiceCollection services)
        {                   
            _service.AddSingleton<ILoggerHelper>(LoggerHelper.Instance);
            _service.AddScoped<IWebApiClient, WebApiClient>();
            _service.AddAutoMapper(typeof(Mapper.AutoMapper));

            return services;
        }

    }
}
