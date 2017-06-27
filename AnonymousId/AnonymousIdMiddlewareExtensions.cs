using Microsoft.AspNetCore.Builder;

namespace ReturnTrue.AspNetCore.Identity.Anonymous
{
    public static class AnonymousIdMiddlewareExtensions
    {
        public static IApplicationBuilder UseAnonymousId(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AnonymousIdMiddleware>(new AnonymousIdCookieOptionsBuilder().Build());
        }

        public static IApplicationBuilder UseAnonymousId(this IApplicationBuilder builder, AnonymousIdCookieOptionsBuilder options)
        {
            return builder.UseMiddleware<AnonymousIdMiddleware>(options.Build());
        }
    }
}