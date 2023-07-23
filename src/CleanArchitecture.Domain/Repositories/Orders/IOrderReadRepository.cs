using CleanArchitecture.Domain.Entites.Orders;

namespace CleanArchitecture.Domain.Repositories.Orders;

public interface IOrderReadRepository : IQuerableRepository<Order>
{
    Task<Order?> GetByIdAsync(OrderId orderId, CancellationToken cancellationToken = default);
}