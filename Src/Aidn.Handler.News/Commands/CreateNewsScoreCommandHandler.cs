using FastEndpoints;

namespace Aidn.Handler.News.Commands;

public class CreateNewsScoreCommandHandler : ICommandHandler<CreateNewsScoreCommand, NewsScoreDto>
{
    public Task<NewsScoreDto> ExecuteAsync(CreateNewsScoreCommand command, CancellationToken ct)
    {
        var heartRateScore = CalculateHeartRateScore(command.HeartRate);
        var bodyTemperatureScore = CalculateBodyTemperatureScore(command.BodyTemperature);
        var respiratoryRateScore = CalculateRespiratoryRateScore(command.RespiratoryRate);
        var totalScore = heartRateScore + bodyTemperatureScore + respiratoryRateScore;
        return Task.FromResult(
            new NewsScoreDto
            {
                TotalScore = totalScore,
                HeartRateScore = heartRateScore,
                BodyTemperatureScore = bodyTemperatureScore,
                RespiratoryRateScore = respiratoryRateScore,
            }
        );
    }

    public int CalculateHeartRateScore(int heartRate)
    {
        return 0;
    }

    public int CalculateBodyTemperatureScore(int bodyTemperature)
    {
        return 0;
    }

    public int CalculateRespiratoryRateScore(int respiratoryRate)
    {
        return 0;
    }
}
