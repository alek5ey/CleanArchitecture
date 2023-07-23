using CleanArchitecture.Domain.Entites.Products;

namespace CleanArchitecture.Domain.Repositories.Products;

public interface IProductReadRepository : IQuerableRepository<Product>
{
    Task<Product?> GetByIdAsync(ProductId productId, CancellationToken cancellationToken = default);
}