using Elastic.Clients.Elasticsearch;
using UserQueryService.Application;
using UserQueryService.Application.UseCases;

namespace UserQueryService.Infrastructure
{
    internal class ElasticSearchUserQueryService(ElasticSearchOptions option, ElasticsearchClient client) : IUserQueryRepository
    {
        private readonly ElasticSearchOptions option = option;
        private readonly ElasticsearchClient _client = client;

        public async Task<UserResponse?> GetByUserNameAsync(string userName, CancellationToken cancellationToken)
        {
            var response = await _client
                .SearchAsync<UserResponse>(
                    u => u
                        .Indices(option.IndexName)
                        .Query(q => q.Term(t => t.Field(f => f.UserName).Value(userName)))
                        .Size(1),
                    cancellationToken
                );
            return response.Documents.FirstOrDefault();
        }
    }
}
