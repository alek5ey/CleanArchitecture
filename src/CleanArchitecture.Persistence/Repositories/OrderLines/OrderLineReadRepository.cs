using CleanArchitecture.Domain.Entites.OrderLines;
using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Repositories.OrderLines;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories.OrderLines;

public class OrderLineReadRepository : BaseRepository<OrderLine, OrderLineId>, IOrderLineReadRepository
{

    public OrderLineReadRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task<IReadOnlyList<OrderLine>> GetByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken = default) =>
        await Context.OrderLines.Where(ol => ol.OrderId == orderId).ToListAsync(cancellationToken);
}