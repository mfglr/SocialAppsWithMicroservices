using PostMedia.Application;
using PostMedia.Infrastructure;
using PostMedia.Worker.ServiceRegistrations;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddAutoMapper(builder.Configuration)
    .AddMassTransit(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrastructure();

var host = builder.Build();
host.Run();
