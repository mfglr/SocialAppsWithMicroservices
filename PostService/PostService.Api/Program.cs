using PostService.Infrastructure;
using PostService.Application;
using PostService.Api;

var builder = WebApplication.CreateBuilder(args);

DbConfiguration.Configure();

builder.Services.AddControllers();
builder.Services
    .AddAutoMapper(builder.Configuration)
    .AddMassTransit(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();
app.UseAuthorization();
app.MapControllers();

app.Run();
