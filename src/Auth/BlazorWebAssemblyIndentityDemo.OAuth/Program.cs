using BlazorWebAssemblyIdentityDemo.OAuth;
using BlazorWebAssemblyIdentityDemo.OAuth.Configuration;
using BlazorWebAssemblyIdentityDemo.OAuth.Data;
using BlazorWebAssemblyIdentityDemo.OAuth.Extensions;
using BlazorWebAssemblyIdentityDemo.OAuth.Model;
using Dynami.IdentityServer4.EntityFramework.Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var migrationAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

builder.Services.AddDbContext<AuthIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AuthIdentityContext>().AddDefaultTokenProviders(); 

builder.Services.AddTransient<AuthIdentityContextSeed>();

builder.Services.AddIdentityServer()
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
                })
                .AddDeveloperSigningCredential();

//builder.Services.AddIdentityServer()
//                .AddInMemoryApiScopes(InMemoryConfig.GetApiScopes())
//                .AddInMemoryApiResources(InMemoryConfig.GetApiResources())
//                .AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
//                .AddInMemoryClients(InMemoryConfig.GetClients())
//                .AddTestUsers(InMemoryConfig.GetUsers())
//                .AddDeveloperSigningCredential()
//                .AddProfileService<CustomProfileService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<AuthIdentityContextSeed>();
    await initializer.InitializeAsync();
    await initializer.SeedAsync();
}

app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

//app.Run();
app.MigrateDatabase().Run();
