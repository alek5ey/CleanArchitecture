using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Repositories.Customers;

namespace CleanArchitecture.Persistence.Repositories.Customers;

public class CustomerWriteRepository : ICustomerWriteRepository
{
    private readonly ApplicationDbContext _context;
    
    public CustomerWriteRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default) =>
        await _context.AddAsync(customer, cancellationToken);

    public void Update(Customer customer) =>
        _context.Update(customer);

    public void Remove(Customer customer) =>
        _context.Remove(customer);
}