using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using ThumbnailGenerator.Application;

namespace ThumbnailGenerator.Infrastructure
{
    internal class RedisAccessTokenProvider : IAccessTokenProvider
    {
        private readonly ConnectionMultiplexer _connecitonMultiplexer;
        private readonly IDatabase _database;
        private readonly IConfiguration _configuration;

        public RedisAccessTokenProvider(ConnectionMultiplexer connecitonMultiplexer, IConfiguration configuration)
        {
            _connecitonMultiplexer = connecitonMultiplexer;
            _database = _connecitonMultiplexer.GetDatabase();
            _configuration = configuration;
        }

        public string GetAccessToken() => _database.StringGet(_configuration["ClientId"]).ToString();
    }
}
