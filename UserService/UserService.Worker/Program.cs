using MassTransit;
using UserService.Worker.ServiceRegistrations;
using UserService.Infrastructure;
using UserService.Application;
using UserService.Worker;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddSingleton<IIdentityService, WorkerIdentityService>()
    .AddMassTransit(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var host = builder.Build();
host.Run();
