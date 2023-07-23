using CleanArchitecture.Domain.Entites.OrderLines;
using CleanArchitecture.Domain.Entites.Orders;

namespace CleanArchitecture.Domain.Repositories.OrderLines;

public interface IOrderLineWriteRepository
{
    Task AddAsync(OrderLine orderLine, CancellationToken cancellationToken = default);
    
    void Update(OrderLine orderLine);
    
    void Remove(OrderLine orderLine);
}