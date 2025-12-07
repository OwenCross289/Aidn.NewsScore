using Aidn.Constants;
using FluentValidation.Results;
using Microsoft.AspNetCore.Diagnostics;

namespace Aidn.Api.ProblemDetails;

public static class ProblemDetailsExtensions
{
    public static IApplicationBuilder UseProblemDetailsExceptionHandler(this IApplicationBuilder app, bool useGenericReason = false)
    {
        app.UseExceptionHandler(errApp =>
        {
            errApp.Run(async ctx =>
            {
                var exHandlerFeature = ctx.Features.Get<IExceptionHandlerFeature>();
                if (exHandlerFeature is not null)
                {
                    var exceptionType = exHandlerFeature.Error.GetType().Name;
                    var reason = exHandlerFeature.Error.Message;
                    ctx.Response.StatusCode = 500;
                    ctx.Response.ContentType = MimeTypeConstants.ProblemJson;

                    var (title, detail, type) = GetRfcStatusCodeInfo(ctx.Response.StatusCode);
                    await ctx.Response.WriteAsJsonAsync(
                        new AidnProblemDetailsResponse
                        {
                            Type = type,
                            Title = title,
                            Status = ctx.Response.StatusCode,
                            Instance = ctx.Request.Path,
                            TraceId = ctx.TraceIdentifier,
                            Detail = detail,
                            Errors =
                            [
                                new AidnError
                                {
                                    Name = exceptionType,
                                    Reason = useGenericReason ? "An unexpected error has occurred." : reason,
                                    Code = "UNHANDLED_EXCEPTION_OCCURRED", // Move to a common error code constant
                                    Severity = "Error",
                                },
                            ],
                        }
                    );
                }
            });
        });

        return app;
    }

    public static void FailureAidnProblemDetailsResponseBuilder(HttpContext context, object? obj)
    {
        //handle everything except for 400 and 500 as they require some extra logic.
        //400 is handled in this file by ValidationFailureProblemDetailsBuilder
        //500 is handled in the DefaultExceptionHandlerExtensions
        if (context.Response is { StatusCode: > 400 and not 500 })
        {
            context.Response.ContentType = MimeTypeConstants.ProblemJson;
            var problemDetails = CreateProblemDetails(context.Response.StatusCode, context.Request.Path, context.TraceIdentifier);
            context.Response.WriteAsJsonAsync(problemDetails);
        }
    }

    public static AidnProblemDetailsResponse ValidationFailureAidnProblemDetailsResponseBuilder(
        List<ValidationFailure> errors,
        HttpContext context,
        int statusCode
    )
    {
        var (title, detail, type) = GetRfcStatusCodeInfo(statusCode);
        return new AidnProblemDetailsResponse()
        {
            Type = type,
            Title = title,
            Status = context.Response.StatusCode,
            Instance = context.Request.Path,
            TraceId = context.TraceIdentifier,
            Detail = detail,
            Errors = errors
                .Select(vf => new AidnError
                {
                    Name = vf.PropertyName ?? "Unknown",
                    Reason = vf.ErrorMessage ?? "Unknown error",
                    Code = vf.ErrorCode ?? ErrorCodeConstants.BadRequest,
                    Severity = vf.Severity.ToString(),
                })
                .ToArray(),
        };
    }

    private static AidnProblemDetailsResponse CreateProblemDetails(int statusCode, string path, string traceId)
    {
        var (title, detail, type) = GetRfcStatusCodeInfo(statusCode);

        return new AidnProblemDetailsResponse
        {
            Type = type,
            Title = title,
            Status = statusCode,
            Instance = path,
            TraceId = traceId,
            Detail = detail,
            Errors = [],
        };
    }

    public static (string Title, string Detail, string Type) GetRfcStatusCodeInfo(int statusCode)
    {
        return statusCode switch
        {
            400 => ("Bad Request", "A validation failure has occurred.", "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.1"),
            401 => (
                "Unauthorized",
                "Authentication is required and has failed or not been provided.",
                "https://www.rfc-editor.org/rfc/rfc7235#section-3.1"
            ),
            402 => ("Payment Required", "Reserved for future use.", "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.2"),
            403 => (
                "Forbidden",
                "The server understood the request but refuses to authorize it.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.3"
            ),
            404 => ("Not Found", "The requested resource could not be found.", "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.4"),
            405 => (
                "Method Not Allowed",
                "The request method is not supported for the requested resource.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.5"
            ),
            406 => (
                "Not Acceptable",
                "The requested resource is capable of generating only content not acceptable according to the Accept headers sent.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.6"
            ),
            407 => (
                "Proxy Authentication Required",
                "Authentication with the proxy is required.",
                "https://www.rfc-editor.org/rfc/rfc7235#section-3.2"
            ),
            408 => ("Request Timeout", "The server timed out waiting for the request.", "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.7"),
            409 => (
                "Conflict",
                "The request could not be completed due to a conflict with the current state of the resource.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.8"
            ),
            410 => (
                "Gone",
                "The requested resource is no longer available and will not be available again.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.9"
            ),
            411 => (
                "Length Required",
                "The request did not specify the length of its content.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.10"
            ),
            412 => (
                "Precondition Failed",
                "The server does not meet one of the preconditions specified in the request.",
                "https://www.rfc-editor.org/rfc/rfc7232#section-4.2"
            ),
            413 => (
                "Payload Too Large",
                "The request is larger than the server is willing or able to process.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.11"
            ),
            414 => (
                "URI Too Long",
                "The URI provided was too long for the server to process.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.12"
            ),
            415 => (
                "Unsupported Media Type",
                "The media format of the requested data is not supported.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.13"
            ),
            416 => (
                "Range Not Satisfiable",
                "The range specified in the Range header cannot be fulfilled.",
                "https://www.rfc-editor.org/rfc/rfc7233#section-4.4"
            ),
            417 => (
                "Expectation Failed",
                "The expectation given in the Expect header could not be met.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.14"
            ),
            418 => (
                "I'm a teapot",
                "The server refuses to brew coffee because it is a teapot.",
                "https://datatracker.ietf.org/doc/html/rfc2324#section-2.3.2"
            ),
            421 => (
                "Misdirected Request",
                "The request was directed at a server that is not able to produce a response.",
                "https://www.rfc-editor.org/rfc/rfc7540#section-9.1.2"
            ),
            422 => (
                "Unprocessable Entity",
                "The request was well-formed but was unable to be followed due to semantic errors.",
                "https://www.rfc-editor.org/rfc/rfc4918#section-11.2"
            ),
            423 => ("Locked", "The resource that is being accessed is locked.", "https://www.rfc-editor.org/rfc/rfc4918#section-11.3"),
            424 => (
                "Failed Dependency",
                "The request failed due to failure of a previous request.",
                "https://www.rfc-editor.org/rfc/rfc4918#section-11.4"
            ),
            425 => (
                "Too Early",
                "The server is unwilling to risk processing a request that might be replayed.",
                "https://www.rfc-editor.org/rfc/rfc8470#section-5.2"
            ),
            426 => ("Upgrade Required", "The client should switch to a different protocol.", "https://www.rfc-editor.org/rfc/rfc7231#section-6.5.15"),
            428 => (
                "Precondition Required",
                "The origin server requires the request to be conditional.",
                "https://www.rfc-editor.org/rfc/rfc6585#section-3"
            ),
            429 => (
                "Too Many Requests",
                "The user has sent too many requests in a given amount of time.",
                "https://www.rfc-editor.org/rfc/rfc6585#section-4"
            ),
            431 => (
                "Request Header Fields Too Large",
                "The server is unwilling to process the request because its header fields are too large.",
                "https://www.rfc-editor.org/rfc/rfc6585#section-5"
            ),
            451 => (
                "Unavailable For Legal Reasons",
                "The requested resource is unavailable due to legal reasons.",
                "https://www.rfc-editor.org/rfc/rfc7725#section-3"
            ),
            500 => (
                "Internal Server Error",
                "The server encountered an unexpected condition that prevented it from fulfilling the request.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.1"
            ),
            501 => (
                "Not Implemented",
                "The server does not support the functionality required to fulfill the request.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.2"
            ),
            502 => (
                "Bad Gateway",
                "The server received an invalid response from an upstream server.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.3"
            ),
            503 => (
                "Service Unavailable",
                "The server is currently unable to handle the request due to temporary overloading or maintenance.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.4"
            ),
            504 => (
                "Gateway Timeout",
                "The server did not receive a timely response from an upstream server.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.5"
            ),
            505 => (
                "HTTP Version Not Supported",
                "The server does not support the HTTP protocol version used in the request.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.6.6"
            ),
            506 => (
                "Variant Also Negotiates",
                "The server has an internal configuration error: the chosen variant resource is configured to engage in transparent content negotiation itself.",
                "https://www.rfc-editor.org/rfc/rfc2295#section-8.1"
            ),
            507 => (
                "Insufficient Storage",
                "The server is unable to store the representation needed to complete the request.",
                "https://www.rfc-editor.org/rfc/rfc4918#section-11.5"
            ),
            508 => (
                "Loop Detected",
                "The server detected an infinite loop while processing the request.",
                "https://www.rfc-editor.org/rfc/rfc5842#section-7.2"
            ),
            510 => (
                "Not Extended",
                "Further extensions to the request are required for the server to fulfill it.",
                "https://www.rfc-editor.org/rfc/rfc2774#section-7"
            ),
            511 => (
                "Network Authentication Required",
                "The client needs to authenticate to gain network access.",
                "https://www.rfc-editor.org/rfc/rfc6585#section-6"
            ),
            >= 400 and < 500 => (
                $"Client Error {statusCode}",
                $"An unexpected client error occurred with status code {statusCode}.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.5"
            ),
            >= 500 and < 600 => (
                $"Server Error {statusCode}",
                $"An unexpected server error occurred with status code {statusCode}.",
                "https://www.rfc-editor.org/rfc/rfc7231#section-6.6"
            ),
            _ => ($"Unknown Status {statusCode}", $"An unknown status code {statusCode} was returned.", "https://www.rfc-editor.org/rfc/rfc7231"),
        };
    }
}
