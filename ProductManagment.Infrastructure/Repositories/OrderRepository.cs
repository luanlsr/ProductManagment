using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces.Repositories;
using ProductManagment.Infrastructure.Context;

namespace ProductManagment.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order, Guid>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
