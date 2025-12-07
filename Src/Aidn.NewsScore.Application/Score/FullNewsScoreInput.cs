namespace Aidn.NewsScore.Application.Score;

public sealed record FullNewsScoreInput
{
    public required int HeartRate { get; init; }
    public required int BodyTemperature { get; init; }
    public required int RespiratoryRate { get; init; }
}
