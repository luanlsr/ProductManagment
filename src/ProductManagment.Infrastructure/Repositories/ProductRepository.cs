using Microsoft.EntityFrameworkCore;
using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces.Repositories;
using ProductManagment.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductManagment.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtém um produto pelo ID com includes opcionais
        /// </summary>
        public override async Task<Product> GetByIdAsync(Guid id, params Expression<Func<Product, object>>[] includeProperties)
        {
            IQueryable<Product> query = _context.Products;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Obtém uma lista de produtos com includes opcionais
        /// </summary>
        public override async Task<List<Product>> ListAsync(params Expression<Func<Product, object>>[] includeProperties)
        {
            IQueryable<Product> query = _context.Products;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Obtém um produto pelo nome com includes opcionais
        /// </summary>
        public override async Task<Product> GetByNameAsync(
            Expression<Func<Product, string>> nameSelector,
            string name,
            params Expression<Func<Product, object>>[] includeProperties)
        {
            IQueryable<Product> query = _context.Products;

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(p => EF.Property<string>(p, nameSelector.Parameters[0].Name).Equals(name));
        }
    }
}
