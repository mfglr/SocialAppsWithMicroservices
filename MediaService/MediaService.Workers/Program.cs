using MediaService.Application;
using MediaService.Infrastructure;
using MediaService.Workers;

var builder = Host.CreateApplicationBuilder(args);
DbConfiguration.Configure();

builder.Services
    .AddAutoMapper(builder.Configuration)
    .AddMasstransit(builder.Configuration)
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices();

var host = builder.Build();
host.Run();
