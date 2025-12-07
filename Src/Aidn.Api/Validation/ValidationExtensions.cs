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
}
