using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Repositories.Orders;

namespace CleanArchitecture.Persistence.Repositories.Orders;

public class OrderWriteRepository : IOrderWriteRepository
{
    private readonly ApplicationDbContext _context;
    
    public OrderWriteRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Order order, CancellationToken cancellationToken = default) =>
        await _context.AddAsync(order, cancellationToken);

    public void Update(Order order) =>
        _context.Update(order);

    public void Remove(Order order) =>
        _context.Remove(order);
}