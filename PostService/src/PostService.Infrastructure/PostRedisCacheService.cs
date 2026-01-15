using Microsoft.Extensions.Configuration;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using PostService.Application;
using PostService.Domain;
using StackExchange.Redis;

namespace PostService.Infrastructure
{
    public class PostRedisCacheService : IPostCacheService
    {
        private readonly ConnectionMultiplexer _muxer;
        private readonly IDatabase _database;

        private static string GetId(Guid id) => $"post_{id}";
        private static string GetVersionId(Guid id) => $"post_version_{id}";

        public PostRedisCacheService(ConnectionMultiplexer muxer)
        {
            _muxer = muxer;
            _database = _muxer.GetDatabase();
        }

        public async Task CreateAsync(Post post)
        {
            var trans = new Transaction(_database);
            _ = trans.Db.StringSetAsync(GetVersionId(post.Id), post.Version);
            _ = trans.Json.SetAsync(GetId(post.Id), "$", post);
            var result = await trans.ExecuteAsync();

            if (!result)
                throw new Exception("Post not able to cached");
        }

        public async Task DeleteAsync(Post post)
        {
            var trans = new Transaction(_database);
            _ = trans.Db.StringGetDeleteAsync(GetVersionId(post.Id));
            _ = trans.Json.DelAsync(GetId(post.Id));
            await trans.ExecuteAsync();
        }

        public Task<Post?> GetByIdAsync(Guid id)
            => _database.JSON().GetAsync<Post>(GetId(id));

        public async Task UpdateAsync(Post post)
        {
            var trans = new Transaction(_database);
            trans.AddCondition(Condition.StringEqual(GetVersionId(post.Id), post.Version - 1));
            _ = trans.Json.SetAsync(GetId(post.Id), "$", post);
            _ = trans.Db.StringSetAsync(GetVersionId(post.Id), post.Version);
            var result = await trans.ExecuteAsync();

            if (!result)
                throw new Exception("Concurrency");
        }
    }
}
