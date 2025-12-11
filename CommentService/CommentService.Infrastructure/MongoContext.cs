using CommentService.Domain;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace CommentService.Infrastructure
{
    internal class MongoContext
    {
        public IMongoClient Client { get; set; }
        public IMongoCollection<Comment> Comments { get; set; }

        public MongoContext(IConfiguration configuration)
        {
            Client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]!);
            var database = Client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]!);
            Comments = database.GetCollection<Comment>("comments");
        }

    }
}
