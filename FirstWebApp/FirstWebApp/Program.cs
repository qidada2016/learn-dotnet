using Microsoft.AspNetCore.WebUtilities;
using System.IO;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.Use(async (HttpContext context, RequestDelegate next) =>
{

});

app.Run(async (HttpContext context) =>
{
    var sr = new StreamReader(context.Request.Body);
    var body = await sr.ReadToEndAsync();

    var queryDict = QueryHelpers.ParseQuery(body);

    if (queryDict.ContainsKey("firstName"))
    {
        var firstName = queryDict["firstName"][0];
        await context.Response.WriteAsync(firstName);
    }

});

app.Run();
