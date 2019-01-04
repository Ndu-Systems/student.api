using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Studentio.Contracts.ILoggerService;
using Studentio.Contracts.IRepositoryWrapper;
using Studentio.Contracts.IToken;
using Studentio.Entities.Context;
using Studentio.LoggerService;
using Studentio.Repository.RepositoryWrapper;
using Studentio.Repository.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studentio.Api.ServiceExtentions
{
    public static class ServiceExtentions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                               .AllowAnyHeader()
                               .AllowAnyMethod()
                               .AllowCredentials());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            string connectionString = "";
            connectionString = connectionString.IsDevelopment(true, config);
            services.AddDbContext<RepositoryContext>(o => o.UseMySql(connectionString));
        }
        public static string IsDevelopment(this string connectionString, bool check, IConfiguration config)
        {

            if (check == true)
                connectionString = config["mysqlconnection:connectionString-Dev"];
            else
                connectionString = config["mysqlconnection:connectionString-Prod"];

            return connectionString;
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddTransient<ITokenReepository, TokenRepository>();
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureJWTToken(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "https://www.student-io.net",
                        ValidAudience = "https://www.student-io.net",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]))
                    };
                });
        }

        public static void ConfigurActionContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
        }

        public static void ConfigureUrlHelper(this IServiceCollection services)
        {
            services.AddScoped<IUrlHelper>(factory =>
            {
                var actionContext = factory.GetService<IActionContextAccessor>()
                                           .ActionContext;
                return new UrlHelper(actionContext);
            });
        }
    }
}
