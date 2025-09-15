using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions()
{
    WebRootPath = "myroot"
});
var app = builder.Build();

app.UseStaticFiles(); // myroot

// ÅäÖÃ¾²Ì¬ÎÄ¼þÂ·¾¶
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "mywebroot"))
}); // mywebroot

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.Map("/", async (HttpContext contenxt) =>
    {
        await contenxt.Response.WriteAsync("Hello World!");
    });
});

app.Run();
