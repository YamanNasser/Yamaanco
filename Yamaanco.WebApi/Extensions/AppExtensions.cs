using Microsoft.AspNetCore.Builder;
using Yamaanco.WebApi.Middlewares;

namespace Yamaanco.WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}