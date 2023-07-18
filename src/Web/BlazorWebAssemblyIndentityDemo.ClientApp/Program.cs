using BlazorWebAssemblyIndentityDemo.ClientApp;
using BlazorWebAssemblyIndentityDemo.ClientApp.ClaimsPrincipalFactory;
using BlazorWebAssemblyIndentityDemo.ClientApp.MessageHandler;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthenticationConfigurations(builder.Configuration);

builder.Services.AddWebAPIEndPoints();

builder.Services.AddHttpServices();

await builder.Build().RunAsync();
