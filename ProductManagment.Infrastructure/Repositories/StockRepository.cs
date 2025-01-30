using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces.Repositories;
using ProductManagment.Infrastructure.Context;

namespace ProductManagment.Infrastructure.Repositories
{
    public class StockRepository : Repository<Stock, Guid>, IStockRepository
    {
        public StockRepository(AppDbContext context) : base(context)
        {
        }
    }
}
