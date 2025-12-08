namespace Aidn.Api.Endpoints.NewsScores.CreateNewsScore;

/// <summary>
/// Request containing payload to create a news score for a patient.
/// </summary>
public sealed record CreateNewsScoreRequest
{
    /// <summary>
    /// List of measurements for the patient.
    /// </summary>
    public List<Measurement> Measurements { get; init; } = [];
}

/// <summary>
/// Measurement data for use in calculating NEWS score.
/// </summary>
public sealed record Measurement
{
    /// <summary>
    /// Type of measurement. Valid options are: 'HR', 'Temp', 'RR'
    /// </summary>
    public string Type { get; init; } = string.Empty;

    /// <summary>
    /// Value of the measurement
    /// </summary>
    public int Value { get; init; }
}
