using ProductManagment.Domain.Core.Base;

namespace ProductManagment.Domain.Core.Interface
{
    public interface IService<T, TId> where T : EntityBaseDTO<TId>
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
