using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Domain.Repositories.Customers;
using CleanArchitecture.Domain.Repositories.OrderLines;
using CleanArchitecture.Domain.Repositories.Orders;
using CleanArchitecture.Domain.Repositories.Products;
using CleanArchitecture.Persistence.Repositories.Customers;
using CleanArchitecture.Persistence.Repositories.OrderLines;
using CleanArchitecture.Persistence.Repositories.Orders;
using CleanArchitecture.Persistence.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddScoped<IApplicationDbContext>(serviceProvider =>
            serviceProvider.GetRequiredService<ApplicationDbContext>());
        
        services.AddScoped<IUnitOfWork>(serviceProvider =>
            serviceProvider.GetRequiredService<ApplicationDbContext>());
        
        services.AddScoped<IOrderReadRepository, OrderReadRepository>();
        services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
        
        services.AddScoped<IOrderLineReadRepository, OrderLineReadRepository>();
        services.AddScoped<IOrderLineWriteRepository, OrderLineWriteRepository>();
        
        services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
        services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
        
        services.AddScoped<IProductReadRepository, ProductReadRepository>();
        services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        return services;
    }
}