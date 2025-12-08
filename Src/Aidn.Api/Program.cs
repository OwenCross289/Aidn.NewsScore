using Aidn.Api.ProblemDetails;
using FastEndpoints;
using FastEndpoints.Swagger;
using Scalar.AspNetCore;

var bld = WebApplication.CreateBuilder(args);
bld.Services.AddFastEndpoints()
    .SwaggerDocument(o =>
    {
        o.ShortSchemaNames = true;
    });

var app = bld.Build();

app.UseProblemDetailsExceptionHandler();
app.UseFastEndpoints(x =>
{
    x.Endpoints.GlobalResponseModifier = ProblemDetailsExtensions.FailureAidnProblemDetailsResponseBuilder;
    x.Errors.ResponseBuilder = ProblemDetailsExtensions.ValidationFailureAidnProblemDetailsResponseBuilder;
});

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi(c => c.Path = "/openapi/{documentName}.json");
    app.MapScalarApiReference(o =>
    {
        o.Theme = ScalarTheme.Laserwave;
    });
}

app.Run();
