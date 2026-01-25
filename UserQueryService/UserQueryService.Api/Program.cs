using UserQueryService.Api.ServiceRegistrations;
using UserQueryService.Application;
using UserQueryService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services
    .AddAutoMapper(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Run();
