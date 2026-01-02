using PostService.Infrastructure;
using PostService.Workers.ServiceRegistrations;

var builder = Host.CreateApplicationBuilder(args);

DbConfiguration.Configure();

builder.Services
    .AddAutoMapper(builder.Configuration)
    .AddMassTransit(builder.Configuration)
    .AddUseCases()
    .AddInfrastructureServices(builder.Configuration);

var host = builder.Build();
host.Run();
