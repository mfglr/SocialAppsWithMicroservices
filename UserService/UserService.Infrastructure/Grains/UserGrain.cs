using Orleans;
using Orleans.Providers;
using UserService.Domain;

namespace UserService.Infrastructure.Grains
{
    [StorageProvider(ProviderName = "UserStorage")]
    internal class UserGrain : Grain<User>, IUserGrain
    {
        public Task<User> Get() => Task.FromResult(State);

        public Task Create(Username username)
        {
            State = new User(this.GetPrimaryKey(), username);
            return WriteStateAsync();
        }

        public Task AddMedia(Media media)
        {
            if (State == default)
                throw new UserNotFoundException();
            State.AddMedia(media);
            return WriteStateAsync();
        }

        public Task UpdateName(Name name)
        {
            if (State == default)
                throw new UserNotFoundException();
            State.UpdateName(name);
            return WriteStateAsync();
        }

        public Task SetMediaMatadata(string blobName, Metadata metadata)
        {
            if (State == default)
                throw new UserNotFoundException();
            State.SetMediaMetadata(blobName, metadata);
            return WriteStateAsync();
        }

        public Task SetMediaModerationResult(string blobName, ModerationResult moderationResult)
        {
            if (State == default)
                throw new UserNotFoundException();
            State.SetMediaModerationResult(blobName, moderationResult);
            return WriteStateAsync();
        }

        public Task AddMediaThumbnail(string blobName, Thumbnail thumbnail)
        {
            if (State == default)
                throw new UserNotFoundException();
            State.AddMediaThumbnail(blobName, thumbnail);
            return WriteStateAsync();
        }
    }
}
