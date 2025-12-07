using Aidn.Api.Endpoints.NewsScores;
using Aidn.Api.Endpoints.NewsScores.CreateNewsScore;
using Aidn.Application.Score;
using Aidn.Constants;

namespace Aidn.Api.Tests.NewsScores;

public sealed class NewsScoreMappersTests
{
    [Fact]
    public void ToInput_WithAllMeasurements_ShouldMapCorrectly()
    {
        // Arrange
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = NewsScoresConstants.Types.HeartRate, Value = 80 },
                new Measurement { Type = NewsScoresConstants.Types.BodyTemperature, Value = 37 },
                new Measurement { Type = NewsScoresConstants.Types.RespiratoryRate, Value = 16 },
            ],
        };

        // Act
        var result = request.ToInput();

        // Assert
        result.HeartRate.ShouldBe(80);
        result.BodyTemperature.ShouldBe(37);
        result.RespiratoryRate.ShouldBe(16);
    }

    [Fact]
    public void ToInput_WithMissingMeasurement_ShouldDefaultToZero()
    {
        // Arrange
        var request = new CreateNewsScoreRequest { Measurements = [new Measurement { Type = NewsScoresConstants.Types.HeartRate, Value = 80 }] };

        // Act
        var result = request.ToInput();

        // Assert
        result.HeartRate.ShouldBe(80);
        result.BodyTemperature.ShouldBe(0);
        result.RespiratoryRate.ShouldBe(0);
    }

    [Fact]
    public void ToInput_WithEmptyMeasurements_ShouldDefaultAllToZero()
    {
        // Arrange
        var request = new CreateNewsScoreRequest { Measurements = [] };

        // Act
        var result = request.ToInput();

        // Assert
        result.HeartRate.ShouldBe(0);
        result.BodyTemperature.ShouldBe(0);
        result.RespiratoryRate.ShouldBe(0);
    }

    [Fact]
    public void ToResponse_ShouldMapTotalScoreCorrectly()
    {
        // Arrange
        var dto = new NewsScoreDto
        {
            TotalScore = 5,
            HeartRateScore = 2,
            BodyTemperatureScore = 1,
            RespiratoryRateScore = 2,
        };

        // Act
        var result = dto.ToResponse();

        // Assert
        result.Score.ShouldBe(5);
    }
}
