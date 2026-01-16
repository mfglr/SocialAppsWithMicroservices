using StackExchange.Redis;
using TokenManager.Abstracts;

namespace TokenManager.Concretes
{
    public class RedisAccessTokenCache : IAccessTokenCache
    {
        private readonly ConnectionMultiplexer _muxer;
        private readonly IDatabase _database;

        public RedisAccessTokenCache(ConnectionMultiplexer muxer)
        {
            _muxer = muxer;
            _database = _muxer.GetDatabase();
        }

        public void Set(string clientId, string accessToken) => _database.StringSet(clientId, accessToken);
    }
}
