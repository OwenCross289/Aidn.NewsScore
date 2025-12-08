namespace Aidn.Api.Endpoints.NewsScores.CreateNewsScore;

/// <summary>
/// NEWS Score response.
/// </summary>
public sealed record CreateNewsScoreResponse
{
    /// <summary>
    /// NEWS Score based on patient measurements.
    /// </summary>
    public required int Score { get; init; }
}
