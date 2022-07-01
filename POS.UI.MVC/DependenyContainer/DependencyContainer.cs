using FullStack.Infrastructure.Common.WebApi;
using FullStack.Infrastructure.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace FullStack.UI.MVC
{
    public class DependencyContainer
    {
        public static IServiceCollection _service;
        
        public static void RegisterServices(IServiceCollection services)
        {
            _service = services;
            //Fullstack.Core


            //FullStack.Domain.Interfaces and repositories


            //Fullstack.Infrasture
            //_service.AddScoped<ILoggerHelper, LoggerHelper>();
            _service.AddSingleton<ILoggerHelper>(LoggerHelper.Instance);
            _service.AddScoped<IWebApiClient, WebApiClient>();
        }
    }
}
