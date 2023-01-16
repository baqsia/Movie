using System.Net.Mime;
using System.Text.Json;

namespace Movie.Api.Infrastructure;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {  
            context.Response.Clear();
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = MediaTypeNames.Application.Json;

            var errorResponse = new
            {
                message = "server error, will be fixed soon :("
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}