namespace Aidn.Application.Errors;

public sealed record Error
{
    public required string PropertyName { get; set; }
    public required string Message { get; set; }
}
