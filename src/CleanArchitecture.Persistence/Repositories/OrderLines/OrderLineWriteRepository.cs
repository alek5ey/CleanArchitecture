using CleanArchitecture.Domain.Entites.OrderLines;
using CleanArchitecture.Domain.Repositories.OrderLines;

namespace CleanArchitecture.Persistence.Repositories.OrderLines;

public class OrderLineWriteRepository : BaseRepository<OrderLine, OrderLineId>, IOrderLineWriteRepository
{
    public OrderLineWriteRepository(ApplicationDbContext context) : base(context)
    {
    }
}