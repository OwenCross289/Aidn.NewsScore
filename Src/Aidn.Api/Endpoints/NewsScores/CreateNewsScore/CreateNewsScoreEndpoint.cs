using Aidn.Api.Validation;
using Aidn.Application.Score;
using FastEndpoints;

namespace Aidn.Api.Endpoints.NewsScores.CreateNewsScore;

public class CreateNewsScoreEndpoint : Endpoint<CreateNewsScoreRequest, CreateNewsScoreResponse>
{
    private const string _route = Endpoints.Routes.NewsScores.Create;

    public override void Configure()
    {
        Post(_route);
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateNewsScoreRequest req, CancellationToken ct)
    {
        var result = NewsScoreCalculator.CalculateFullScore(req.ToInput());

        if (result.IsT1)
        {
            foreach (var error in result.AsT1)
            {
                ValidationFailures.Add(error.ToValidationFailure());
            }

            await Send.ErrorsAsync(cancellation: ct);
        }
        else
        {
            await Send.OkAsync(result.AsT0.ToResponse(), ct);
        }
    }
}
