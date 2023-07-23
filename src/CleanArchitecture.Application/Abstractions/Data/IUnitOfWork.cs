using CleanArchitecture.Domain.Primitives.DomainEvents;

namespace CleanArchitecture.Application.Abstractions.Data;

public interface IUnitOfWork
{
    IReadOnlyList<DomainEvent> ProducedDomainEvents { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    Task TransactionSuccessfulAsync(CancellationToken cancellationToken = default);
}