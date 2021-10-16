using GameStore.Foundation.ExceptionHandling;
using Microsoft.AspNetCore.Builder;

namespace GameStore.Foundation
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}