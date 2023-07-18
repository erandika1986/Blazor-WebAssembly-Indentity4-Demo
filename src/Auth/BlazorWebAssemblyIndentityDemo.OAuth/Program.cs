using BlazorWebAssemblyIdentityDemo.OAuth;
using BlazorWebAssemblyIdentityDemo.OAuth.Configuration;
using BlazorWebAssemblyIdentityDemo.OAuth.Data;
using BlazorWebAssemblyIdentityDemo.OAuth.Extensions;
using BlazorWebAssemblyIdentityDemo.OAuth.Model;
using Dynami.IdentityServer4;
using Dynami.IdentityServer4.EntityFramework.Microsoft.Extensions.DependencyInjection;
using Dynami.IdentityServer4.Services;
using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();

var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

builder.Services.AddDbContext<AuthIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AuthIdentityContext>().AddDefaultTokenProviders(); 

builder.Services.AddTransient<AuthIdentityContextSeed>();

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;

    // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
    options.EmitStaticAudienceClaim = true;
})
                .AddAspNetIdentity<User>()
                .AddConfigurationStore(opt =>
                {
                    opt.ConfigureDbContext = c => c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                        sql => sql.MigrationsAssembly(migrationAssembly));
                })
                .AddOperationalStore(opt =>
                {
                    opt.ConfigureDbContext = o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                        sql => sql.MigrationsAssembly(migrationAssembly));
                }).AddDeveloperSigningCredential();


builder.Services.AddScoped<IProfileService, ProfileService>();



builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(IdentityServerConstants.LocalApi.PolicyName, policy =>
    {
        policy.AddAuthenticationSchemes(IdentityServerConstants.LocalApi.AuthenticationScheme);
        policy.RequireAuthenticatedUser();

    });
});


var app = builder.Build();

app.UseCookiePolicy(new CookiePolicyOptions
{
    HttpOnly = HttpOnlyPolicy.None,
    MinimumSameSitePolicy = SameSiteMode.None,
    Secure = CookieSecurePolicy.Always
});

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<AuthIdentityContextSeed>();
    await initializer.InitializeAsync();
    await initializer.SeedAsync();
}

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

//app.Run();
app.MigrateDatabase().Run();
