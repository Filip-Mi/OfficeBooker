using Microsoft.AspNetCore.Diagnostics;
using OfficeBooker.Models.Exceptions;
using OfficeBooker.Models.Exceptions.Base;
using System.Diagnostics;
namespace OfficeBooker.Middleware
{
    /// <summary>
    /// Global exception handler middleware for the ASP.NET Core application.
    /// </summary>
    /// <remarks>
    /// This middleware intercepts all unhandled exceptions in the application
    /// and returns a formatted JSON response with error information.
    /// </remarks>
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            // Retrieve or generate a unique trace ID for error tracking
            var traceId = Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString();

            // Special handling for FluentValidation exceptions to return detailed validation errors
            if (exception is  FluentValidation.ValidationException validation)
            {
                var valResponse = new ErrorResponse
                {
                    Message = "Validation failed for the request.",
                    ErrorCode = "VALIDATION_ERROR",
                    StatusCode = StatusCodes.Status400BadRequest,
                    Path = httpContext.Request.Path,
                    Timestamp = DateTime.UtcNow,
                    TraceId = traceId,
                    Details = validation.Message,
                    ValidationErrors = validation.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToList()
                        )
                };
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(valResponse, cancellationToken);
                return true;
            }
            // Log the exception details for diagnostics
            _logger.LogError(exception, "An unhandled exception occurred. TraceId: {TraceId}", traceId);

            // Map the exception to an HTTP status code and error code
            var (statusCode, errorCode, message) = MapExceptionToResponse(exception);

            // Create the error response object
            var response = new ErrorResponse
            {
                Message = message,
                ErrorCode = errorCode,
                StatusCode = statusCode,
                Path = httpContext.Request.Path,
                Timestamp = DateTime.UtcNow,
                TraceId = traceId,
                Details = exception.Message
            };

            // Set the HTTP status code and send the JSON response
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
            return true;
        }

        /// <summary>
        /// Maps an exception type to an HTTP status code, error code, and user-friendly message.
        /// </summary>
        /// <param name="exception">The exception to be mapped.</param>
        /// <returns>
        /// A tuple containing the HTTP status code, error code, and error message.
        /// </returns>
        /// <remarks>
        /// This method handles the following exception types:
        /// <list type="bullet">
        /// <item>
        /// <description><see cref="KeyNotFoundException"/> → 404 Not Found</description>
        /// </item>
        /// <item>
        /// <description><see cref="UnauthorizedAccessException"/> → 401 Unauthorized</description>
        /// </item>
        /// <item>
        /// <description><see cref="ArgumentException"/> → 400 Bad Request</description>
        /// </item>
        /// <item>
        /// <description><see cref="InvalidOperationException"/> → 409 Conflict</description>
        /// </item>
        /// <item>
        /// <description>All other exceptions → 500 Internal Server Error</description>
        /// </item>
        /// </list>
        /// </remarks>
            private static (int StatusCode, string ErrorCode, string Message) MapExceptionToResponse(Exception exception)
        {
            return exception switch
            {
                BaseDomainException domainEx => (
                    domainEx.StatusCode,
                    domainEx.GetType().Name.Replace("Exception", "").ToUpper(),
    domainEx.Message
                ),
                KeyNotFoundException => (
                    StatusCodes.Status404NotFound,
                    "NOT_FOUND",
                    "The requested resource was not found."
                ),

                UnauthorizedAccessException => (
                    StatusCodes.Status401Unauthorized,
                    "UNAUTHORIZED",
                    "You are not authorized to access this resource."
                ),

                _ => (
                    StatusCodes.Status500InternalServerError,
                    "INTERNAL_SERVER_ERROR",
                    "An unexpected error occurred. Please try again later."
                )
            };
        } 
    }
}

