using MiddlewareExample.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();


var app = builder.Build();


app.Use(async (context, next) =>
{
    await context.Response.WriteAsync("from middleware 1 \n");
    await next(context);
});

//app.UseMiddleware<MyCustomMiddleware>();
//app.UseMyCustomMiddleware();
app.UseHelloCustomMiddleware();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("from middleware 3 \n");
});

app.Run();
