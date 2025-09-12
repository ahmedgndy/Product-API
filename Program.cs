var builder = WebApplication.CreateBuilder(args);
//add services 
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();
