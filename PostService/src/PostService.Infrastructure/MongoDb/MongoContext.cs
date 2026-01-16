using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PostService.Domain;

namespace PostService.Infrastructure.MongoDb
{
    internal class MongoContext
    {
        public IMongoClient Client { get; set; }
        public IMongoCollection<Post> Posts { get; set; }

        public MongoContext(IConfiguration configuration)
        {
            Client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]!);
            var database = Client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]!);
            Posts = database.GetCollection<Post>("posts");

            //var indexModel = new CreateIndexModel<Post>(Builders<Post>.IndexKeys.Ascending(m => m.UserId));
            //Posts.Indexes.CreateOne(indexModel);
        }

    }
}
