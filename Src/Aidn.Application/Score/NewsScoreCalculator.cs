using Aidn.Application.Errors;
using OneOf;

namespace Aidn.Application.Score;

public static class NewsScoreCalculator
{
    public static OneOf<NewsScoreDto, Error[]> CalculateFullScore(FullNewsScoreInput input)
    {
        var heartRateResult = CalculateHeartRateScore(input.HeartRate);
        var bodyTemperatureResult = CalculateBodyTemperatureScore(input.BodyTemperature);
        var respiratoryRateResult = CalculateRespiratoryRateScore(input.RespiratoryRate);

        var errors = new List<Error>();

        if (heartRateResult.IsT1)
        {
            errors.Add(heartRateResult.AsT1);
        }

        if (bodyTemperatureResult.IsT1)
        {
            errors.Add(bodyTemperatureResult.AsT1);
        }

        if (respiratoryRateResult.IsT1)
        {
            errors.Add(respiratoryRateResult.AsT1);
        }

        if (errors.Count != 0)
        {
            return errors.ToArray();
        }

        var heartRateScore = heartRateResult.AsT0;
        var bodyTemperatureScore = bodyTemperatureResult.AsT0;
        var respiratoryRateScore = respiratoryRateResult.AsT0;

        var totalScore = heartRateScore + bodyTemperatureScore + respiratoryRateScore;

        return new NewsScoreDto
        {
            TotalScore = totalScore,
            HeartRateScore = heartRateScore,
            BodyTemperatureScore = bodyTemperatureScore,
            RespiratoryRateScore = respiratoryRateScore,
        };
    }

    public static OneOf<int, Error> CalculateHeartRateScore(int heartRate)
    {
        return heartRate switch
        {
            > 25 and <= 40 => 3,
            > 40 and <= 50 => 1,
            > 50 and <= 90 => 0,
            > 90 and <= 110 => 1,
            > 110 and <= 130 => 2,
            > 130 and <= 220 => 3,
            _ => new Error
            {
                PropertyName = "HR",
                Message = $"'HR' must be a value greater than 25 and less than or equal to 220. You input '{heartRate}'.",
            },
        };
    }

    public static OneOf<int, Error> CalculateBodyTemperatureScore(int bodyTemperature)
    {
        return bodyTemperature switch
        {
            > 31 and <= 35 => 3,
            > 35 and <= 36 => 1,
            > 36 and <= 38 => 0,
            > 38 and <= 39 => 1,
            > 39 and <= 42 => 2,
            _ => new Error
            {
                PropertyName = "TEMP",
                Message = $"'TEMP' must be a value greater than 31 and less than or equal to 42. You input '{bodyTemperature}'.",
            },
        };
    }

    public static OneOf<int, Error> CalculateRespiratoryRateScore(int respiratoryRate)
    {
        return respiratoryRate switch
        {
            > 3 and <= 8 => 3,
            > 8 and <= 11 => 1,
            > 11 and <= 20 => 0,
            > 20 and <= 24 => 2,
            > 24 and <= 60 => 3,
            _ => new Error
            {
                PropertyName = "RR",
                Message = $"'RR' must be a value greater than 3 and less than or equal to 60. You input '{respiratoryRate}'",
            },
        };
    }
}
