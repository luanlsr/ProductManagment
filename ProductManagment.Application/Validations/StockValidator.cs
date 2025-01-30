using FluentValidation;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Application.Validations
{
    public class StockValidator : AbstractValidator<Stock>
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
