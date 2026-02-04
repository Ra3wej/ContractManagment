using System.Net;
using System.Text.Json;

namespace ContractManagment.Api.Middlewares;

public static class ExceptionHandlerMiddleware
{
    public static void ConfigureExceptionHandler(
        this IApplicationBuilder app,
        ILogger logger)
    {
        app.Use(async (context, next) =>
        {
            try
            {
                await next();
            }
            catch (Exception ex)
            {
                var traceId = context.TraceIdentifier;

                logger.LogError(
                    ex,
                    "Unhandled exception | TraceId: {TraceId} | Method: {Method} | Path: {Path}",
                    traceId,
                    context.Request.Method,
                    context.Request.Path
                );

                var response = new ApiErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "An unexpected error occurred. Please contact support.",
                    Path = context.Request.Path,
                    Method = context.Request.Method,
                    TraceId = traceId,
                    Timestamp = DateTime.UtcNow
                };

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = response.StatusCode;

                var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                await context.Response.WriteAsync(json);
            }
        });
    }
}
public sealed class ApiErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = null!;
    public string Path { get; set; } = null!;
    public string Method { get; set; } = null!;
    public string TraceId { get; set; } = null!;
    public DateTime Timestamp { get; set; }
}
