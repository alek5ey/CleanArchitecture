using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Repositories.Customers;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories.Customers;

public class CustomerReadRepository : ICustomerReadRepository
{
    private readonly ApplicationDbContext _context;

    public IQueryable<Customer> Query => _context.Customers.AsQueryable().AsNoTracking();
    
    public CustomerReadRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Customer?> GetByIdAsync(CustomerId customerId, CancellationToken cancellationToken = default) =>
        await _context.Customers.FirstOrDefaultAsync(o => o.Id == customerId, cancellationToken);
}