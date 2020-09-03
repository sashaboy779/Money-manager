using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using DataAccessLayer.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InfrastructureLayer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureDbContext(this IServiceCollection services, 
            bool isProductionEnvironment, string connectionString)
        {
            if (isProductionEnvironment)
            {
                services.AddDbContext<DataContext>(opt => 
                opt.UseLazyLoadingProxies().UseSqlServer(connectionString));
            }
            else
            {
                services.AddDbContext<DataContext, SqliteDataContext>(opt =>
                opt.UseLazyLoadingProxies().UseSqlite(connectionString));
            }

            return services;
        }

        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            RegisterBusinessLogic(services);
            RegisterDataAccess(services);

            return services;
        }

        private static void RegisterBusinessLogic(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IWalletSevice, WalletService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IOperationService, OperationService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IPasswordValidator, PasswordValidator>();

            services.AddScoped<IUriService, UriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absolutePath = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), "/");

                return new UriService(absolutePath);
            });
        }

        private static void RegisterDataAccess(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGenericRepository<User>, GenericRepository<User>>();
            services.AddScoped<IGenericRepository<Wallet>, GenericRepository<Wallet>>();
            services.AddScoped<IGenericRepository<Operation>, GenericRepository<Operation>>();
            services.AddScoped<IGenericRepository<MainCategory>, GenericRepository<MainCategory>>();
        }
    }
}
