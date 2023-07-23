using CleanArchitecture.Domain.Entites.Customers;

namespace CleanArchitecture.Domain.Repositories.Customers;

public interface ICustomerWriteRepository
{
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
    
    void Update(Customer customer);
    
    void Remove(Customer customer);
}