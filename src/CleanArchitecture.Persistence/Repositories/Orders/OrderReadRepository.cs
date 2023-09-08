using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Repositories.Orders;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories.Orders;

public class OrderReadRepository : BaseRepository<Order, OrderId>, IOrderReadRepository
{
    public OrderReadRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public new async Task<Order?> GetByIdAsync(OrderId orderId, CancellationToken cancellationToken = default) =>
        await Context.Orders
            .Include(o => o.OrderLines)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
}