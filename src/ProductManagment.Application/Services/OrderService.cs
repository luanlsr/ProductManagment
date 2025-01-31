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

            // Adicionando os itens ao pedido
            if (orderDto.Items == null || !orderDto.Items.Any())
                throw new ValidationException("An order must have at least one item.");

            foreach (var itemDto in orderDto.Items)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(itemDto.ProductId);
                if (product == null)
                    throw new NotFoundException($"Product with ID {itemDto.ProductId} not found.");

                var orderItem = new OrderItem(entity.Id, itemDto.ProductId, itemDto.Quantity, product.Price);
                entity.Items.Add(orderItem);
            }

            entity.RecalculateTotal();

            await _unitOfWork.OrderRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<OrderDTO>> GetAllAsync()
        {
            var orders = await _unitOfWork.OrderRepository.ListAsync();
            if (orders == null)
                throw new NotFoundException("No orders found.");

            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationException("Invalid order ID.");

            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
                throw new NotFoundException($"Order with ID {id} not found.");

            return _mapper.Map<OrderDTO>(order);
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

        public async Task UpdateOrderAsync(OrderDTO orderDto)
        {
            if (orderDto.Id == Guid.Empty)
                throw new ValidationException("Invalid order ID.");

            var existingOrder = await _unitOfWork.OrderRepository.GetByIdAsync(orderDto.Id);
            if (existingOrder == null)
                throw new NotFoundException($"Order with ID {orderDto.Id} not found.");

            existingOrder.UpdateStatus(orderDto.Status);

            existingOrder.Items.Clear();

            foreach (var itemDto in orderDto.Items)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(itemDto.ProductId);
                if (product == null)
                    throw new NotFoundException($"Product with ID {itemDto.ProductId} not found.");

                var orderItem = new OrderItem(existingOrder.Id, itemDto.ProductId, itemDto.Quantity, product.Price);
                existingOrder.Items.Add(orderItem);
            }

            existingOrder.RecalculateTotal();

            await _unitOfWork.OrderRepository.UpdateAsync(existingOrder);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(OrderDTO orderDto)
        {
            if (orderDto.Id == Guid.Empty)
                throw new ValidationException("Invalid order ID.");

            var existingOrder = await _unitOfWork.OrderRepository.GetByIdAsync(orderDto.Id);
            if (existingOrder == null)
                throw new NotFoundException($"Order with ID {orderDto.Id} not found.");

            await _unitOfWork.OrderRepository.DeleteAsync(existingOrder.Id);
            await _unitOfWork.CommitAsync();
        }
    }
}
