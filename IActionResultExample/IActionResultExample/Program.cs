var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); // DI
var app = builder.Build();

app.UseStaticFiles(); // ���þ�̬�ļ�
app.UseRouting(); // ����·��
app.MapControllers(); // ӳ�������

app.Run();
