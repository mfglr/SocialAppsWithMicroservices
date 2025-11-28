using PostService.Application;
using PostService.Infrastructure;
using PostService.Workers;

var builder = Host.CreateApplicationBuilder(args);

DbConfiguration.Configure();

builder.Services
    .AddAutoMapper(builder.Configuration)
    .AddMassTransit(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

var host = builder.Build();
host.Run();
