using Aidn.Application.Errors;
using FluentValidation.Results;

namespace Aidn.Api.Validation;

public static class ValidationExtensions
{
    extension<T>(IEnumerable<T> enumerable)
    {
        public string ToValidationMessageFormat()
        {
            return string.Join(", ", enumerable.Select(t => $"'{t}'"));
        }
    }

    extension(Error error)
    {
        public ValidationFailure ToValidationFailure()
        {
            return new ValidationFailure(error.PropertyName, error.Message);
        }
    }
}
