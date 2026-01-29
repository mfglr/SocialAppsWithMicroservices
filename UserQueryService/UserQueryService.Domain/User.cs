namespace UserQueryService.Domain
{
    public class User
    {
        public string Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public int Version { get; private set; }
        public string? Name { get; private set; }
        public string UserName { get; private set; }
        public string Gender { get; private set; }
        public IEnumerable<Media> Media { get; private set; }

        public User(string id, DateTime createdAt, DateTime? updatedAt, int version, string? name, string userName, string gender, IEnumerable<Media> media)
        {
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Version = version;
            Name = name;
            UserName = userName;
            Gender = gender;
            Media = media;
        }

        public void Set(DateTime? updatedAt, int version, string? name, string userName, string gender, IEnumerable<Media> media)
        {
            UpdatedAt = updatedAt;
            Version = version;
            Name = name;
            UserName = userName;
            Gender = gender;
            Media = media;
        }

    }
}
