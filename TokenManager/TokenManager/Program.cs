using StackExchange.Redis;
using TokenManager;
using TokenManager.Abstracts;
using TokenManager.Concretes;

var builder = Host.CreateApplicationBuilder(args);

var authOptions = builder.Configuration.GetSection("AuthOptions").Get<KeycloakAuthOptions>()!;

builder.Services
    .AddSingleton(authOptions)
    .AddSingleton(ConnectionMultiplexer.Connect(builder.Configuration["Redis:Host"]!))
    .AddSingleton<IAccessTokenCache, RedisAccessTokenCache>()
    .AddSingleton<IAccessTokenProvider, KeycloakAccessTokenProvider>()
    .AddHostedService<Worker>();

var host = builder.Build();
host.Run();
