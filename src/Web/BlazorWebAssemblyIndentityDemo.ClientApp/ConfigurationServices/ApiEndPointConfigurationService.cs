using BlazorWebAssemblyIndentityDemo.ClientApp.MessageHandler;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiEndPointConfigurationService
    {
        public static IServiceCollection AddWebAPIEndPoints(this IServiceCollection services)
        {
            services.AddHttpClient("usersAPI", cl =>
            {
                cl.BaseAddress = new Uri("https://localhost:5001/api/");
            }).AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

            services.AddHttpClient("userAPI.Unauthorized", cl =>
            {
                cl.BaseAddress = new Uri("https://localhost:5001/api/");
            });

            services.AddHttpClient("productApi", cl =>
            {
                cl.BaseAddress = new Uri("https://localhost:5002/api/");
            }).AddHttpMessageHandler<CustomAuthorizationMessageHandler>();


            services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("usersAPI"));
            services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("productApi"));

            return services;
        }
    }
}
