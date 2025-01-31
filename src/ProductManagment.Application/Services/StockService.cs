using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces;
using ProductManagment.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagment.Domain.Exceptions;
using FluentValidation;
using ProductManagment.Domain.DTOs;
using AutoMapper;
using System.Collections;

namespace ProductManagment.Application.Services
{
    public class StockService : BaseService, IStockService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<StockDTO> _validator;
        private readonly IMapper _mapper;

        public StockService(IUnitOfWork unitOfWork, IValidator<StockDTO> validator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task AddAsync(StockDTO stockDto)
        {
            Validate(stockDto, _validator);

            var entity = _mapper.Map<Stock>(stockDto);

            await _unitOfWork.StockRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<StockDTO>> GetAllAsync()
        {
            var stocks = await _unitOfWork.StockRepository.ListAsync();
            if (stocks == null)
                throw new NotFoundException("No stock records found.");

            var stocksDto = _mapper.Map<List<StockDTO>>(stocks);

            return stocksDto;
        }

        public async Task<StockDTO> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationException("Invalid stock ID.");

            var stock = await _unitOfWork.StockRepository.GetByIdAsync(id);
            if (stock == null)
                throw new NotFoundException($"Stock with ID {id} not found.");

            var stockDto = _mapper.Map<StockDTO>(stock);

            return stockDto;
        }

        public async Task<int> GetCountAsync()
        {
            return await _unitOfWork.StockRepository.CountAsync();
        }

        public async Task UpdateAsync(StockDTO stockDto)
        {
            Validate(stockDto, _validator);


            var existingStock = await _unitOfWork.StockRepository.GetByAsync(x => x.ProductId == stockDto.ProductId);
            if (existingStock == null)
                throw new NotFoundException($"Stock for product ID {stockDto.ProductId} not found.");

            if (existingStock.Quantity + stockDto.Quantity < 0)
                throw new ValidationException("Stock quantity cannot be negative.");

            existingStock.UpdateQuantity(stockDto.Quantity);

            await _unitOfWork.StockRepository.UpdateAsync(existingStock);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(StockDTO entity)
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
