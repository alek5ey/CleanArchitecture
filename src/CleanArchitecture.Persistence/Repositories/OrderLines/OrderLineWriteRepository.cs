using CleanArchitecture.Domain.Entites.OrderLines;
using CleanArchitecture.Domain.Repositories.OrderLines;

namespace CleanArchitecture.Persistence.Repositories.OrderLines;

public class OrderLineWriteRepository : IOrderLineWriteRepository
{
    private readonly ApplicationDbContext _context;
    
    public OrderLineWriteRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(OrderLine orderLine, CancellationToken cancellationToken = default) =>
        await _context.AddAsync(orderLine, cancellationToken);

    public void Update(OrderLine orderLine) =>
        _context.Update(orderLine);

    public void Remove(OrderLine orderLine) =>
        _context.Remove(orderLine);
}