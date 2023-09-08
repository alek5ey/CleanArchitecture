using CleanArchitecture.Domain.Entites.Products;
using CleanArchitecture.Domain.Repositories.Products;

namespace CleanArchitecture.Persistence.Repositories.Products;

public class ProductWriteRepository : BaseRepository<Product, ProductId>, IProductWriteRepository
{
    public ProductWriteRepository(ApplicationDbContext context) : base(context)
    {
    }
}