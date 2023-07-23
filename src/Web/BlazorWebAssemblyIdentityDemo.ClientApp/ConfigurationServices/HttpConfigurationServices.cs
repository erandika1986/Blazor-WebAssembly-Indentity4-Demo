using BlazorWebAssemblyIdentityDemo.ClientApp.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class HttpConfigurationServices
    {
        public static IServiceCollection AddHttpServices(this IServiceCollection services)
        {
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<ContextMenuService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<IUserStoreService, UserStoreService>();

            return services;
        }
    }
}
