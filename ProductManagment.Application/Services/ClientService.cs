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
    public class ClientService : BaseService, IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Client> _validator;

        public ClientService(IUnitOfWork unitOfWork, IValidator<Client> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task AddAsync(Client entity)
        {
            Validate(entity, _validator);

            await _unitOfWork.ClientRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            var clients = await _unitOfWork.ClientRepository.ListAsync();
            if (clients == null)
                throw new NotFoundException("No clients found.");

            return clients;
        }

        public async Task<Client> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationException("Invalid client ID.");

            var client = await _unitOfWork.ClientRepository.GetByIdAsync(id);
            if (client == null)
                throw new NotFoundException($"Client with ID {id} not found.");

            return client;
        }

        public async Task UpdateAsync(Client entity)
        {
            Validate(entity, _validator);

            var existingClient = await _unitOfWork.ClientRepository.GetByIdAsync(entity.Id);
            if (existingClient == null)
                throw new NotFoundException($"Client with ID {entity.Id} not found.");

            existingClient.Update(entity.Name, entity.Email, entity.Phone);

            await _unitOfWork.ClientRepository.UpdateAsync(existingClient);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Client entity)
        {
            if (entity == null)
                throw new ValidationException("Client cannot be null.");

            var existingClient = await _unitOfWork.ClientRepository.GetByIdAsync(entity.Id);
            if (existingClient == null)
                throw new NotFoundException($"Client with ID {entity.Id} not found.");

            await _unitOfWork.ClientRepository.DeleteAsync(existingClient.Id);
            await _unitOfWork.CommitAsync();
        }
    }
}
