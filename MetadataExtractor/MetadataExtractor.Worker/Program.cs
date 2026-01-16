using MetadataExtractor.Application;
using MetadataExtractor.Infrastructure;
using MetadataExtractor.Worker;

var builder = Host.CreateApplicationBuilder(args);

FFmpegConfigration.Configure();

builder.Services
    .AddMassTransit(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();

var host = builder.Build();
host.Run();
