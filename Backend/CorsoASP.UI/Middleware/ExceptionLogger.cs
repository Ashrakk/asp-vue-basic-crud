using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CorsoASP.UI.Middleware;

public class ExceptionLogger
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionLogger> _logger;

    public ExceptionLogger(RequestDelegate next, ILogger<ExceptionLogger> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                "Time: {time} Error Message: {exceptionMessage}, Stack Trace: {trace}",
                DateTime.UtcNow, exception.Message, exception.StackTrace);   
        }
    }

}