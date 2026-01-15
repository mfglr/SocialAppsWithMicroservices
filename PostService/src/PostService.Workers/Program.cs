using PostService.Application;
using PostService.Infrastructure;
using PostService.Workers;
using PostService.Workers.ServiceRegistrations;

var builder = Host.CreateApplicationBuilder(args);

DbConfiguration.Configure();

builder.Services
    .AddApplication(builder.Configuration)
    .AddMassTransit(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddSingleton<IIdentityService,NullIdentityService>();

var host = builder.Build();
host.Run();
