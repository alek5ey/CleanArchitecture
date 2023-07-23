using CleanArchitecture.Infrastructure.MessageBroker;
using CleanArchitecture.Persistence;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var messageBrokerSettings = services.BuildServiceProvider().GetRequiredService<IOptions<MessageBrokerSettings>>().Value;
        
        services.AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("Default")!, name: "Database");
        
        services.AddMassTransit(mt =>
        {
            mt.SetKebabCaseEndpointNameFormatter();
            mt.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(new Uri(messageBrokerSettings.Host), "/", c =>
                {
                    c.Username(messageBrokerSettings.Username);
                    c.Password(messageBrokerSettings.Password);
                });
                
                cfg.MessageTopology.SetEntityNameFormatter(
                    new CustomNameFormater(cfg.MessageTopology.EntityNameFormatter, messageBrokerSettings));
                
                cfg.ConfigureEndpoints(context);
            });
        });
        
        services.AddPersistence(configuration);
        
        return services;
    }
}