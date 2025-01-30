using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces;
using ProductManagment.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductManagment.Domain.Exceptions;
using ProductManagment.Domain.ValueObjects;
using FluentValidation;

namespace ProductManagment.Application.Services
{
    public class OrderService : BaseService, IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Order> _validator;

        public OrderService(IUnitOfWork unitOfWork, IValidator<Order> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task AddAsync(Order entity)
        {
            Validate(entity, _validator);

            await _unitOfWork.OrderRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var orders = await _unitOfWork.OrderRepository.ListAsync();
            if (orders == null)
                throw new NotFoundException("No orders found.");

            return orders;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationException("Invalid order ID.");

            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
                throw new NotFoundException($"Order with ID {id} not found.");

            return order;
        }

        public async Task UpdateStatusAsync(Guid orderId, OrderStatus status)
        {
            if (orderId == Guid.Empty)
                throw new ValidationException("Invalid order ID.");

            var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
            if (order == null)
                throw new NotFoundException($"Order with ID {orderId} not found.");

            order.UpdateStatus(status);

            await _unitOfWork.OrderRepository.UpdateAsync(order);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Order entity)
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
