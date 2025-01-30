using FluentValidation;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Application.Validations
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .Length(3, 150).WithMessage("Product name must be between 3 and 150 characters.");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(p => p.Category)
                .NotEmpty().WithMessage("Category is required.")
                .Length(3, 100).WithMessage("Category must be between 3 and 100 characters.");

            RuleFor(p => p.SKU)
                .NotEmpty().WithMessage("SKU is required.")
                .Length(5, 50).WithMessage("SKU must be between 5 and 50 characters.");
        }
    }
}
