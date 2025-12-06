using Aidn.Handler.News;
using Aidn.Handler.News.Commands;
using Aidn.NewsScore.Api.Endpoints.NewsScores.CreateNewsScore;

namespace Aidn.NewsScore.Api.Endpoints.NewsScores;

public static class NewsScoresMapper
{
    extension(CreateNewsScoreRequest request)
    {
        public CreateNewsScoreCommand ToCommand()
        {
            var measurements = request.Measurements.ToDictionary(m => m.Type, m => m.Value);

            return new CreateNewsScoreCommand
            {
                HeartRate = measurements.GetValueOrDefault(NewsScoresConstants.HeartRate),
                BodyTemperature = measurements.GetValueOrDefault(NewsScoresConstants.BodyTemperature),
                RespiratoryRate = measurements.GetValueOrDefault(NewsScoresConstants.RespiratoryRate),
            };
        }
    }

    extension(NewsScoreDto dto)
    {
        public CreateNewsScoreResponse ToResponse()
        {
            return new CreateNewsScoreResponse { Score = dto.TotalScore };
        }
    }
}
