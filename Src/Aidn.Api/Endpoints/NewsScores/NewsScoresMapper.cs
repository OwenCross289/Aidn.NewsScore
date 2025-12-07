using Aidn.Api.Endpoints.NewsScores.CreateNewsScore;
using Aidn.Application.Score;

namespace Aidn.Api.Endpoints.NewsScores;

public static class NewsScoresMapper
{
    extension(CreateNewsScoreRequest request)
    {
        public FullNewsScoreInput ToInput()
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

    extension(NewsScoreDto dto)
    {
        public CreateNewsScoreResponse ToResponse()
        {
            return new CreateNewsScoreResponse { Score = dto.TotalScore };
        }
    }
}
