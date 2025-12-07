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
        result.AsT1.Message.ShouldContain(respiratoryRate.ToString());
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
        result.AsT1.Message.ShouldContain(heartRate.ToString());
    }
}
