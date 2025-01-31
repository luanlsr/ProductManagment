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

namespace ProductManagment.Application.Services
{
    public class ClientService : BaseService, IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ClientDTO> _validator;
        private readonly IMapper _mapper;

        public ClientService(IUnitOfWork unitOfWork, IValidator<ClientDTO> validator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task AddAsync(ClientDTO clientDto)
        {
            Validate(clientDto, _validator);

            var entity = _mapper.Map<Client>(clientDto);

            await _unitOfWork.ClientRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ClientDTO>> GetAllAsync()
        {
            var clients = await _unitOfWork.ClientRepository.ListAsync();
            if (clients == null)
                throw new NotFoundException("No clients found.");

            var clientsDto = _mapper.Map<List<ClientDTO>>(clients);

            return clientsDto;
        }

        public async Task<ClientDTO> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationException("Invalid client ID.");

            var client = await _unitOfWork.ClientRepository.GetByIdAsync(id);
            if (client == null)
                throw new NotFoundException($"Client with ID {id} not found.");

            var clientDto = _mapper.Map<ClientDTO>(client);

            return clientDto;
        }

        public async Task UpdateAsync(ClientDTO clientDto)
        {
            Validate(clientDto, _validator);

            var existingClient = await _unitOfWork.ClientRepository.GetByIdAsync(clientDto.Id);
            if (existingClient == null)
                throw new NotFoundException($"Client with ID {clientDto.Id} not found.");

            await _unitOfWork.ClientRepository.UpdateAsync(existingClient);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(ClientDTO entity)
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
