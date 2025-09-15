using RoutingExample.CustomConstraints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("months", typeof(MonthsCustomConstraint));
});


var app = builder.Build();

//app.Use(async (context, next) =>
//{

//    Endpoint? endpoint = context.GetEndpoint();// ��·�˵�
//    await context.Response.WriteAsync($"Endpoint: {(endpoint == null ? null : endpoint.DisplayName)} \n");
//    await next(context);
//});

app.UseRouting();


// �ܽ�: ��·�˵������UseRouting()֮��ִ��, ����ֵ, ����Ϊnull
//app.Use(async (context, next) =>
//{

//    var endpoint = context.GetEndpoint(); // ��·�˵�
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

    // ·�ɲ���: Ĭ��ֵ
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

    // Eg: ��ѡ���� {params?}


    // Eg: ��������Լ�� {params:{int, datetime}}
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

// Ĭ���м��, ��Run������ͬ, ��Ϊ�������չ����, ����ǩ������һ��, ���Թ����ǲ�һ����
app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync($"No Route matched at {context.Request.Path}");
});

app.Run();
