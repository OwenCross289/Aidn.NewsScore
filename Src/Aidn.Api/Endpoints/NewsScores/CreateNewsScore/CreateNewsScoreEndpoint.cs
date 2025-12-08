using Aidn.Api.ProblemDetails;
using Aidn.Api.Validation;
using Aidn.Application.Score;
using Aidn.Constants;
using FastEndpoints;

namespace Aidn.Api.Endpoints.NewsScores.CreateNewsScore;

public class CreateNewsScoreEndpoint : Endpoint<CreateNewsScoreRequest, CreateNewsScoreResponse>
{
    private const string _route = Endpoints.Routes.NewsScores.Create;

    public override void Configure()
    {
        Post(_route);
        AllowAnonymous();
        Description(b =>
        {
            b.WithTags("NEWS Scores");
            b.Produces<CreateNewsScoreResponse>();
            b.Produces<AidnProblemDetailsResponse>(400, MimeTypeConstants.ProblemJson);
        });
    }

    public override async Task HandleAsync(CreateNewsScoreRequest req, CancellationToken ct)
    {
        var result = NewsScoreCalculator.CalculateFullScore(req.ToInput());

        await result.Match(
            async newsScore =>
            {
                await Send.OkAsync(newsScore.ToResponse(), ct);
            },
            async errors =>
            {
                ValidationFailures.AddRange(errors.Select(error => error.ToValidationFailure()));
                await Send.ErrorsAsync(cancellation: ct);
            }
        );
    }
}
