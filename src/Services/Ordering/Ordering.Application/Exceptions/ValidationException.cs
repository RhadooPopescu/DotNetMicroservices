using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ordering.Application.Exceptions
{
    //This class represents our custom validation exception for our command validator classes.
    public class ValidationException : ApplicationException
    {
        //Error property.
        public IDictionary<string, string[]> Errors { get; }

        //This constructor defines our message body.
        public ValidationException()
            : base("One or more validation features have failed.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        //This constructor defines the validation fields that were defined in the command validator classes, and store them in the Errors dictionary.
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        
    }
}
