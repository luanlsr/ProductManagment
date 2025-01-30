using ProductManagment.Domain.Core.Interface;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Exceptions;
using ProductManagment.Domain.ValueObjects;

namespace ProductManagment.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task AddAsync(Order entity);

        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order> GetByIdAsync(Guid id);

        Task UpdateStatusAsync(Guid orderId, OrderStatus status);

        Task DeleteAsync(Order entity);
    }
}
