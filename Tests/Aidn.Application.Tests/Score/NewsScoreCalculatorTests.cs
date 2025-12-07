using Aidn.Application.Score;
using Shouldly;

namespace Aidn.Application.Tests.Score;

public class NewsScoreCalculatorTests
{
    [Theory]
    [InlineData(4, 3)]
    [InlineData(8, 3)]
    public void CalculateRespiratoryRateScore_WhenBetween4And8_ReturnsScore3(int respiratoryRate, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateRespiratoryRateScore(respiratoryRate);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(9, 1)]
    [InlineData(11, 1)]
    public void CalculateRespiratoryRateScore_WhenBetween9And11_ReturnsScore1(int respiratoryRate, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateRespiratoryRateScore(respiratoryRate);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(12, 0)]
    [InlineData(20, 0)]
    public void CalculateRespiratoryRateScore_WhenBetween12And20_ReturnsScore0(int respiratoryRate, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateRespiratoryRateScore(respiratoryRate);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(21, 2)]
    [InlineData(24, 2)]
    public void CalculateRespiratoryRateScore_WhenBetween21And24_ReturnsScore2(int respiratoryRate, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateRespiratoryRateScore(respiratoryRate);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(25, 3)]
    [InlineData(60, 3)]
    public void CalculateRespiratoryRateScore_WhenBetween25And60_ReturnsScore3(int respiratoryRate, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateRespiratoryRateScore(respiratoryRate);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(3)]
    [InlineData(61)]
    [InlineData(-1)]
    public void CalculateRespiratoryRateScore_WhenOutOfRange_ReturnsError(int respiratoryRate)
    {
        var result = NewsScoreCalculator.CalculateRespiratoryRateScore(respiratoryRate);

        result.IsT1.ShouldBeTrue();
        result.AsT1.PropertyName.ShouldBe("RR");
        result.AsT1.Message.ShouldBe($"'RR' must be a value greater than 3 and less than or equal to 60. You input '{respiratoryRate}'");
    }

    [Theory]
    [InlineData(32, 3)]
    [InlineData(35, 3)]
    public void CalculateBodyTemperatureScore_WhenBetween32And35_ReturnsScore3(int bodyTemperature, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateBodyTemperatureScore(bodyTemperature);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(36, 1)]
    public void CalculateBodyTemperatureScore_WhenBetween36And36_ReturnsScore1(int bodyTemperature, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateBodyTemperatureScore(bodyTemperature);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(37, 0)]
    [InlineData(38, 0)]
    public void CalculateBodyTemperatureScore_WhenBetween37And38_ReturnsScore0(int bodyTemperature, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateBodyTemperatureScore(bodyTemperature);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(39, 1)]
    public void CalculateBodyTemperatureScore_When39_ReturnsScore1(int bodyTemperature, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateBodyTemperatureScore(bodyTemperature);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(40, 2)]
    [InlineData(42, 2)]
    public void CalculateBodyTemperatureScore_WhenBetween40And42_ReturnsScore2(int bodyTemperature, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateBodyTemperatureScore(bodyTemperature);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(31)]
    [InlineData(43)]
    [InlineData(-1)]
    public void CalculateBodyTemperatureScore_WhenOutOfRange_ReturnsError(int bodyTemperature)
    {
        var result = NewsScoreCalculator.CalculateBodyTemperatureScore(bodyTemperature);

        result.IsT1.ShouldBeTrue();
        result.AsT1.PropertyName.ShouldBe("TEMP");
        result.AsT1.Message.ShouldContain(bodyTemperature.ToString());
    }

    [Theory]
    [InlineData(26, 3)]
    [InlineData(40, 3)]
    public void CalculateHeartRateScore_WhenBetween26And40_ReturnsScore3(int heartRate, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateHeartRateScore(heartRate);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(41, 1)]
    [InlineData(50, 1)]
    public void CalculateHeartRateScore_WhenBetween41And50_ReturnsScore1(int heartRate, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateHeartRateScore(heartRate);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(51, 0)]
    [InlineData(90, 0)]
    public void CalculateHeartRateScore_WhenBetween51And90_ReturnsScore0(int heartRate, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateHeartRateScore(heartRate);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(91, 1)]
    [InlineData(110, 1)]
    public void CalculateHeartRateScore_WhenBetween91And110_ReturnsScore1(int heartRate, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateHeartRateScore(heartRate);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(111, 2)]
    [InlineData(130, 2)]
    public void CalculateHeartRateScore_WhenBetween111And130_ReturnsScore2(int heartRate, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateHeartRateScore(heartRate);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(131, 3)]
    [InlineData(220, 3)]
    public void CalculateHeartRateScore_WhenBetween131And220_ReturnsScore3(int heartRate, int expectedScore)
    {
        var result = NewsScoreCalculator.CalculateHeartRateScore(heartRate);

        result.IsT0.ShouldBeTrue();
        result.AsT0.ShouldBe(expectedScore);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(25)]
    [InlineData(221)]
    [InlineData(-1)]
    public void CalculateHeartRateScore_WhenOutOfRange_ReturnsError(int heartRate)
    {
        var result = NewsScoreCalculator.CalculateHeartRateScore(heartRate);

        result.IsT1.ShouldBeTrue();
        result.AsT1.PropertyName.ShouldBe("HR");
        result.AsT1.Message.ShouldBe($"'HR' must be a value greater than 25 and less than or equal to 220. You input '{heartRate}'.");
    }

    [Fact]
    public void CalculateFullScore_WhenAllInputsAreValid_ReturnsNewsScoreDto()
    {
        // Arrange - Using values that give known scores:
        // HeartRate: 60 (score 0), BodyTemperature: 37 (score 0), RespiratoryRate: 15 (score 0)
        var input = new FullNewsScoreInput
        {
            HeartRate = 60,
            BodyTemperature = 37,
            RespiratoryRate = 15,
        };

        // Act
        var result = NewsScoreCalculator.CalculateFullScore(input);

        // Assert
        result.IsT0.ShouldBeTrue();
        var dto = result.AsT0;
        dto.HeartRateScore.ShouldBe(0);
        dto.BodyTemperatureScore.ShouldBe(0);
        dto.RespiratoryRateScore.ShouldBe(0);
        dto.TotalScore.ShouldBe(0);
    }

    [Fact]
    public void CalculateFullScore_WhenAllInputsHaveHighScores_ReturnsSummedTotalScore()
    {
        // Arrange - Using values that give high scores:
        // HeartRate: 35 (score 3), BodyTemperature: 33 (score 3), RespiratoryRate: 5 (score 3)
        var input = new FullNewsScoreInput
        {
            HeartRate = 35,
            BodyTemperature = 33,
            RespiratoryRate = 5,
        };

        // Act
        var result = NewsScoreCalculator.CalculateFullScore(input);

        // Assert
        result.IsT0.ShouldBeTrue();
        var dto = result.AsT0;
        dto.HeartRateScore.ShouldBe(3);
        dto.BodyTemperatureScore.ShouldBe(3);
        dto.RespiratoryRateScore.ShouldBe(3);
        dto.TotalScore.ShouldBe(9);
    }

    [Fact]
    public void CalculateFullScore_WhenInputsHaveMixedScores_ReturnsCorrectTotalScore()
    {
        // Arrange - Using values that give different scores:
        // HeartRate: 45 (score 1), BodyTemperature: 41 (score 2), RespiratoryRate: 22 (score 2)
        var input = new FullNewsScoreInput
        {
            HeartRate = 45,
            BodyTemperature = 41,
            RespiratoryRate = 22,
        };

        // Act
        var result = NewsScoreCalculator.CalculateFullScore(input);

        // Assert
        result.IsT0.ShouldBeTrue();
        var dto = result.AsT0;
        dto.HeartRateScore.ShouldBe(1);
        dto.BodyTemperatureScore.ShouldBe(2);
        dto.RespiratoryRateScore.ShouldBe(2);
        dto.TotalScore.ShouldBe(5);
    }

    [Fact]
    public void CalculateFullScore_WhenHeartRateIsInvalid_ReturnsError()
    {
        // Arrange
        var input = new FullNewsScoreInput
        {
            HeartRate = 0,
            BodyTemperature = 37,
            RespiratoryRate = 15,
        };

        // Act
        var result = NewsScoreCalculator.CalculateFullScore(input);

        // Assert
        result.IsT1.ShouldBeTrue();
        var errors = result.AsT1;
        errors.Length.ShouldBe(1);
        errors[0].PropertyName.ShouldBe("HR");
    }

    [Fact]
    public void CalculateFullScore_WhenBodyTemperatureIsInvalid_ReturnsError()
    {
        // Arrange
        var input = new FullNewsScoreInput
        {
            HeartRate = 60,
            BodyTemperature = 0,
            RespiratoryRate = 15,
        };

        // Act
        var result = NewsScoreCalculator.CalculateFullScore(input);

        // Assert
        result.IsT1.ShouldBeTrue();
        var errors = result.AsT1;
        errors.Length.ShouldBe(1);
        errors[0].PropertyName.ShouldBe("TEMP");
        errors[0].Message.ShouldBe("'TEMP' must be a value greater than 31 and less than or equal to 42. You input '0'.");
    }

    [Fact]
    public void CalculateFullScore_WhenRespiratoryRateIsInvalid_ReturnsError()
    {
        // Arrange
        var input = new FullNewsScoreInput
        {
            HeartRate = 60,
            BodyTemperature = 37,
            RespiratoryRate = 0,
        };

        // Act
        var result = NewsScoreCalculator.CalculateFullScore(input);

        // Assert
        result.IsT1.ShouldBeTrue();
        var errors = result.AsT1;
        errors.Length.ShouldBe(1);
        errors[0].PropertyName.ShouldBe("RR");
        errors[0].Message.ShouldBe("'RR' must be a value greater than 3 and less than or equal to 60. You input '0'");
    }

    [Fact]
    public void CalculateFullScore_WhenMultipleInputsAreInvalid_ReturnsAllErrors()
    {
        // Arrange
        var input = new FullNewsScoreInput
        {
            HeartRate = 0,
            BodyTemperature = 0,
            RespiratoryRate = 0,
        };

        // Act
        var result = NewsScoreCalculator.CalculateFullScore(input);

        // Assert
        result.IsT1.ShouldBeTrue();
        var errors = result.AsT1;
        errors.Length.ShouldBe(3);
        errors.ShouldContain(e =>
            e.PropertyName == "HR" && e.Message == "'HR' must be a value greater than 25 and less than or equal to 220. You input '0'."
        );
        errors.ShouldContain(e =>
            e.PropertyName == "TEMP" && e.Message == "'TEMP' must be a value greater than 31 and less than or equal to 42. You input '0'."
        );
        errors.ShouldContain(e =>
            e.PropertyName == "RR" && e.Message == "'RR' must be a value greater than 3 and less than or equal to 60. You input '0'"
        );
    }

    [Fact]
    public void CalculateFullScore_WhenTwoInputsAreInvalid_ReturnsTwoErrors()
    {
        // Arrange
        var input = new FullNewsScoreInput
        {
            HeartRate = 0,
            BodyTemperature = 0,
            RespiratoryRate = 15,
        };

        // Act
        var result = NewsScoreCalculator.CalculateFullScore(input);

        // Assert
        result.IsT1.ShouldBeTrue();
        var errors = result.AsT1;
        errors.Length.ShouldBe(2);
        errors.ShouldContain(e =>
            e.PropertyName == "HR" && e.Message == "'HR' must be a value greater than 25 and less than or equal to 220. You input '0'."
        );
        errors.ShouldContain(e =>
            e.PropertyName == "TEMP" && e.Message == "'TEMP' must be a value greater than 31 and less than or equal to 42. You input '0'."
        );
    }
}
