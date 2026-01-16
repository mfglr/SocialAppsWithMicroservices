using PostService.Api.ServiceRegistrations;
using PostService.Application;
using PostService.Infrastructure;
using PostService.Infrastructure.MongoDb;

var builder = WebApplication.CreateBuilder(args);

DbConfiguration.Configure();

builder.Services.AddControllers();
builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddIdentity(builder.Configuration)
    .AddMassTransit(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
