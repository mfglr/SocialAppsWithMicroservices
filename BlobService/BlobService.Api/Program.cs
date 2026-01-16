using BlobService.Api;
using BlobService.Api.Abstracts;
using BlobService.Api.Concretes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .AddAuthenticationAndAuthorization(builder.Configuration)
    .AddSingleton<PathFinder>()
    .AddSingleton<UniqNameGenerator>()
    .AddSingleton<IContainerService, LocalContainerService>()
    .AddSingleton<IBlobService, LocalBlobService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
