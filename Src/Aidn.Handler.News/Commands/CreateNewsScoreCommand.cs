using FastEndpoints;

namespace Aidn.Handler.News.Commands;

public sealed record CreateNewsScoreCommand : ICommand<NewsScoreDto>
{
    public required int HeartRate { get; init; }
    public required int BodyTemperature { get; init; }
    public required int RespiratoryRate { get; init; }
}
