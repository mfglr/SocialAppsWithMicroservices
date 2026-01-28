using Orleans;

namespace UserService.Domain
{
    public interface IUserGrain : IGrainWithGuidKey
    {
        Task<User> Get();
        Task Create(Username username);
        Task AddMedia(Media media);
        Task UpdateName(Name name);
        Task SetMediaMatadata(string blobName, Metadata metadata);
        Task SetMediaModerationResult(string blobName, ModerationResult moderationResult);
        Task AddMediaThumbnail(string blobName, Thumbnail thumbnail);
    }
}
