﻿namespace ProductManagment.Domain.Base
{
    public interface IService<T, TId> where T : EntityBase<TId>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
