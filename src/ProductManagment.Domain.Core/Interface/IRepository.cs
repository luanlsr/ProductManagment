using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagment.Domain.Core.Interface
{
    public interface IRepository<TEntity, in TId> where TEntity : class where TId : struct
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<List<TEntity>> ListAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetByIdAsync(TId id, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null); 
        Task<TEntity> GetByNameAsync(Expression<Func<TEntity, string>> nameSelector, string name, params Expression<Func<TEntity, object>>[] includeProperties);
        Task DeleteAsync(TId id);
        void Dispose();

    }
}
