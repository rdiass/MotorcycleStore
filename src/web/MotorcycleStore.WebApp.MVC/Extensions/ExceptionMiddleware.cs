using Polly.CircuitBreaker;
using System.Net;

namespace MotorcycleStore.WebApp.MVC.Extensions;

public class ExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (CustomHttpRequestException ex)
        {
            HandleExceptionAsync(httpContext, ex.StatusCode);
        }
        catch(BrokenCircuitException)
        {
            HandleExceptionAsync(httpContext);
        }
    }

    private static void HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode)
    {
        if (statusCode == HttpStatusCode.Unauthorized)
        {
            context.Response.Redirect($"/login?ReturnUrl={context.Request.Path}");
            return;
        }

        context.Response.StatusCode = (int)statusCode;
    }

    private static void HandleExceptionAsync(HttpContext context)
    {
        context.Response.Redirect("/system-unavailable");
    }
}
