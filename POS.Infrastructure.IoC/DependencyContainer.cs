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

            //POS.Core
            _service.AddScoped<IUserservice, Userservice>();
            _service.AddScoped<IDepartmentservice, DepartmentService>();

            _service.AddTransient<IUnitOfWork, UnitOfWork>();
            //POS.Domain.Interfaces and repositories                
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            _service.AddScoped<IUserRepository, UserRepository>();
            //_service.AddScoped<IDepartmentRepository, DepartmentReposity>();


            //_service.AddScoped<IDepartmentRepository2, DepartmentReposity2>();

            //POS.Infrasture           
            _service.AddSingleton<ILoggerHelper>(LoggerHelper.Instance);
            _service.AddScoped<IWebApiClient, WebApiClient>();
            _service.AddAutoMapper(typeof(Mapper.AutoMapper));

        }

        public static void UIRegisterServices(IServiceCollection services)
        {
            _service = services;
            _service.AddSingleton<ILoggerHelper>(LoggerHelper.Instance);
            _service.AddScoped<IWebApiClient, WebApiClient>();
        }
    }
}
