namespace Aidn.Api.ProblemDetails;

/// <summary>
/// Represents a problem details for the Opus Web API.
/// </summary>
public record AidnProblemDetailsResponse
{
    /// <summary>
    /// Type of the problem, a URI in the RFC 7807 spec that identifies the problem type.
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Title of the problem, providing a short, human-readable summary of the problem.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// HTTP status code associated with the problem.
    /// </summary>
    public required int Status { get; set; }

    /// <summary>
    /// Instance URI that identifies the specific occurrence of the problem.
    /// </summary>
    public required string Instance { get; set; }

    /// <summary>
    /// Trace identifier for the request, useful for debugging and tracking issues.
    /// </summary>
    public required string TraceId { get; set; }

    /// <summary>
    /// Detailed description of the problem.
    /// </summary>
    public required string Detail { get; set; }

    /// <summary>
    /// Ccollection of errors associated with the problem.
    /// </summary>
    public required AidnError[] Errors { get; set; }
}

/// <summary>
/// Represents the details of an individual error in a problem details.
/// </summary>
public record AidnError
{
    /// <summary>
    /// Name of the error.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Reason for the error.
    /// </summary>
    public required string Reason { get; set; }

    /// <summary>
    /// Error code associated with the error. e.g. "UNHANDLED_EXCEPTION_OCCURRED".
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    /// Severity level of the error.
    /// </summary>
    public required string Severity { get; set; }
}
