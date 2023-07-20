using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.MessageHandler
{
    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public CustomAuthorizationMessageHandler(IAccessTokenProvider provider, NavigationManager navigation)
        : base(provider, navigation)
        {
            ConfigureHandler(
                        authorizedUrls: new[] { "https://localhost:5001", "https://localhost:5002" },
                        scopes: new[] { "userApi" ,"productApi"});
        }
    }
}
