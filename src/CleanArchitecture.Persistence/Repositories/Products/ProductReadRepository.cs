using CleanArchitecture.Domain.Entites.Products;
using CleanArchitecture.Domain.Repositories.Products;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories.Products;

public class ProductReadRepository : IProductReadRepository
{
    private readonly ApplicationDbContext _context;

    public IQueryable<Product> Query => _context.Products.AsQueryable().AsNoTracking();
    
    public ProductReadRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Product?> GetByIdAsync(ProductId productId, CancellationToken cancellationToken = default) =>
        await _context.Products.AsNoTracking().FirstOrDefaultAsync(o => o.Id == productId, cancellationToken);
}