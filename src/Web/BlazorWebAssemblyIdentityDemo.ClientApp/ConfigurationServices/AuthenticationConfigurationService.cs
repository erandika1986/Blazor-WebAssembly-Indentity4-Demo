using BlazorWebAssemblyIdentityDemo.ClientApp.ClaimsPrincipalFactory;
using BlazorWebAssemblyIdentityDemo.ClientApp.MessageHandler;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AuthenticationConfigurationService
    {
        public static IServiceCollection AddAuthenticationConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<CustomAuthorizationMessageHandler>();

            services.AddOidcAuthentication(options =>
            {
                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                configuration.Bind("oidc", options.ProviderOptions);
                options.UserOptions.RoleClaim = "role";
            }).AddAccountClaimsPrincipalFactory<MultipleRoleClaimsPrincipalFactory<RemoteUserAccount>>();

            return services;
        }
    }
}
