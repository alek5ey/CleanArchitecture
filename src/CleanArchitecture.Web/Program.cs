using CleanArchitecture.Application;
using CleanArchitecture.Infrastructure;
using CleanArchitecture.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddConfigurationSources(args);

builder.Services
    .AddConfigurations(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddSwagger()
    .AddCustomApiVersioning()
    .AddControllersWithViews();

builder.Host.AddSerilog();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseHsts();
else
{
    app
        .UseDeveloperExceptionPage()
        .ConfigureSwagger();
}

app
    .UseHttpsRedirection()
    .UseHealthChecks()
    .UseRouting()
    .UseCustomExceptionHandler()
    .UseEndpoints(endpoints => endpoints.MapControllers());

await app.EnsureDatabaseCreatedAsync();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }