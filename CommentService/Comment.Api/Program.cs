using Comment.Api;
using CommentService.Application;
using CommentService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

DbConfigurator.Configure();

builder.Services.AddControllers();

builder.Services
    .AddAutoMapper(builder.Configuration)
    .AddMassTransit(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices();

var app = builder.Build();
app.UseAuthorization();
app.MapControllers();

app.Run();
