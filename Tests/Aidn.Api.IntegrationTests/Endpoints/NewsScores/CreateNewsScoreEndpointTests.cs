using System.Diagnostics;
using System.Net;
using System.Net.Http.Json;
using Aidn.Api.Endpoints;
using Aidn.Api.Endpoints.NewsScores.CreateNewsScore;
using Aidn.Api.ProblemDetails;
using Argon;

namespace Aidn.Api.IntegrationTests.Endpoints.NewsScores;

public class CreateNewsScoreEndpointTests(AidnApiWebApplicationFactory webApplicationFactory)
{
    private const string _route = Routes.NewsScores.Create;
    private readonly HttpClient _client = webApplicationFactory.CreateClient();

    [Fact]
    public async Task Create_WhenMeasurementsAreMissing_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new CreateNewsScoreRequest { Measurements = [] };
        var ct = TestContext.Current.CancellationToken;

        // Act
        var response = await _client.PostAsJsonAsync(_route, request, ct);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetails = await response.Content.ReadFromJsonAsync<AidnProblemDetailsResponse>(ct);
        await Verify(problemDetails).ScrubMember("TraceId").ScrubMember("Instance");
    }

    [Fact]
    public async Task Create_WhenMeasurementTypeIsMissing_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = "HR", Value = 70 },
                new Measurement { Type = "TEMP", Value = 37 },
                // Missing RR
            ],
        };
        var ct = TestContext.Current.CancellationToken;

        // Act
        var response = await _client.PostAsJsonAsync(_route, request, ct);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetails = await response.Content.ReadFromJsonAsync<AidnProblemDetailsResponse>(ct);
        await Verify(problemDetails).ScrubMember("TraceId").ScrubMember("Instance");
    }

    [Fact]
    public async Task Create_WhenDuplicateMeasurementTypes_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = "HR", Value = 70 },
                new Measurement { Type = "HR", Value = 80 },
                new Measurement { Type = "TEMP", Value = 37 },
                new Measurement { Type = "RR", Value = 15 },
            ],
        };
        var ct = TestContext.Current.CancellationToken;

        // Act
        var response = await _client.PostAsJsonAsync(_route, request, ct);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetails = await response.Content.ReadFromJsonAsync<AidnProblemDetailsResponse>(ct);
        await Verify(problemDetails).ScrubMember("TraceId").ScrubMember("Instance");
    }

    [Fact]
    public async Task Create_WhenInvalidMeasurementType_ShouldReturnBadRequest()
    {
        // Arrange
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = "HR", Value = 70 },
                new Measurement { Type = "TEMP", Value = 37 },
                new Measurement { Type = "RR", Value = 15 },
                new Measurement { Type = "INVALID", Value = 100 },
            ],
        };
        var ct = TestContext.Current.CancellationToken;

        // Act
        var response = await _client.PostAsJsonAsync(_route, request, ct);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetails = await response.Content.ReadFromJsonAsync<AidnProblemDetailsResponse>(ct);
        await Verify(problemDetails).ScrubMember("TraceId").ScrubMember("Instance");
    }

    [Fact]
    public async Task Create_WhenHeartRateOutOfRange_ShouldReturnBadRequest()
    {
        // Arrange - Heart rate must be > 25 and <= 220
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = "HR", Value = 25 }, // Out of range (must be > 25)
                new Measurement { Type = "TEMP", Value = 37 },
                new Measurement { Type = "RR", Value = 15 },
            ],
        };
        var ct = TestContext.Current.CancellationToken;

        // Act
        var response = await _client.PostAsJsonAsync(_route, request, ct);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetails = await response.Content.ReadFromJsonAsync<AidnProblemDetailsResponse>(ct);
        await Verify(problemDetails).ScrubMember("TraceId").ScrubMember("Instance");
    }

    [Fact]
    public async Task Create_WhenBodyTemperatureOutOfRange_ShouldReturnBadRequest()
    {
        // Arrange - Body temperature must be > 31 and <= 42
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = "HR", Value = 70 },
                new Measurement { Type = "TEMP", Value = 50 }, // Out of range (must be <= 42)
                new Measurement { Type = "RR", Value = 15 },
            ],
        };
        var ct = TestContext.Current.CancellationToken;

        // Act
        var response = await _client.PostAsJsonAsync(_route, request, ct);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetails = await response.Content.ReadFromJsonAsync<AidnProblemDetailsResponse>(ct);
        await Verify(problemDetails).ScrubMember("TraceId").ScrubMember("Instance");
    }

    [Fact]
    public async Task Create_WhenRespiratoryRateOutOfRange_ShouldReturnBadRequest()
    {
        // Arrange - Respiratory rate must be > 3 and <= 60
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = "HR", Value = 70 },
                new Measurement { Type = "TEMP", Value = 37 },
                new Measurement { Type = "RR", Value = 0 }, // Out of range (must be > 3)
            ],
        };
        var ct = TestContext.Current.CancellationToken;

        // Act
        var response = await _client.PostAsJsonAsync(_route, request, ct);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetails = await response.Content.ReadFromJsonAsync<AidnProblemDetailsResponse>(ct);
        await Verify(problemDetails).ScrubMember("TraceId").ScrubMember("Instance");
    }

    [Fact]
    public async Task Create_WhenMultipleValuesOutOfRange_ShouldReturnBadRequestWithAllErrors()
    {
        // Arrange
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = "HR", Value = 0 },
                new Measurement { Type = "TEMP", Value = 0 },
                new Measurement { Type = "RR", Value = 0 },
            ],
        };
        var ct = TestContext.Current.CancellationToken;

        // Act
        var response = await _client.PostAsJsonAsync(_route, request, ct);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problemDetails = await response.Content.ReadFromJsonAsync<AidnProblemDetailsResponse>(ct);
        await Verify(problemDetails).ScrubMember("TraceId").ScrubMember("Instance");
    }

    [Fact]
    public async Task Create_WithValidMeasurements_ShouldReturnOkWithScore0()
    {
        // Arrange - All normal values (score = 0)
        // HR: 50-90 = score 0, TEMP: 36-38 = score 0, RR: 11-20 = score 0
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = "HR", Value = 70 },
                new Measurement { Type = "TEMP", Value = 37 },
                new Measurement { Type = "RR", Value = 15 },
            ],
        };
        var ct = TestContext.Current.CancellationToken;

        // Act
        var response = await _client.PostAsJsonAsync(_route, request, ct);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<CreateNewsScoreResponse>(ct);
        Debug.WriteLine(result);

        // Hack: Newtonsoft.Json by default ignores properties with default values (e.g., 0 for int).
        await Verify(result).AddExtraSettings(x => x.DefaultValueHandling = DefaultValueHandling.Include);
    }

    [Fact]
    public async Task Create_WithMildlyAbnormalValues_ShouldReturnCorrectScore()
    {
        // Arrange
        // HR: 45 (score 1, range 40-50)
        // TEMP: 36 (score 1, range 35-36)
        // RR: 10 (score 1, range 8-11)
        // Total expected: 3
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = "HR", Value = 45 },
                new Measurement { Type = "TEMP", Value = 36 },
                new Measurement { Type = "RR", Value = 10 },
            ],
        };
        var ct = TestContext.Current.CancellationToken;

        // Act
        var response = await _client.PostAsJsonAsync(_route, request, ct);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<CreateNewsScoreResponse>(ct);
        await Verify(result);
    }

    [Fact]
    public async Task Create_WithModeratelyAbnormalValues_ShouldReturnCorrectScore()
    {
        // Arrange
        // HR: 120 (score 2, range 110-130)
        // TEMP: 40 (score 2, range 39-42)
        // RR: 22 (score 2, range 20-24)
        // Total expected: 6
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = "HR", Value = 120 },
                new Measurement { Type = "TEMP", Value = 40 },
                new Measurement { Type = "RR", Value = 22 },
            ],
        };
        var ct = TestContext.Current.CancellationToken;

        // Act
        var response = await _client.PostAsJsonAsync(_route, request, ct);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<CreateNewsScoreResponse>(ct);
        await Verify(result);
    }

    [Fact]
    public async Task Create_WithSeverelyAbnormalValues_ShouldReturnHighScore()
    {
        // Arrange
        // HR: 35 (score 3, range 25-40)
        // TEMP: 33 (score 3, range 31-35)
        // RR: 5 (score 3, range 3-8)
        // Total expected: 9
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = "HR", Value = 35 },
                new Measurement { Type = "TEMP", Value = 33 },
                new Measurement { Type = "RR", Value = 5 },
            ],
        };
        var ct = TestContext.Current.CancellationToken;

        // Act
        var response = await _client.PostAsJsonAsync(_route, request, ct);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<CreateNewsScoreResponse>(ct);
        await Verify(result);
    }

    [Fact]
    public async Task Create_WithBoundaryValues_ShouldReturnCorrectScore()
    {
        // Arrange - Test upper boundary values
        // HR: 220 (score 3, at boundary)
        // TEMP: 42 (score 2, at boundary)
        // RR: 60 (score 3, at boundary)
        // Total expected: 8
        var request = new CreateNewsScoreRequest
        {
            Measurements =
            [
                new Measurement { Type = "HR", Value = 220 },
                new Measurement { Type = "TEMP", Value = 42 },
                new Measurement { Type = "RR", Value = 60 },
            ],
        };
        var ct = TestContext.Current.CancellationToken;

        // Act
        var response = await _client.PostAsJsonAsync(_route, request, ct);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var result = await response.Content.ReadFromJsonAsync<CreateNewsScoreResponse>(ct);
        await Verify(result);
    }
}
