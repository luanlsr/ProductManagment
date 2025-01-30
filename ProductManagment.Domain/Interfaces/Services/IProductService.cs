using ProductManagment.Domain.Core.Interface;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Domain.Interfaces.Services
{
    public interface IProductService : IService<Product, Guid>
    {
    }
}
