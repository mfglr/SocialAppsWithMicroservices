using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using PostService.Domain;

namespace PostService.Infrastructure
{
    public static class DbConfiguration
    {
        public static void Configure()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            BsonClassMap.RegisterClassMap<Post>(cm =>
            {
                cm.MapIdMember(q => q.Id);
                cm.MapMember(q => q.CreatedAt);
                cm.MapMember(q => q.UpdatedAt);
                cm.MapMember(q => q.Version);
                cm.MapMember(q => q.Content);
                cm.MapMember(q => q.Media);
            });
        }
    }
}
