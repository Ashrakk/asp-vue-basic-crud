using CorsoASP.UI.Middleware;

namespace CorsoASP.UI.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionLogger(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionLogger>();
    }
}