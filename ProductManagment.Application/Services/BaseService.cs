using FluentValidation;
using FluentValidation.Results;

namespace ProductManagment.Application.Services
{
    public abstract class BaseService
    {
        protected void Validate<T>(T entity, IValidator<T> validator)
        {
            var validationResult = validator.Validate(entity);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(FormatValidationErrors(validationResult));
            }
        }

        private string FormatValidationErrors(ValidationResult validationResult)
        {
            return string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
        }
    }
}
