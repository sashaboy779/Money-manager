using AutoMapper;
using MoneyManagerUi.Data;
using MoneyManagerUi.Infrastructure;
using MoneyManagerUi.Services;
using MoneyManagerUi.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Constants = MoneyManagerUi.Infrastructure.Constants.Configuration;
using Blazored.Modal;
using Blazored.SessionStorage;

namespace MoneyManagerUi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddBlazoredModal();
            services.AddControllers();
            services.AddAutoMapper(Assembly.Load(Constants.AssemblyName));

            var appSettingsSection = Configuration.GetSection(Constants.AppSettings);
            services.Configure<AppSettings>(appSettingsSection);

            services.AddScoped<IUserService, UserService>();
            services.AddBlazoredSessionStorage();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddScoped<IExpanseManagerClient, ExpanseManagerClient>();
            services.AddScoped<IOperationService, OperationService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IReportService, ReportService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var supportedCultures = new[] { Constants.EnglishCulture, Constants.UkraineCulture };
            var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);

            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(Constants.ExceptionHandler);
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage(Constants.FallbackPage);
            });
        }
    }
}
