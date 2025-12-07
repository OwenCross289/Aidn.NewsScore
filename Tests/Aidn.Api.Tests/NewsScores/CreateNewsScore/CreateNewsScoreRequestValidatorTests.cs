using Aidn.Api.Endpoints.NewsScores.CreateNewsScore;
using Aidn.Constants;

namespace Aidn.Api.Tests.NewsScores.CreateNewsScore;

public class CreateNewsScoreRequestValidatorTests
{
    private readonly CreateNewsScoreRequestValidator _validator = new();

    [Fact]
    public void Validate_WithAllValidMeasurements_ShouldPassValidation()
    {
        // Arrange
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = NewsScoresConstants.Types.HeartRate, Value = 80 },
                new Measurement { Type = NewsScoresConstants.Types.RespiratoryRate, Value = 16 },
                new Measurement { Type = NewsScoresConstants.Types.BodyTemperature, Value = 37 },
            ],
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeTrue();
        result.Errors.ShouldBeEmpty();
    }

    [Fact]
    public void Validate_WithMissingMeasurementTypes_ShouldFailValidation()
    {
        // Arrange
        var request = new CreateNewsScoreRequest { Measurements = [new Measurement { Type = NewsScoresConstants.Types.HeartRate, Value = 80 }] };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result
            .Errors.Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage,
                e.ErrorCode,
            })
            .ToArray()
            .ShouldBeEquivalentTo(
                new[]
                {
                    new
                    {
                        PropertyName = "Measurements",
                        ErrorMessage = "'Measurements' must contain exactly one of each of the following: 'HR', 'RR', 'TEMP'. Missing type(s): 'RR', 'TEMP'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.MissingMeasurementTypes,
                    },
                }
            );
    }

    [Fact]
    public void Validate_WithDuplicateMeasurementTypes_ShouldFailValidation()
    {
        // Arrange
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = NewsScoresConstants.Types.HeartRate, Value = 80 },
                new Measurement { Type = NewsScoresConstants.Types.HeartRate, Value = 90 },
                new Measurement { Type = NewsScoresConstants.Types.RespiratoryRate, Value = 16 },
                new Measurement { Type = NewsScoresConstants.Types.BodyTemperature, Value = 37 },
            ],
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result
            .Errors.Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage,
                e.ErrorCode,
            })
            .ToArray()
            .ShouldBeEquivalentTo(
                new[]
                {
                    new
                    {
                        PropertyName = "Measurements",
                        ErrorMessage = "'Measurements' must contain exactly one of each of the following: 'HR', 'RR', 'TEMP'. Duplicate type(s): 'HR'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.DuplicatesMeasurementTypes,
                    },
                }
            );
    }

    [Fact]
    public void Validate_WithUnexpectedMeasurementType_ShouldFailValidation()
    {
        // Arrange
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = NewsScoresConstants.Types.HeartRate, Value = 80 },
                new Measurement { Type = NewsScoresConstants.Types.RespiratoryRate, Value = 16 },
                new Measurement { Type = NewsScoresConstants.Types.BodyTemperature, Value = 37 },
                new Measurement { Type = "UNKNOWN", Value = 100 },
            ],
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result
            .Errors.Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage,
                e.ErrorCode,
            })
            .ToArray()
            .ShouldBeEquivalentTo(
                new[]
                {
                    new
                    {
                        PropertyName = "Measurements",
                        ErrorMessage = "'Measurements' must contain exactly one of each of the following: 'HR', 'RR', 'TEMP'. Unknown type(s): 'UNKNOWN'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.InvalidMeasurementTypes,
                    },
                    new
                    {
                        PropertyName = "Measurements[3].Type",
                        ErrorMessage = "Measurement type must be one of: 'HR', 'RR', 'TEMP'. You input 'UNKNOWN'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.InvalidMeasurementType,
                    },
                }
            );
    }

    [Fact]
    public void Validate_WithInvalidMeasurementTypeInChildRule_ShouldFailValidation()
    {
        // Arrange
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = "INVALID", Value = 80 },
                new Measurement { Type = NewsScoresConstants.Types.RespiratoryRate, Value = 16 },
                new Measurement { Type = NewsScoresConstants.Types.BodyTemperature, Value = 37 },
            ],
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result
            .Errors.Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage,
                e.ErrorCode,
            })
            .ToArray()
            .ShouldBeEquivalentTo(
                new[]
                {
                    new
                    {
                        PropertyName = "Measurements",
                        ErrorMessage = "'Measurements' must contain exactly one of each of the following: 'HR', 'RR', 'TEMP'. Missing type(s): 'HR'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.MissingMeasurementTypes,
                    },
                    new
                    {
                        PropertyName = "Measurements",
                        ErrorMessage = "'Measurements' must contain exactly one of each of the following: 'HR', 'RR', 'TEMP'. Unknown type(s): 'INVALID'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.InvalidMeasurementTypes,
                    },
                    new
                    {
                        PropertyName = "Measurements[0].Type",
                        ErrorMessage = "Measurement type must be one of: 'HR', 'RR', 'TEMP'. You input 'INVALID'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.InvalidMeasurementType,
                    },
                }
            );
    }

    [Fact]
    public void Validate_WithEmptyMeasurements_ShouldFailValidation()
    {
        // Arrange
        var request = new CreateNewsScoreRequest { Measurements = [] };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result
            .Errors.Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage,
                e.ErrorCode,
            })
            .ToArray()
            .ShouldBeEquivalentTo(
                new[]
                {
                    new
                    {
                        PropertyName = "Measurements",
                        ErrorMessage = "'Measurements' must contain exactly one of each of the following: 'HR', 'RR', 'TEMP'. Missing type(s): 'HR', 'RR', 'TEMP'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.MissingMeasurementTypes,
                    },
                }
            );
    }

    [Fact]
    public void Validate_WithMultipleDuplicateTypes_ShouldFailValidation()
    {
        // Arrange
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = NewsScoresConstants.Types.HeartRate, Value = 80 },
                new Measurement { Type = NewsScoresConstants.Types.HeartRate, Value = 90 },
                new Measurement { Type = NewsScoresConstants.Types.RespiratoryRate, Value = 16 },
                new Measurement { Type = NewsScoresConstants.Types.RespiratoryRate, Value = 18 },
                new Measurement { Type = NewsScoresConstants.Types.BodyTemperature, Value = 37 },
            ],
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result
            .Errors.Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage,
                e.ErrorCode,
            })
            .ToArray()
            .ShouldBeEquivalentTo(
                new[]
                {
                    new
                    {
                        PropertyName = "Measurements",
                        ErrorMessage = "'Measurements' must contain exactly one of each of the following: 'HR', 'RR', 'TEMP'. Duplicate type(s): 'HR', 'RR'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.DuplicatesMeasurementTypes,
                    },
                }
            );
    }

    [Fact]
    public void Validate_WithMultipleUnknownTypes_ShouldFailValidation()
    {
        // Arrange
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = NewsScoresConstants.Types.HeartRate, Value = 80 },
                new Measurement { Type = NewsScoresConstants.Types.RespiratoryRate, Value = 16 },
                new Measurement { Type = NewsScoresConstants.Types.BodyTemperature, Value = 37 },
                new Measurement { Type = "UNKNOWN1", Value = 100 },
                new Measurement { Type = "UNKNOWN2", Value = 200 },
            ],
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result
            .Errors.Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage,
                e.ErrorCode,
            })
            .ToArray()
            .ShouldBeEquivalentTo(
                new[]
                {
                    new
                    {
                        PropertyName = "Measurements",
                        ErrorMessage = "'Measurements' must contain exactly one of each of the following: 'HR', 'RR', 'TEMP'. Unknown type(s): 'UNKNOWN1', 'UNKNOWN2'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.InvalidMeasurementTypes,
                    },
                    new
                    {
                        PropertyName = "Measurements[3].Type",
                        ErrorMessage = "Measurement type must be one of: 'HR', 'RR', 'TEMP'. You input 'UNKNOWN1'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.InvalidMeasurementType,
                    },
                    new
                    {
                        PropertyName = "Measurements[4].Type",
                        ErrorMessage = "Measurement type must be one of: 'HR', 'RR', 'TEMP'. You input 'UNKNOWN2'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.InvalidMeasurementType,
                    },
                }
            );
    }

    [Fact]
    public void Validate_WithOnlyUnknownTypes_ShouldFailWithMultipleErrors()
    {
        // Arrange
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = "INVALID1", Value = 80 },
                new Measurement { Type = "INVALID2", Value = 16 },
                new Measurement { Type = "INVALID3", Value = 37 },
            ],
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result
            .Errors.Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage,
                e.ErrorCode,
            })
            .ToArray()
            .ShouldBeEquivalentTo(
                new[]
                {
                    new
                    {
                        PropertyName = "Measurements",
                        ErrorMessage = "'Measurements' must contain exactly one of each of the following: 'HR', 'RR', 'TEMP'. Missing type(s): 'HR', 'RR', 'TEMP'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.MissingMeasurementTypes,
                    },
                    new
                    {
                        PropertyName = "Measurements",
                        ErrorMessage = "'Measurements' must contain exactly one of each of the following: 'HR', 'RR', 'TEMP'. Unknown type(s): 'INVALID1', 'INVALID2', 'INVALID3'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.InvalidMeasurementTypes,
                    },
                    new
                    {
                        PropertyName = "Measurements[0].Type",
                        ErrorMessage = "Measurement type must be one of: 'HR', 'RR', 'TEMP'. You input 'INVALID1'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.InvalidMeasurementType,
                    },
                    new
                    {
                        PropertyName = "Measurements[1].Type",
                        ErrorMessage = "Measurement type must be one of: 'HR', 'RR', 'TEMP'. You input 'INVALID2'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.InvalidMeasurementType,
                    },
                    new
                    {
                        PropertyName = "Measurements[2].Type",
                        ErrorMessage = "Measurement type must be one of: 'HR', 'RR', 'TEMP'. You input 'INVALID3'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.InvalidMeasurementType,
                    },
                }
            );
    }

    [Fact]
    public void Validate_WithMissingAndDuplicateTypes_ShouldFailWithMultipleErrors()
    {
        // Arrange
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = NewsScoresConstants.Types.HeartRate, Value = 80 },
                new Measurement { Type = NewsScoresConstants.Types.HeartRate, Value = 90 },
                new Measurement { Type = NewsScoresConstants.Types.RespiratoryRate, Value = 16 },
            ],
        };

        // Act
        var result = _validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeFalse();
        result
            .Errors.Select(e => new
            {
                e.PropertyName,
                e.ErrorMessage,
                e.ErrorCode,
            })
            .ToArray()
            .ShouldBeEquivalentTo(
                new[]
                {
                    new
                    {
                        PropertyName = "Measurements",
                        ErrorMessage = "'Measurements' must contain exactly one of each of the following: 'HR', 'RR', 'TEMP'. Missing type(s): 'TEMP'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.MissingMeasurementTypes,
                    },
                    new
                    {
                        PropertyName = "Measurements",
                        ErrorMessage = "'Measurements' must contain exactly one of each of the following: 'HR', 'RR', 'TEMP'. Duplicate type(s): 'HR'",
                        ErrorCode = NewsScoresConstants.ErrorCodes.DuplicatesMeasurementTypes,
                    },
                }
            );
    }
}
