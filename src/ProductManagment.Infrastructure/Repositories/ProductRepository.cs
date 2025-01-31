using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces.Repositories;
using ProductManagment.Infrastructure.Context;

namespace ProductManagment.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
