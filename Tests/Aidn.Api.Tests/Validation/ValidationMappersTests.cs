using Aidn.Api.Validation;
using Aidn.Application.Errors;
using FluentValidation;

namespace Aidn.Api.Tests.Validation;

public class ValidationMappersTests
{
    [Fact]
    public void ToValidationMessageFormat_WithEmptyEnumerable_ReturnsEmptyString()
    {
        // Arrange
        var enumerable = Enumerable.Empty<string>();

        // Act
        var result = enumerable.ToValidationMessageFormat();

        // Assert
        result.ShouldBeEmpty();
    }

    [Fact]
    public void ToValidationMessageFormat_WithSingleItem_ReturnsSingleQuotedItem()
    {
        // Arrange
        var enumerable = new[] { "test" };

        // Act
        var result = enumerable.ToValidationMessageFormat();

        // Assert
        result.ShouldBe("'test'");
    }

    [Fact]
    public void ToValidationMessageFormat_WithMultipleItems_ReturnsCommaSeparatedQuotedItems()
    {
        // Arrange
        var enumerable = new[] { "first", "second", "third" };

        // Act
        var result = enumerable.ToValidationMessageFormat();

        // Assert
        result.ShouldBe("'first', 'second', 'third'");
    }

    [Fact]
    public void ToValidationMessageFormat_WithIntegers_ReturnsCommaSeparatedQuotedIntegers()
    {
        // Arrange
        var enumerable = new[] { 1, 2, 3 };

        // Act
        var result = enumerable.ToValidationMessageFormat();

        // Assert
        result.ShouldBe("'1', '2', '3'");
    }

    [Fact]
    public void ToValidationFailure_MapsAllProperties()
    {
        // Arrange
        var error = new Error
        {
            PropertyName = "TestProperty",
            Message = "Test error message",
            ErrorCode = "ERROR_CODE",
        };

        // Act
        var result = error.ToValidationFailure();

        // Assert
        result.PropertyName.ShouldBe("TestProperty");
        result.ErrorMessage.ShouldBe("Test error message");
        result.ErrorCode.ShouldBe("ERROR_CODE");
        result.Severity.ShouldBe(Severity.Error);
    }

    [Fact]
    public void ToValidationFailure_WithEmptyValues_MapsEmptyValues()
    {
        // Arrange
        var error = new Error
        {
            PropertyName = "",
            Message = "",
            ErrorCode = "",
        };

        // Act
        var result = error.ToValidationFailure();

        // Assert
        result.PropertyName.ShouldBeEmpty();
        result.ErrorMessage.ShouldBeEmpty();
        result.ErrorCode.ShouldBeEmpty();
        result.Severity.ShouldBe(Severity.Error);
    }
}
