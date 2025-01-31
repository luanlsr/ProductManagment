using ProductManagment.Domain.Core.Interface;
using ProductManagment.Domain.DTOs;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.ValueObjects;

namespace ProductManagment.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task AddAsync(OrderDTO entity);

        Task<IEnumerable<OrderDTO>> GetAllAsync();

        Task<OrderDTO> GetByIdAsync(Guid id);

        Task<int> GetCountAsync();
        Task UpdateStatusAsync(OrderDTO entity);

        Task DeleteAsync(OrderDTO entity);
    }
}
