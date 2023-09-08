using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Repositories.Customers;

namespace CleanArchitecture.Persistence.Repositories.Customers;

public class CustomerWriteRepository : BaseRepository<Customer, CustomerId>, ICustomerWriteRepository
{
    public CustomerWriteRepository(ApplicationDbContext context) : base(context)
    {
    }
}