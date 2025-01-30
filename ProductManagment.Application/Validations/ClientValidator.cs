using FluentValidation;
using ProductManagment.Domain.Entities;

namespace ProductManagment.Application.Validations
{
    public class ClientValidator : AbstractValidator<Client>
    {
        public ClientValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Client name is required.")
                .Length(3, 100).WithMessage("Client name must be between 3 and 100 characters.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(c => c.Phone)
                .Matches(@"^\+?\d{10,15}$").WithMessage("Invalid phone number.");

            RuleFor(c => c.Document)
                .NotEmpty().WithMessage("Document is required.")
                .Length(11, 14).WithMessage("Document must be between 11 and 14 characters.");
        }
    }
}
