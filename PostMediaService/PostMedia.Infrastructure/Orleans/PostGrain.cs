using Orleans.Providers;
using PostMedia.Domain;

namespace PostMedia.Infrastructure.Orleans
{
    [StorageProvider(ProviderName = "PostStorage")]
    internal class PostGrain : Grain<Post>, IPostGrain
    {
        public Task<Post> Get() => Task.FromResult(State);
        public async Task<Post> Create(IEnumerable<Media> media)
        {
            State = new Post(this.GetPrimaryKey(), media);
            await WriteStateAsync();
            return State;
        }
        public async Task Delete()
        {
            await ClearStateAsync();
            DeactivateOnIdle();
        }
        public async Task<Post> AddThumbnail(string blobName, Thumbnail thumbnail)
        {
            State.AddThumbnail(blobName, thumbnail);
            await WriteStateAsync();
            return State;
        }
        public async Task<Post> SetMetadata(string blobName, Metadata metadata)
        {
            State.SetMetadata(blobName, metadata);
            await WriteStateAsync();
            return State;
        }
        public async Task<Post> SetModerationResult(string blobName, ModerationResult moderationResult)
        {
            State.SetModerationResult(blobName, moderationResult);
            await WriteStateAsync();
            return State;
        }
        public async Task<Post> SetTranscodedBlobName(string blobName, string transcodedBlobName)
        {
            State.SetTranscodedBlobName(blobName, transcodedBlobName);
            await WriteStateAsync();
            return State;
        }
    }
}
