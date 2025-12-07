namespace Aidn.Api.Endpoints;

public static class Routes
{
    private const string _apiBase = "api";

    public static class NewsScores
    {
        private const string _base = $"{_apiBase}/news-scores";
        public const string Create = _base;
    }
}
