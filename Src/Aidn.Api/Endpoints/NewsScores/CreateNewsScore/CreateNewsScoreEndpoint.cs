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
        await Send.OkAsync(new CreateNewsScoreResponse() { Score = 1 }, ct);
    }
}
