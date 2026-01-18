using VideoTranscoder.Api;
using VideoTranscoder.Application;
using VideoTranscoder.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddMassTransit(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();
