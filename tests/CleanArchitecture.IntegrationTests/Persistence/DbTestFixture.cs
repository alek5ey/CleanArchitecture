using CleanArchitecture.Persistence;
using CleanArchitecture.Persistence.Extensions;
using MediatR;
using Testcontainers.PostgreSql;

namespace CleanArchitecture.IntegrationTests.Persistence;

public class DbTestFixture : IAsyncLifetime
{
    private readonly PostgreSqlContainer _container;
    public ApplicationDbContext? Context { get; private set; }

    public DbTestFixture()
    {
        _container = new PostgreSqlBuilder()
            .WithAutoRemove(true)
            .Build();
    }
    
    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        var connectionString = _container.GetConnectionString();
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseNpgsql(connectionString)
            .Options;

        var mockPublisher = new Mock<IPublisher>();
        
        Context = new ApplicationDbContext(options, mockPublisher.Object);
        
        await Context.Database.MigrateAsync();
        await Context.SeedDatabaseAsync();
    }

    public async Task SaveChangesAsync() =>
        await Context!.SaveChangesAsync();
    
    public Task DisposeAsync() =>
        _container.DisposeAsync().AsTask();
}