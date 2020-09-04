using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using InfrastructureLayer;
using System.IO;
using MoneyManagerApi.Infrastructure.Constants;
using AutoMapper;
using System.Reflection;

namespace MoneyManagerApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment environment;
        private readonly IConfiguration configuration;
        
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            this.environment = environment;
            this.configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = configuration.GetConnectionString(AppConfiguration.ConnectionStringName);
            services.ConfigureDbContext(environment.IsProduction(), connectionString);

            services.AddCors();
            services.AddControllers();
            services.AddAutoMapper(
                Assembly.Load(AppConfiguration.WebApiProject),
                Assembly.Load(AppConfiguration.BusinessLogicProject));

            services.ConfigureDependencyInjection();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile(String.Format(AppConfiguration.LogFilePath, path));

            app.ConfigureExceptionHandler(loggerFactory.CreateLogger<ErrorDetails>());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
