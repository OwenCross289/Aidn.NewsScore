namespace Aidn.Api.Endpoints.NewsScores.CreateNewsScore;

public sealed record CreateNewsScoreRequest
{
    public List<Measurement> Measurements { get; init; } = [];
}

public sealed record Measurement
{
    public string Type { get; init; } = string.Empty;
    public int Value { get; init; }
}
