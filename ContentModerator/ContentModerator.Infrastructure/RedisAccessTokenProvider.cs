using ContentModerator.Application;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace ContentModerator.Infrastructure
{
    internal class RedisAccessTokenProvider(ConnectionMultiplexer connectionMultiplexer, IConfiguration configuration) : IAccessTokenProvider
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IDatabase _database = connectionMultiplexer.GetDatabase();

        public string GetAccessToken() => _database.StringGet(_configuration["ClientId"]).ToString();
    }
}
