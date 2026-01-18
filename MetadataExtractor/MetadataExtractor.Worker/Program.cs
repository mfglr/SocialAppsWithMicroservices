using MetadataExtractor.Application;
using MetadataExtractor.Infrastructure;
using MetadataExtractor.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddMassTransit(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApplication(builder.Configuration);

var host = builder.Build();
host.Run();
