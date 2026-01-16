using ThumbnailGenerator.Application;
using ThumbnailGenerator.Infrastructure;
using ThumbnailGenerator.Workers;

var builder = Host.CreateApplicationBuilder(args);

FFmpegConfigration.Configure();

builder.Services
    .AddMassTransit(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

var host = builder.Build();
host.Run();
