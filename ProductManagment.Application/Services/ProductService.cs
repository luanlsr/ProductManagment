using ProductManagment.Domain.Entities;
using ProductManagment.Domain.Interfaces;
using ProductManagment.Domain.Interfaces.Services;
using ProductManagment.Domain.Exceptions;
using ProductManagment.Application.Validations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using FluentValidation;
using ProductManagment.Domain.DTOs;
using AutoMapper;
using ProductManagment.Domain.Core.Interface;

namespace ProductManagment.Application.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<ProductDTO> _validator;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IValidator<ProductDTO> validator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task AddAsync(ProductDTO productDto)
        {
            Validate(productDto, _validator);
            var entity = _mapper.Map<Product>(productDto);

            await _unitOfWork.ProductRepository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _unitOfWork.ProductRepository.ListAsync();
            if (products == null)
                throw new NotFoundException("No products found.");

            var productsDTO = _mapper.Map<List<ProductDTO>>(products);

            return productsDTO;
        }

        public async Task<ProductDTO> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ValidationException("Invalid product ID.");

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException($"Product with ID {id} not found.");

            var productDTO = _mapper.Map<ProductDTO>(product);

            return productDTO;
        }

        public async Task UpdateAsync(ProductDTO productDto)
        {
            Validate(productDto, _validator);

            var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(productDto.Id);
            if (existingProduct == null)
                throw new NotFoundException($"Product with ID {productDto.Id} not found.");

            existingProduct.Update(productDto.Name, productDto.Description, productDto.Price, productDto.Category, productDto.SKU);

            await _unitOfWork.ProductRepository.UpdateAsync(existingProduct);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(ProductDTO productDto)
        {
            if (productDto == null)
                throw new ValidationException("Product cannot be null.");

            var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(productDto.Id);
            if (existingProduct == null)
                throw new NotFoundException($"Product with ID {productDto.Id} not found.");

            await _unitOfWork.ProductRepository.DeleteAsync(existingProduct.Id);
            await _unitOfWork.CommitAsync();
        }
    }
}
