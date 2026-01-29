using Orleans.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseOrleans(
        siloBuilder =>
        {
            siloBuilder.UseLocalhostClustering(11112,30001);
            siloBuilder
                .UseMongoDBClient(builder.Configuration["MongoDbSettings:ConnectionString"])
                .AddMongoDBGrainStorage("PostStorage", options =>
                {
                    options.DatabaseName = builder.Configuration["MongoDbSettings:DatabaseName"];
                })
                .Configure<ExceptionSerializationOptions>(o => o.SupportedExceptionTypeFilter = _ => true);
        }
    );

var app = builder.Build();
app.Run();
