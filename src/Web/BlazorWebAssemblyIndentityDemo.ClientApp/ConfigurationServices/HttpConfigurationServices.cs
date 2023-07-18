using BlazorWebAssemblyIndentityDemo.ClientApp.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HttpConfigurationServices
    {
        public static IServiceCollection AddHttpServices(this IServiceCollection services)
        {
            services.AddScoped<IUserStoreService, UserStoreService>();

            return services;
        }
    }
}
