using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces;
using ProductManagment.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagment.Domain.Exceptions;
using FluentValidation;

namespace ProductManagment.Application.Services
{
    public class StockService : BaseService, IStockService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Stock> _validator;

        public StockService(IUnitOfWork unitOfWork, IValidator<Stock> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task AddAsync(Stock entity)
        {
            Validate(entity, _validator);

            await _unitOfWork.StockRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            var stocks = await _unitOfWork.StockRepository.ListAsync();
            if (stocks == null)
                throw new NotFoundException("No stock records found.");

            return stocks;
        }

        public async Task<Stock> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationException("Invalid stock ID.");

            var stock = await _unitOfWork.StockRepository.GetByIdAsync(id);
            if (stock == null)
                throw new NotFoundException($"Stock with ID {id} not found.");

            return stock;
        }

        public async Task UpdateAsync(Stock entity)
        {
            Validate(entity, _validator);

            var stock = await _unitOfWork.StockRepository.GetByAsync(x => x.ProductId == entity.ProductId);
            if (stock == null)
                throw new NotFoundException($"Stock for product ID {entity.ProductId} not found.");

            if (stock.Quantity + entity.Quantity < 0)
                throw new ValidationException("Stock quantity cannot be negative.");

            stock.UpdateQuantity(entity.Quantity);

            await _unitOfWork.StockRepository.UpdateAsync(stock);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Stock entity)
        {
            if (entity == null)
                throw new ValidationException("Stock cannot be null.");

            var existingStock = await _unitOfWork.StockRepository.GetByIdAsync(entity.Id);
            if (existingStock == null)
                throw new NotFoundException($"Stock with ID {entity.Id} not found.");

            await _unitOfWork.StockRepository.DeleteAsync(existingStock.Id);
            await _unitOfWork.CommitAsync();
        }
    }
}
