using Microsoft.EntityFrameworkCore.Storage;
using ProductManagment.Domain.Interfaces;
using ProductManagment.Domain.Interfaces.Repositories;
using ProductManagment.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagment.Infrastructure
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IStockRepository _stockRepository;

        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(
            AppDbContext context, 
            IProductRepository workItemRepository, 
            IProductRepository productRepository, 
            IOrderRepository orderRepository, 
            IClientRepository clientRepository, 
            IStockRepository stockRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _clientRepository = clientRepository;
            _stockRepository = stockRepository;
        }

        public IProductRepository ProductRepository => _productRepository;
        public IOrderRepository OrderRepository => _orderRepository;
        public IClientRepository ClientRepository => _clientRepository;
        public IStockRepository StockRepository => _stockRepository;

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction == null)
            {
                _currentTransaction = await _context.Database.BeginTransactionAsync();
            }
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                var result = await _context.SaveChangesAsync();
                if (_currentTransaction != null)
                {
                    await _currentTransaction.CommitAsync();
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
                return result;
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }

        public async Task RollbackAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }

        public bool HasActiveTransaction()
        {
            return _currentTransaction != null;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _currentTransaction?.Dispose();
            _context?.Dispose();
        }
    }
}
