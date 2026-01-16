using CommentService.Application;
using CommentService.Domain;
using CommentService.Infrastructure;
using CommetService.Workers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services
    .AddDomain()
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddMassTransit(builder.Configuration)
    .AddScoped<IIdentityService, WorkerIdentiyService>();

DbConfigurator.Configure(builder.Services);

var host = builder.Build();
host.Run();
