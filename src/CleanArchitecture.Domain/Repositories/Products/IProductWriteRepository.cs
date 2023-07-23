using CleanArchitecture.Domain.Entites.Products;

namespace CleanArchitecture.Domain.Repositories.Products;

public interface IProductWriteRepository
{
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    
    void Update(Product product);
    
    void Remove(Product product);
}