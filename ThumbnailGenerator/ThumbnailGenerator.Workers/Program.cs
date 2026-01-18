using ThumbnailGenerator.Application;
using ThumbnailGenerator.Infrastructure;
using ThumbnailGenerator.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddMassTransit(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var host = builder.Build();
host.Run();
