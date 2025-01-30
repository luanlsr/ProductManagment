using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagment.Domain.Base
{
    public interface IRepository<TEntity, in TId> where TEntity : class where TId : struct
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<List<TEntity>> ListAsync(params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetByIdAsync(TId id, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TId id);
        void Dispose();

    }
}
