using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Entites.OrderLines;
using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Entites.Products;
using CleanArchitecture.Domain.Primitives;
using CleanArchitecture.Domain.Primitives.DomainEvents;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence;

public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public IReadOnlyList<DomainEvent> ProducedDomainEvents { get; private set; } = Array.Empty<DomainEvent>();
    
    public ApplicationDbContext(DbContextOptions options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseNpgsql().UseSnakeCaseNamingConvention();

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<OrderLine> OrderLines { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ProducedDomainEvents = ChangeTracker.Entries<DomainEntity>()
            .Select(e => e.Entity)
            .Where(e => e.HasDomainEvents)
            .SelectMany(e => e.GetDomainEvents())
            .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    public async Task TransactionSuccessfulAsync(CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in ProducedDomainEvents)
            await _publisher.Publish(domainEvent, cancellationToken);
    }
}