using BlazorWebAssemblyIdentityDemo.Product.Application.Contracts;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Commands;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Data;
using BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Interceptors;
using BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Repositories.Commands;
using BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();

            services.AddDbContext<DemoContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<IDemoContext>(provider => provider.GetRequiredService<DemoContext>());
            services.AddTransient<DemoContextInitializer>();

            services.AddTransient<IProductCategoryQueryRepository, ProductCategoryQueryRepository>();
            services.AddTransient<IProductCategoryCommandRepository, ProductCategoryCommandRepository>();

            services.AddTransient<IProductQueryRepository, ProductQueryRepository>();
            services.AddTransient<IProductCommandRepository, ProductCommandRepository>();

            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
