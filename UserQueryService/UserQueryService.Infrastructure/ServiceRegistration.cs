using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserQueryService.Domain;

namespace UserQueryService.Infrastructure
{
    public static class ServiceRegistration
    {
        private static void CreateIndex(ElasticsearchClient client, ElasticSearchOptions option)
        {
            var response = client.Indices.Exists("users");
            if (!response.Exists)
            {
                client.Indices.Create<User>(index => index
                    .Index(option.IndexName)
                    .Mappings(mappings => mappings
                        .Properties(properties => properties
                            .IntegerNumber(u => u.Version)
                            .Date(u => u.CreatedAt)
                            .Date(u => u.UpdatedAt)
                            .Text(u => u.Name, tpd => tpd.Fields(pd => pd.Keyword(u => u.Name)))
                            .Keyword(u => u.UserName)
                            .Keyword(u => u.Gender)
                            .Flattened(u => u.Media)
                        )
                    )
                );
            }
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var option = configuration.GetSection(nameof(ElasticSearchOptions)).Get<ElasticSearchOptions>()!;
            var clientSettings = new ElasticsearchClientSettings(new Uri(option.Host))
                .Authentication(new BasicAuthentication(option.UserName, option.Password));

            var client = new ElasticsearchClient(clientSettings);
            CreateIndex(client,option);

            return
                services
                .AddSingleton(option)
                .AddSingleton(client)
                .AddSingleton<IUserRepository,ElasticSearchUserRepository>();
        }
    }
}
