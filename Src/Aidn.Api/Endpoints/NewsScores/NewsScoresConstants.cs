namespace Aidn.Api.Endpoints.NewsScores;

public sealed class NewsScoresConstants
{
    public const string HeartRate = "HR";
    public const string RespiratoryRate = "RR";
    public const string BodyTemperature = "TEMP";

    public static readonly HashSet<string> AllMeasurementTypes = [HeartRate, RespiratoryRate, BodyTemperature];
}
