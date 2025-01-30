using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces;
using ProductManagment.Domain.Interfaces.Services;
using ProductManagment.Domain.Exceptions;
using ProductManagment.Domain.ValueObjects;
using FluentValidation;
using ProductManagment.Domain.DTOs;
using AutoMapper;

namespace ProductManagment.Application.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<OrderDTO> _validator;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IValidator<OrderDTO> validator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task AddAsync(OrderDTO orderDto)
        {
            Validate(orderDto, _validator);

            var entity = _mapper.Map<Order>(orderDto);

            await _unitOfWork.OrderRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<OrderDTO>> GetAllAsync()
        {
            var orders = await _unitOfWork.OrderRepository.ListAsync();
            if (orders == null)
                throw new NotFoundException("No orders found.");

            var ordersDto = _mapper.Map<List<OrderDTO>>(orders);
            return ordersDto;
        }

        public async Task<OrderDTO> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationException("Invalid order ID.");

            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
                throw new NotFoundException($"Order with ID {id} not found.");

            var orderDto = _mapper.Map<OrderDTO>(order);

            return orderDto;
        }

        public async Task UpdateStatusAsync(OrderDTO orderDto)
        {
            if (orderDto.Id == Guid.Empty)
                throw new ValidationException("Invalid order ID.");

            var existingOrder = await _unitOfWork.OrderRepository.GetByIdAsync(orderDto.Id);
            if (existingOrder == null)
                throw new NotFoundException($"Order with ID {orderDto.Id} not found.");

            existingOrder.UpdateStatus(orderDto.Status);

            await _unitOfWork.OrderRepository.UpdateAsync(existingOrder);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(OrderDTO entity)
        {
            if (entity == null)
                throw new ValidationException("Order cannot be null.");

            var existingOrder = await _unitOfWork.OrderRepository.GetByIdAsync(entity.Id);
            if (existingOrder == null)
                throw new NotFoundException($"Order with ID {entity.Id} not found.");

            await _unitOfWork.OrderRepository.DeleteAsync(existingOrder.Id);
            await _unitOfWork.CommitAsync();
        }
    }
}
