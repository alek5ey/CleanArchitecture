using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Repositories.Orders;

namespace CleanArchitecture.Persistence.Repositories.Orders;

public class OrderWriteRepository : BaseRepository<Order, OrderId>, IOrderWriteRepository
{
    public OrderWriteRepository(ApplicationDbContext context) : base(context)
    {
    }
}