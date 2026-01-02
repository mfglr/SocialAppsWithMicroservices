using PostService.Infrastructure;
using PostService.Api.ServiceRegistrations;

var builder = WebApplication.CreateBuilder(args);

DbConfiguration.Configure();

builder.Services.AddControllers();
builder.Services
    .AddUseCases()
    .AddInfrastructureServices(builder.Configuration)
    .AddIdentity(builder.Configuration)
    .AddAutoMapper(builder.Configuration)
    .AddMassTransit(builder.Configuration);

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
