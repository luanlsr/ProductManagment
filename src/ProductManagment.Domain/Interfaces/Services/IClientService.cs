﻿using ProductManagment.Domain.Core.Interface;
using ProductManagment.Domain.DTOs;

namespace ProductManagment.Domain.Interfaces.Services
{
    public interface IClientService : IService<ClientDTO, Guid>
    {
        Task<int> GetCountAsync();
        Task<ClientDTO> GetByNameAsync(string name);
    }
}
