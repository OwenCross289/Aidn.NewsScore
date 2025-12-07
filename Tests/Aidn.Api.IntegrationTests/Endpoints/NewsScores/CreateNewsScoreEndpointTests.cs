namespace Aidn.Api.IntegrationTests.Endpoints.NewsScores;

public class CreateNewsScoreEndpointTests(AidnApiWebApplicationFactory webApplicationFactory)
{
    private readonly HttpClient _client = webApplicationFactory.CreateClient();
}
