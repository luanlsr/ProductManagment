using ProductManagment.Domain.DTOs;
using ProductManagment.Domain.Core.Interface;

namespace ProductManagment.Domain.Interfaces.Services
{
    public interface IProductService : IService<ProductDTO, Guid>
    {
        Task<int> GetCountAsync();
        Task<ProductDTO> GetByNameAsync(string name);
    }
}
