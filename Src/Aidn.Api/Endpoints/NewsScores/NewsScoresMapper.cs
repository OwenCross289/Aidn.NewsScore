using Aidn.Api.Endpoints.NewsScores.CreateNewsScore;
using Aidn.Application.Score;
using Aidn.Constants;

namespace Aidn.Api.Endpoints.NewsScores;

public static class NewsScoresMappers
{
    extension(CreateNewsScoreRequest request)
    {
        public FullNewsScoreInput ToInput()
        {
            var measurements = request.Measurements.ToDictionary(m => m.Type, m => m.Value);

            return new FullNewsScoreInput
            {
                HeartRate = measurements.GetValueOrDefault(NewsScoresConstants.Types.HeartRate),
                BodyTemperature = measurements.GetValueOrDefault(NewsScoresConstants.Types.BodyTemperature),
                RespiratoryRate = measurements.GetValueOrDefault(NewsScoresConstants.Types.RespiratoryRate),
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
