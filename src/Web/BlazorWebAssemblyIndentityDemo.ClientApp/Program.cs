using BlazorWebAssemblyIndentityDemo.ClientApp;
using BlazorWebAssemblyIndentityDemo.ClientApp.ClaimsPrincipalFactory;
using BlazorWebAssemblyIndentityDemo.ClientApp.MessageHandler;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<CustomAuthorizationMessageHandler>();

builder.Services.AddHttpClient("usersAPI", cl =>
{
    cl.BaseAddress = new Uri("https://localhost:5001/api/");
})
.AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

builder.Services.AddHttpClient("userAPI.Unauthorized", cl =>
{
    cl.BaseAddress = new Uri("https://localhost:5001/api/");
});

builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("usersAPI"));

builder.Services.AddOidcAuthentication(options =>
{
    // Configure your authentication provider options here.
    // For more information, see https://aka.ms/blazor-standalone-auth
    builder.Configuration.Bind("oidc", options.ProviderOptions);
    options.UserOptions.RoleClaim = "role";
}).AddAccountClaimsPrincipalFactory<MultipleRoleClaimsPrincipalFactory<RemoteUserAccount>>();

await builder.Build().RunAsync();
