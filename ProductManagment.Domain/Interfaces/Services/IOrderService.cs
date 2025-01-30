using ProductManagment.Domain.Base;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Domain.Interfaces.Services
{
    public interface IOrderService : IService<Order, Guid>
    {
    }
}
