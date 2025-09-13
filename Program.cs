var builder = WebApplication.CreateBuilder(args);
//add services 
builder.Services.AddControllers();
builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.Run();
