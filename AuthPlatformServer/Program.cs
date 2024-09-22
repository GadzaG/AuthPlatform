using AuthPlatformServer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


builder.Services.AddDbContext<AuthPlatformDbContext>(options =>
{
    options.UseNpgsql(configuration.GetConnectionString(nameof(AuthPlatformDbContext)));
});
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<KeyService>();
app.MapGrpcService<ProductService>();
app.MapGrpcService<GlobalService>();
app.MapGrpcService<ApiService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
