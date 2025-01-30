using ProductManagment.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagment.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepository { get; }
        IOrderRepository OrderRepository { get; }
        IClientRepository ClientRepository { get; }
        IStockRepository StockRepository { get; }
        Task BeginTransactionAsync();
        Task<int> CommitAsync();
        Task RollbackAsync();
        bool HasActiveTransaction();
        Task SaveChangesAsync();
    }
}
