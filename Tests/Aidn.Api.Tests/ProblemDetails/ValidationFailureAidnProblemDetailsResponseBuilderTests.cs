using Aidn.Api.ProblemDetails;
using Aidn.Constants;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;

namespace Aidn.Api.Tests.ProblemDetails;

public class ValidationFailureAidnProblemDetailsResponseBuilderTests
{
    [Fact]
    public void ValidationFailures_ValidationFailureAidnProblemDetailsResponseBuilder_ReturnsCorrectAidnProblemDetailsResponse()
    {
        // Arrange
        var validationFailures = new List<ValidationFailure>
        {
            new("FirstName", "First name is required") { ErrorCode = "REQUIRED_FIELD", Severity = Severity.Error },
            new("Email", "Email format is invalid") { ErrorCode = "INVALID_FORMAT", Severity = Severity.Warning },
        };

        var httpContext = new DefaultHttpContext
        {
            Response = { StatusCode = 400 },
            TraceIdentifier = "0HMPNHL0JHL76:00000001",
            Request = { Path = "/api/users" },
        };

        var expected = new AidnProblemDetailsResponse
        {
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
            Title = "Bad Request",
            Status = 400,
            Instance = "/api/users",
            TraceId = "0HMPNHL0JHL76:00000001",
            Detail = "A validation failure has occurred.",
            Errors =
            [
                new AidnError
                {
                    Name = "FirstName",
                    Reason = "First name is required",
                    Code = "REQUIRED_FIELD",
                    Severity = "Error",
                },
                new AidnError
                {
                    Name = "Email",
                    Reason = "Email format is invalid",
                    Code = "INVALID_FORMAT",
                    Severity = "Warning",
                },
            ],
        };

        // Act
        var actual = ProblemDetailsExtensions.ValidationFailureAidnProblemDetailsResponseBuilder(validationFailures, httpContext, 400);

        // Assert
        actual.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void EmptyValidationFailures_ValidationFailureAidnProblemDetailsResponseBuilder_ReturnsAidnProblemDetailsResponseWithEmptyErrors()
    {
        // Arrange
        var validationFailures = new List<ValidationFailure>();

        var httpContext = new DefaultHttpContext
        {
            Response = { StatusCode = 400 },
            TraceIdentifier = "0HMPNHL0JHL76:00000001",
            Request = { Path = "/api/users" },
        };

        var expected = new AidnProblemDetailsResponse
        {
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
            Title = "Bad Request",
            Status = 400,
            Instance = "/api/users",
            TraceId = "0HMPNHL0JHL76:00000001",
            Detail = "A validation failure has occurred.",
            Errors = [],
        };

        // Act
        var actual = ProblemDetailsExtensions.ValidationFailureAidnProblemDetailsResponseBuilder(validationFailures, httpContext, 400);

        // Assert
        actual.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ValidationFailuresWithNullValues_ValidationFailureAidnProblemDetailsResponseBuilder_HandlesNullsCorrectly()
    {
        // Arrange
        var validationFailures = new List<ValidationFailure>
        {
            new(null, null) { ErrorCode = null, Severity = Severity.Error },
        };

        var httpContext = new DefaultHttpContext
        {
            Response = { StatusCode = 400 },
            TraceIdentifier = "0HMPNHL0JHL76:00000001",
            Request = { Path = "/api/resource" },
        };

        var expected = new AidnProblemDetailsResponse
        {
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
            Title = "Bad Request",
            Status = 400,
            Instance = "/api/resource",
            TraceId = "0HMPNHL0JHL76:00000001",
            Detail = "A validation failure has occurred.",
            Errors =
            [
                new AidnError
                {
                    Name = "Unknown",
                    Reason = "Unknown error",
                    Code = ErrorCodeConstants.BadRequest,
                    Severity = "Error",
                },
            ],
        };

        // Act
        var actual = ProblemDetailsExtensions.ValidationFailureAidnProblemDetailsResponseBuilder(validationFailures, httpContext, 400);

        // Assert
        actual.ShouldBeEquivalentTo(expected);
    }

    [Fact]
    public void ValidationFailuresWithDifferentSeverities_ValidationFailureAidnProblemDetailsResponseBuilder_MaintainsSeverities()
    {
        // Arrange
        var validationFailures = new List<ValidationFailure>
        {
            new("Field1", "Error message") { Severity = Severity.Error, ErrorCode = "CODE1" },
            new("Field2", "Warning message") { Severity = Severity.Warning, ErrorCode = "CODE2" },
            new("Field3", "Info message") { Severity = Severity.Info, ErrorCode = "CODE3" },
        };

        var httpContext = new DefaultHttpContext
        {
            Response = { StatusCode = 400 },
            TraceIdentifier = "0HMPNHL0JHL76:00000001",
            Request = { Path = "/api/resource" },
        };

        var expected = new AidnProblemDetailsResponse
        {
            Type = "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1",
            Title = "Bad Request",
            Status = 400,
            Instance = "/api/resource",
            TraceId = "0HMPNHL0JHL76:00000001",
            Detail = "A validation failure has occurred.",
            Errors =
            [
                new AidnError
                {
                    Name = "Field1",
                    Reason = "Error message",
                    Severity = "Error",
                    Code = "CODE1",
                },
                new AidnError
                {
                    Name = "Field2",
                    Reason = "Warning message",
                    Severity = "Warning",
                    Code = "CODE2",
                },
                new AidnError
                {
                    Name = "Field3",
                    Reason = "Info message",
                    Severity = "Info",
                    Code = "CODE3",
                },
            ],
        };

        // Act
        var actual = ProblemDetailsExtensions.ValidationFailureAidnProblemDetailsResponseBuilder(validationFailures, httpContext, 400);

        // Assert
        actual.ShouldBeEquivalentTo(expected);
    }
}
