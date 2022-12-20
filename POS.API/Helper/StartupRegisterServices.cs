using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using POS.Application.Helper;
using POS.Infrastructure.Data.Context;


namespace POS.API.Helper
{
    public static class StartupRegisterServices
    {
        public static IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "POS.API", Version = "v1" });
            });

            services.AddDbContext<POSDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"));
            }, ServiceLifetime.Transient);

            // configure strongly typed settings objects
            var appSetting = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSetting);

            JWTServiceRegister(services, appSetting);

            services.AddCors(o => o.AddPolicy("DevCorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            return services;
        }

        private static IServiceCollection JWTServiceRegister(IServiceCollection services, IConfiguration configuration)
        {
            // configure jwt authentication
            var appSettings = configuration.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            return services;
        }
    }
}
