using POS.Application.Services;
using POS.Domain.IRepositories;
using POS.Domain.IServices;
using POS.Infrastructure.Common.WebApi;
using POS.Infrastructure.Data.Repository;
using POS.Infrastructure.Logger;
using Microsoft.Extensions.DependencyInjection;

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

            //POS.Domain.Interfaces and repositories          
            _service.AddScoped<IUserRepository, UserReposity>();

            //POS.Infrasture
            //_service.AddScoped<ILoggerHelper, LoggerHelper>();
            _service.AddSingleton<ILoggerHelper>(LoggerHelper.Instance);
            _service.AddScoped<IWebApiClient, WebApiClient>();
        }

        public static void UIRegisterServices(IServiceCollection services)
        {
            _service = services;               
            _service.AddSingleton<ILoggerHelper>(LoggerHelper.Instance);
            _service.AddScoped<IWebApiClient, WebApiClient>();
        }
    }
}
