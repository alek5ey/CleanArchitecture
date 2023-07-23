using CleanArchitecture.Domain.Entites.OrderLines;
using CleanArchitecture.Domain.Entites.Orders;

namespace CleanArchitecture.Domain.Repositories.OrderLines;

public interface IOrderLineReadRepository : IQuerableRepository<OrderLine>
{
    Task<OrderLine?> GetByIdAsync(OrderLineId orderLineId, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyList<OrderLine>> GetByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken = default);
}