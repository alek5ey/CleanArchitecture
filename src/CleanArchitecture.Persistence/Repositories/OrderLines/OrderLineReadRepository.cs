using CleanArchitecture.Domain.Entites.OrderLines;
using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Repositories.OrderLines;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories.OrderLines;

public class OrderLineReadRepository : IOrderLineReadRepository
{
    private readonly ApplicationDbContext _context;

    public IQueryable<OrderLine> Query => _context.OrderLines.AsQueryable().AsNoTracking();

    public OrderLineReadRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<OrderLine?> GetByIdAsync(OrderLineId orderLineId, CancellationToken cancellationToken = default) =>
        await _context.OrderLines.FirstOrDefaultAsync(o => o.Id == orderLineId, cancellationToken);
    
    public async Task<IReadOnlyList<OrderLine>> GetByOrderIdAsync(OrderId orderId, CancellationToken cancellationToken = default) =>
        await _context.OrderLines.Where(ol => ol.OrderId == orderId).ToListAsync(cancellationToken);
}