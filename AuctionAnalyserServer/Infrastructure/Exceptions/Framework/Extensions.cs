using Microsoft.AspNetCore.Builder;

namespace AuctionAnalyserServer.Infrastructure.Exceptions.Framework
{
    public static class Extensions
    {
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder builder) => builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}