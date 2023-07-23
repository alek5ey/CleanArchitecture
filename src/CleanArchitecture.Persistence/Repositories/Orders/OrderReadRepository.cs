using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Repositories.Orders;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories.Orders;

public class OrderReadRepository : IOrderReadRepository
{
    private readonly ApplicationDbContext _context;

    public IQueryable<Order> Query => _context.Orders.AsQueryable().AsNoTracking();
    
    public OrderReadRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Order?> GetByIdAsync(OrderId orderId, CancellationToken cancellationToken = default) =>
        await _context.Orders
            .Include(o => o.OrderLines)
            .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
}