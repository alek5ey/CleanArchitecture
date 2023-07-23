using CleanArchitecture.Domain.Entites.Customers;

namespace CleanArchitecture.Domain.Repositories.Customers;

public interface ICustomerReadRepository : IQuerableRepository<Customer>
{
    Task<Customer?> GetByIdAsync(CustomerId orderId, CancellationToken cancellationToken = default);
}