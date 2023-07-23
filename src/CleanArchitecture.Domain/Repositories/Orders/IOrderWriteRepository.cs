using CleanArchitecture.Domain.Entites.Orders;

namespace CleanArchitecture.Domain.Repositories.Orders;

public interface IOrderWriteRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken = default);
    
    void Update(Order order);
    
    void Remove(Order order);
}