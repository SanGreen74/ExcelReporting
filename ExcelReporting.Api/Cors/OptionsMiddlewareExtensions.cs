using Microsoft.AspNetCore.Builder;

namespace ExcelReporting.Api.Cors;

public static class OptionsMiddlewareExtensions
{
    public static IApplicationBuilder UseOptions(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<OptionsMiddleware>();
    }
}