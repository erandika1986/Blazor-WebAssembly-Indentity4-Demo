using BlazorWebAssemblyIdentityDemo.ManageUserApi;
using BlazorWebAssemblyIdentityDemo.ManageUserApi.Extensions;
using Dynami.IdentityServer4.Services;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.ConfigureCors();
//builder.Services.ConfigureIISIntegration();
//builder.Services.ConfigureSqlContext(builder.Configuration);

builder.Services.AddWebAPIServices(builder.Configuration);

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", opt =>
//    {
//        opt.RequireHttpsMetadata = false;
//        opt.Authority = "https://localhost:5005";
//        opt.Audience = "userApi";
//    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseCors("CorsPolicy");

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});



app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
