using Aidn.NewsScore.Api.Endpoints.NewsScores.CreateNewsScore;
using Aidn.NewsScore.Application;
using Aidn.NewsScore.Application.Score;

namespace Aidn.NewsScore.Api.Endpoints.NewsScores;

public static class NewsScoresMapper
{
    extension(CreateNewsScoreRequest request)
    {
        public FullNewsScoreInput ToCommand()
        {
            var measurements = request.Measurements.ToDictionary(m => m.Type, m => m.Value);

            return new FullNewsScoreInput
            {
                HeartRate = measurements.GetValueOrDefault(NewsScoresConstants.HeartRate),
                BodyTemperature = measurements.GetValueOrDefault(NewsScoresConstants.BodyTemperature),
                RespiratoryRate = measurements.GetValueOrDefault(NewsScoresConstants.RespiratoryRate),
            };
        }
    }

    extension(Application.Score.NewsScore dto)
    {
        public CreateNewsScoreResponse ToResponse()
        {
            return new CreateNewsScoreResponse { Score = dto.TotalScore };
        }
    }
}
