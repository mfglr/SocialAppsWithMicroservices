using VideoTranscoder.Application;
using VideoTranscoder.Infrastructure;
using VideoTranscoder.Worker;

var builder = Host.CreateApplicationBuilder(args);

FFmpegConfigration.Configure();

builder.Services
    .AddMassTransit(builder.Configuration)
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

var host = builder.Build();
host.Run();
