using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrder
{
    //This class verifies the required fileds for updating an order.
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(p => p.UserName)
                .NotEmpty().WithMessage("{UserName} is required")
                .NotNull()
                .MaximumLength(20).WithMessage("{UserName} must not exceed 20 characters");

            RuleFor(p => p.TotalPrice)
                .NotEmpty().WithMessage("{TotalPrice} is required.")
                .GreaterThan(0).WithMessage("{TotalPrice} must be greater than zero.");

            RuleFor(p => p.FirstName)
               .NotEmpty().WithMessage("{FirstName} is required.")
               .NotNull();

            RuleFor(p => p.LastName)
               .NotEmpty().WithMessage("{LastName} is required.")
               .NotNull();

            RuleFor(p => p.EmailAddress)
               .NotEmpty().WithMessage("{EmailAddress} is required.")
               .NotNull();

            RuleFor(p => p.AddressLine)
               .NotEmpty().WithMessage("{AddressLine} is required.")
               .NotNull();

            RuleFor(p => p.Country)
               .NotEmpty().WithMessage("{Country} is required.")
               .NotNull();

            RuleFor(p => p.State)
               .NotEmpty().WithMessage("{State} is required.")
               .NotNull();

            RuleFor(p => p.ZipCode)
               .NotEmpty().WithMessage("{ZipCode} is required.")
               .NotNull();

            RuleFor(p => p.CardName)
               .NotEmpty().WithMessage("{CardName} is required.")
               .NotNull();

            RuleFor(p => p.CardNumber)
                .NotEmpty().WithMessage("{CardNumber} is required")
                .NotNull()
                .MaximumLength(16).WithMessage("{CardNumber} must not exceed 16 characters");

            RuleFor(p => p.Expiration)
                .NotEmpty().WithMessage("{Expiration} is required")
                .NotNull()
                .MaximumLength(5).WithMessage("{Expiration} must not exceed 5 characters");

            RuleFor(p => p.CVV)
                .NotEmpty().WithMessage("{CVV} is required")
                .NotNull()
                .MaximumLength(3).WithMessage("{CVV} must not exceed 3 characters");
        }
    }
}
