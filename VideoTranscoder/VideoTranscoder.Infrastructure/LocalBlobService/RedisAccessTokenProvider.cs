using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using VideoTranscoder.Application;

namespace VideoTranscoder.Infrastructure.LocalBlobService
{
    internal class RedisAccessTokenProvider : IAccessTokenProvider
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;
        private readonly IConfiguration _configuration;

        public RedisAccessTokenProvider(ConnectionMultiplexer connectionMultiplexer, IConfiguration configuration)
        {
            _connectionMultiplexer = connectionMultiplexer;
            _database = _connectionMultiplexer.GetDatabase();
            _configuration = configuration;
        }

        public string GetAccessToken() => _database.StringGet(_configuration["ClientId"]).ToString();
    }
}
