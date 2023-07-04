using BlazorWebAssemblyIdentityDemo.Application.Contracts;
using BlazorWebAssemblyIdentityDemo.Domain.Repositories.Commands;
using BlazorWebAssemblyIdentityDemo.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Infrastructure.Data;
using BlazorWebAssemblyIdentityDemo.Infrastructure.Interceptors;
using BlazorWebAssemblyIdentityDemo.Infrastructure.Repositories.Commands;
using BlazorWebAssemblyIdentityDemo.Infrastructure.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            services.AddTransient<IUserQueryRepository, UserQueryRepository>();
            services.AddTransient<IUserCommandRepository, UserCommandRepository>();

            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
