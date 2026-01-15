using Shared.Objects;

namespace QueryService.Domain.UserDomain
{
    public class User
    {
        public Guid Id { get; private set; }
        public byte[] RowVersion { get; private set; } = null!;
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public int Version { get; private set; }
        public bool IsDeleted { get; private set; }
        public string? Name { get; private set; }
        public string Username { get; private set; }
        public string Gender { get; private set; }
        public List<Media> Media { get; private set; }

        private User() { }


        public User(
            Guid id,
            DateTime createdAt,
            DateTime? updatedAt,
            int version,
            bool isDeleted,
            string? name,
            string username,
            string gender,
            IEnumerable<Media> media
        ){
            Id = id;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
            Version = version;
            IsDeleted = isDeleted;
            Name = name;
            Username = username;
            Gender = gender;
            Media = [..media];
        }


        public void Set(
            DateTime? updatedAt,
            int version,
            bool isDeleted,
            string? name,
            string username,
            string gender,
            IEnumerable<Media> media
        )
        {
            UpdatedAt = updatedAt;
            Version = version;
            IsDeleted = isDeleted;
            Name = name;
            Username = username;
            Gender = gender;
            Media = [..media];
        }
    }
}
