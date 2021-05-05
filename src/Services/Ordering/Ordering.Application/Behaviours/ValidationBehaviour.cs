using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = Ordering.Application.Exceptions.ValidationException;

namespace Ordering.Application.Behaviours
{
    //This class collects and manages all the validations fields required in order to manage an order.
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        //This field collects all the validation objects namely update order validator and checkout order validator.
        private readonly IEnumerable<IValidator<TRequest>> validators;

        //Constructor.
        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        //This method handles the validation fields one by one and checks for completion.
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (validators.Any())
            {
                //Getting the context of the validation field.
                ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
                //Select validators fields one by one and perfrom the validate async method. 
                ValidationResult[] validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                //Check for failed validation results and store them in the ValidationException dictionary.
                List<ValidationFailure> failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                if (failures.Count != 0)
                    throw new ValidationException(failures);
            }
            return await next();
        }
    }
}
