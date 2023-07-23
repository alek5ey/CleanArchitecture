using CleanArchitecture.Domain.Entites.Products;
using CleanArchitecture.Domain.Repositories.Products;

namespace CleanArchitecture.Persistence.Repositories.Products;

public class ProductWriteRepository : IProductWriteRepository
{
    private readonly ApplicationDbContext _context;
    
    public ProductWriteRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAsync(Product product, CancellationToken cancellationToken = default) =>
        await _context.AddAsync(product, cancellationToken);

    public void Update(Product product) =>
        _context.Update(product);

    public void Remove(Product product) =>
        _context.Remove(product);
}