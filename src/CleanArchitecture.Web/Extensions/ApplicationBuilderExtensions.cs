using CleanArchitecture.Persistence;
using CleanArchitecture.Persistence.Extensions;
using CleanArchitecture.Web.Middlewares;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace CleanArchitecture.Web.Extensions;

internal static class ApplicationBuilderExtensions
{
    internal static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        => builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    
    internal static IApplicationBuilder ConfigureSwagger(
        this IApplicationBuilder builder)
    {
        var provider = builder.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
        
        builder
            .UseSwagger()
            .UseSwaggerUI(swaggerUiOptions =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
                swaggerUiOptions.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            
            swaggerUiOptions.RoutePrefix = "api/swagger";
        });
        
        return builder;
    }
    
    internal static async Task EnsureDatabaseCreatedAsync(this IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices.CreateScope();
        await using var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        var isDbCreated = await dbContext.Database.EnsureCreatedAsync();
        if (isDbCreated)
            await dbContext.SeedDatabaseAsync();
    }
    
    internal static IApplicationBuilder UseHealthChecks(this IApplicationBuilder builder)
    {
        builder.UseHealthChecks("/_health", new HealthCheckOptions
        {
            Predicate = _ => true,
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
        
        return builder;
    }
}