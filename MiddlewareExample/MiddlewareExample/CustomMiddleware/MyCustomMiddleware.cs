
namespace MiddlewareExample.CustomMiddleware
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("from MyCustomMiddleware start > ");

            await next(context);

            await context.Response.WriteAsync("from MyCustomMiddleware end > ");
        }
    }

    public static class CustomMiddleware
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
