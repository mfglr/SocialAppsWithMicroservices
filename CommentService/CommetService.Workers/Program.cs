using CommentService.Application;
using CommentService.Infrastructure;
using CommetService.Workers;

var builder = Host.CreateApplicationBuilder(args);

DbConfigurator.Configure();

builder.Services
    .AddAutoMapper(builder.Configuration)
    .AddMassTransit(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices();

var host = builder.Build();
host.Run();
