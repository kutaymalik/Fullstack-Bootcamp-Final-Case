using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

namespace OrderAutomationsProject.Api.Middlewares;

public interface ILoggerService
{
    public void Write(string message);
}

public class ConsoleLogger : ILoggerService
{
    public void Write(string message)
    {
        Console.WriteLine("[ConsoleLogger] - " + message);
    }
}

public class DBLogger : ILoggerService
{
    public void Write(string message)
    {
        Console.WriteLine("[DBLogger] - " + message);
    }
}

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILoggerService loggerService;

    public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
    {
        this.next = next;
        this.loggerService = loggerService;
    }

    public async Task Invoke(HttpContext context)
    {
        var watch = Stopwatch.StartNew();

        try
        {
            string message = "[Request]  HTTP " + context.Request.Method + " - " + context.Request.Path;
            loggerService.Write(message);

            await next(context);

            watch.Stop();

            message = "[Response] HTTP " + context.Request.Method + " - " +
                context.Request.Path +
                " responded " + context.Response.StatusCode +
                " in " + watch.Elapsed.TotalMilliseconds + "ms";

            loggerService.Write(message);

        }
        catch (Exception ex)
        {
            watch.Stop();
            await HandleException(context, ex, watch);
        }
    }

    private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        string message = "[Error]    HTTP " + context.Request.Method + " - " +
            context.Response.StatusCode +
            " Error Message: " + ex.Message +
            " in " + watch.Elapsed.TotalMilliseconds + " ms";

        loggerService.Write(message);

        var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

        return context.Response.WriteAsync(result);
    }
}

public static class CustomExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionMiddleware>();
    }
}
