var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); // DI
var app = builder.Build();

app.UseStaticFiles(); // 启用静态文件
app.UseRouting(); // 启用路由
app.MapControllers(); // 映射管理器

app.Run();
