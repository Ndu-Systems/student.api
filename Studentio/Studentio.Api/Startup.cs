using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using NLog;
using Studentio.Api.ServiceExtentions;

namespace Studentio.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            LogManager.LoadConfiguration(String.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors();
            services.ConfigurActionContextAccessor();
            services.ConfigureUrlHelper();
            services.ConfigureIISIntegration();
            services.ConfigureLoggerService();
            services.ConfigureMySqlContext(Configuration);
            services.ConfigureRepositoryWrapper();
            services.ConfigureJWTToken(Configuration);

            services.AddMvc(opt =>
            {
                opt.ReturnHttpNotAcceptable = true;
                opt.OutputFormatters
               .OfType<JsonOutputFormatter>()
               .FirstOrDefault()
               ?.SupportedMediaTypes
                  .Add("application/vnd.fiver.hateoas+json");
            }).AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.All
            });

            app.Use(async (context, next) =>
            {
                await next();

                if (context.Response.StatusCode == 404
                    && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = "/index.html";
                    await next();
                }
            });

            app.UseStaticFiles();
            //app.UseHttpsRedirection();
            app.UseAuthentication();
            //app.UseMvc(routes => {
            //    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            //});
            app.UseMvcWithDefaultRoute();


        }
    }
}
