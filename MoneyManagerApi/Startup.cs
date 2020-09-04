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
using BusinessLogicLayer;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MoneyManagerApi.Models;

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

            services.AddLocalization(options => options.ResourcesPath = AppConfiguration.ResourcesPath);
            services.AddMvc()
                .AddNewtonsoftJson(options =>
                {
                    options.AllowInputFormatterExceptionMessages = false;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .AddDataAnnotationsLocalization(options =>
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(ModelsResources)));


            var appSettingsSection = configuration.GetSection(AppConfiguration.AppSettingsSection);
            services.Configure<AppSettings>(appSettingsSection);

            // Add JWT configuration
            services.AddHttpContextAccessor();
            services.ConfigureDependencyInjection();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile(String.Format(AppConfiguration.LogFilePath, path));

            app.ConfigureExceptionHandler(loggerFactory.CreateLogger<ErrorDetails>());

            var supportedCultures = AppConfiguration.SupportedCultures;
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(AppConfiguration.DefaultCulture)
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
