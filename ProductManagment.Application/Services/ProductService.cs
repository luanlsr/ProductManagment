using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces;
using ProductManagment.Domain.Interfaces.Services;
using ProductManagment.Domain.Exceptions;
using ProductManagment.Application.Validations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using FluentValidation;

namespace ProductManagment.Application.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<Product> _validator;

        public ProductService(IUnitOfWork unitOfWork, IValidator<Product> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task AddAsync(Product entity)
        {
            Validate(entity, _validator);

            await _unitOfWork.ProductRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await _unitOfWork.ProductRepository.ListAsync();
            if (products == null)
                throw new NotFoundException("No products found.");

            return products;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationException("Invalid product ID.");

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException($"Product with ID {id} not found.");

            return product;
        }

        public async Task UpdateAsync(Product entity)
        {
            Validate(entity, _validator);

            var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(entity.Id);
            if (existingProduct == null)
                throw new NotFoundException($"Product with ID {entity.Id} not found.");

            existingProduct.Update(entity.Name, entity.Description, entity.Price, entity.Category, entity.SKU);

            await _unitOfWork.ProductRepository.UpdateAsync(existingProduct);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Product entity)
        {
            if (entity == null)
                throw new ValidationException("Product cannot be null.");

            var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(entity.Id);
            if (existingProduct == null)
                throw new NotFoundException($"Product with ID {entity.Id} not found.");

            await _unitOfWork.ProductRepository.DeleteAsync(existingProduct.Id);
            await _unitOfWork.CommitAsync();
        }
    }
}
