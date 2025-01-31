using ProductManagment.Domain.Core.Interface;
using ProductManagment.Domain.DTOs;

namespace ProductManagment.Domain.Interfaces.Services
{
    public interface IStockService : IService<StockDTO, Guid>
    {
    }
}
