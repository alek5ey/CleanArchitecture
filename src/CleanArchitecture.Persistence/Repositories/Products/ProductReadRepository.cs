using CleanArchitecture.Domain.Entites.Products;
using CleanArchitecture.Domain.Repositories.Products;

namespace CleanArchitecture.Persistence.Repositories.Products;

public class ProductReadRepository : BaseRepository<Product, ProductId>, IProductReadRepository
{
    public ProductReadRepository(ApplicationDbContext context) : base(context)
    {
    }
}