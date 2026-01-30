namespace BusinessService.Domain
{
    public class Business
    {

        public readonly static string ContainerName = "BusinessMedia";

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? UpdatedAt { get; private set; }
        public Name Name { get; private set; }
        public UserName UserName { get; private set; }
        public Media? Media { get; private set; }



        public Business(Guid id, Name name, UserName userName, Media? media)
        {
            Id = id;
            Name = name;
            UserName = userName;
            Media = media;
            CreatedAt = DateTime.UtcNow;
        }




    }
}
