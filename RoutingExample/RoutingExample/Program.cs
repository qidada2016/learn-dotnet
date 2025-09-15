using RoutingExample.CustomConstraints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthsCustomConstraint));
});


var app = builder.Build();

//app.Use(async (context, next) =>
//{

//    Endpoint? endpoint = context.GetEndpoint();// 短路端点
//    await context.Response.WriteAsync($"Endpoint: {(endpoint == null ? null : endpoint.DisplayName)} \n");
//    await next(context);
//});

app.UseRouting();


// 总结: 短路端点必须在UseRouting()之后执行, 才有值, 否则为null
//app.Use(async (context, next) =>
//{

//    var endpoint = context.GetEndpoint(); // 短路端点
//    await context.Response.WriteAsync($"Endpoint: {endpoint?.DisplayName} \n");
//    await next(context);
//});

app.UseEndpoints(endpoints =>
{
    endpoints.Map("files/{filename}.{extension}", async (context) =>
    {

        var filename = Convert.ToString(context.Request.RouteValues["filename"]);
        var extension = Convert.ToString(context.Request.RouteValues["extension"]);

        await context.Response.WriteAsync($"request success!!! \nfilename - {filename} \nextension - {extension}");
    });

    // 路由参数: 默认值
    endpoints.Map("employee/profile/{employeename=tom}", async context =>
    {

        var employeeName = Convert.ToString(context.Request.RouteValues["employeename"]);

        await context.Response.WriteAsync($"employeename: {employeeName}");
    });

    // Eg: products/details/{id}
    endpoints.Map("products/details/{id?}", async context =>
    {

        var id = Convert.ToInt32(context.Request.RouteValues["id"]);
        await context.Response.WriteAsync($"products details id: {id}");
    });

    // Eg: 可选参数 {params?}


    // Eg: 参数类型约束 {params:{int, datetime}}
    endpoints.Map("daily-digest-report/{reportdate:datetime}", async context =>
    {

        var reportDate = Convert.ToDateTime(context.Request.RouteValues["reportdate"]);
        await context.Response.WriteAsync($"daily digest report: {reportDate.ToShortDateString()}");
    });

    // Eg: {cities:guid}
    endpoints.Map("cities/{id:guid}", async context =>
    {

        var cityId = Guid.Parse(Convert.ToString(context.Request.RouteValues["id"])!);
        await context.Response.WriteAsync($"cities id: {cityId}");

    });

    endpoints.Map("sales-report/{year:int:min(1900)}/{month:months}", async context =>
    {

        int year = Convert.ToInt32(context.Request.RouteValues["year"]);
        string? month = Convert.ToString(context.Request.RouteValues["month"]);
        await context.Response.WriteAsync($"sales report - {year} - {month}");

    });


});

// 默认中间件, 和Run并不相同, 因为这个是扩展方法, 方法签名并不一致, 所以功能是不一样的
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync($"No Route matched at {context.Request.Path}");
});

app.Run();
