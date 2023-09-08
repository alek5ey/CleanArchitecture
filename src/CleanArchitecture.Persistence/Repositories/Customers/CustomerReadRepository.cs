using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Repositories.Customers;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Repositories.Customers;

public class CustomerReadRepository : BaseRepository<Customer, CustomerId>, ICustomerReadRepository
{
    public CustomerReadRepository(ApplicationDbContext context) : base(context)
    {
    }
}