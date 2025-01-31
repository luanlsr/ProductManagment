using FluentValidation;
using ProductManagment.Domain.DTOs;

namespace ProductManagment.Application.Validations
{
    public class StockValidator : AbstractValidator<StockDTO>
    {
        public StockValidator()
        {
            RuleFor(s => s.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");

            RuleFor(s => s.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.");
        }
    }
}
