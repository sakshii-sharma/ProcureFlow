using Microsoft.AspNetCore.Diagnostics;
using ProcureFlow.API.Common.Responses;
using ProcureFlow.Application.Common.Exceptions;

namespace ProcureFlow.API.Common.Exceptions;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "An unhandled exception occurred.");

        var response = exception switch
        {
            NotFoundException ex => CreateResponse(StatusCodes.Status404NotFound, ex.Code, ex.Message),

            ConflictException ex => CreateResponse(StatusCodes.Status409Conflict, ex.Code, ex.Message),

            _ => CreateResponse(StatusCodes.Status500InternalServerError, "INTERNAL_SERVER_ERROR", "An unexpected error occurred.")
        };

        httpContext.Response.StatusCode = response.StatusCode;

        await httpContext.Response.WriteAsJsonAsync( response.Body, cancellationToken);

        return true;
    }

    private static ExceptionResponse CreateResponse(int statusCode, string code, string message)
    {
        var body = ApiResponse<object>.ErrorResponse( message,
            new List<ApiError>
            {
                new()
                {
                    Code = code,
                    Message = message
                }
            }
        );

        return new ExceptionResponse(statusCode, body);
    }

    private sealed record ExceptionResponse( int StatusCode, ApiResponse<object> Body );
}