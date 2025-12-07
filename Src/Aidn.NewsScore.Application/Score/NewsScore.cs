namespace Aidn.NewsScore.Application.Score;

public sealed record NewsScore
{
    public required int TotalScore { get; init; }
    public required int HeartRateScore { get; init; }
    public required int BodyTemperatureScore { get; init; }
    public required int RespiratoryRateScore { get; init; }
}
