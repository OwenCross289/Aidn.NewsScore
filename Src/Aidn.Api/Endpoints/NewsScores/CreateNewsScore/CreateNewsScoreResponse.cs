namespace Aidn.Api.Endpoints.NewsScores.CreateNewsScore;

public sealed record CreateNewsScoreResponse
{
    public required int Score { get; init; }
}
