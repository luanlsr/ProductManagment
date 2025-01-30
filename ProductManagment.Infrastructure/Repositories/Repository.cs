using Microsoft.EntityFrameworkCore;
using ProductManagment.Domain.Base;
using ProductManagment.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagment.Infrastructure.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        private readonly DbSet<TEntity> _dataSet;
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected AppDbContext DbContext => DbFactory.Init();

        public Repository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dataSet = DbContext.Set<TEntity>();
        }

        public Repository(DbContext context) => Context = context;

        protected readonly DbContext Context;

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dataSet.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<TEntity>> ListAsync(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dataSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.ToListAsync();
        }


        public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dataSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync(where);
        }

        public async Task<TEntity> GetByIdAsync(TId id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dataSet;
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.FirstOrDefaultAsync(e => EF.Property<TId>(e, "Id").Equals(id));
        }


        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dataSet.Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dataSet.Remove(entity);
                await Context.SaveChangesAsync();
            }
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
