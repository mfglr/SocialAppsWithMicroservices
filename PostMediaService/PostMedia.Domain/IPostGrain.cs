namespace PostMedia.Domain
{
    [Alias("PostMedia.Domain.IPostGrain")]
    public interface IPostGrain : IGrainWithGuidKey
    {
        [Alias("Get")]
        Task<Post> Get();
        [Alias("Create")]
        Task<Post> Create(IEnumerable<Media> media);
        [Alias("SetMetadata")]
        Task<Post> SetMetadata(string blobName, Metadata metadata);
        [Alias("SetModerationResult")]
        Task<Post> SetModerationResult(string blobName, ModerationResult moderationResult);
        [Alias("AddThumbnail")]
        Task<Post> AddThumbnail(string blobName, Thumbnail thumbnail);
        [Alias("SetTranscodedBlobName")]
        Task<Post> SetTranscodedBlobName(string blobName, string transcodedBlobName);
        [Alias("Delete")]
        Task Delete();
    }
}
