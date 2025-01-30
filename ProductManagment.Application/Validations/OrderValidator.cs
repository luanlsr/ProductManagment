using FluentValidation;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Application.Validations
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.ClientId)
                .NotEmpty().WithMessage("Client ID is required.");

            RuleFor(o => o.Items)
                .NotEmpty().WithMessage("Order must contain at least one item.");

            RuleFor(o => o.Total)
                .GreaterThan(0).WithMessage("Order total must be greater than zero.");

            RuleFor(o => o.Status)
                .IsInEnum().WithMessage("Invalid order status.");
        }
    }
}
