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
}
