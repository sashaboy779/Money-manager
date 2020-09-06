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
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BusinessLogicLayer.Services.Interfaces;
using System.Text;
using Microsoft.IdentityModel.Tokens;

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

            ConfigureJwtAuthentication(appSettingsSection, services);
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

        private void ConfigureJwtAuthentication(IConfigurationSection appSettingsSection, IServiceCollection services)
        {
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = OnTokenValidatedHandler
                };
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
        }

        private Task OnTokenValidatedHandler(TokenValidatedContext context)
        {
            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            var userId = int.Parse(context.Principal.Identity.Name);
            var user = userService.GetByIdAsync(userId);

            if (user == null)
            {
                context.Fail(AppConfiguration.Unauthorized);
            }

            return Task.CompletedTask;
        }
    }
}
