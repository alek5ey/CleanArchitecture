using CleanArchitecture.Persistence;
using MassTransit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CleanArchitecture.FunctionalTests.Web;

public class ApiTestFixture : WebApplicationFactory<Program>
{
    protected override IHost CreateHost(IHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        
        builder.ConfigureServices(services =>
        {
            var forDeleteServiceDescriptors = services.Where(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>) ||
                                                                  (d.ServiceType.Namespace?.StartsWith("MassTransit") ?? false)).ToList();
            foreach (var descriptor in forDeleteServiceDescriptors)
                services.Remove(descriptor);
            
            services.AddEntityFrameworkInMemoryDatabase();
            
            services.AddMassTransit(c =>
            {
                c.AddBus(provider =>
                {
                    var control = Bus.Factory.CreateUsingInMemory(cfg =>
                    {
                        cfg.ConfigureEndpoints(provider);
                    });
                    control.Start();
                    return control;
                });
            });

            var provider = services
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
                options.UseInternalServiceProvider(provider);
            });
        });
        
        return base.CreateHost(builder);
    }
}