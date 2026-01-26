using Elastic.Clients.Elasticsearch;
using UserQueryService.Domain;

namespace UserQueryService.Infrastructure
{
    internal record Version(long? SeqNo, long? PrimaryTerm) : IVersion;

    internal class ElasticSearchUserRepository(ElasticsearchClient client, ElasticSearchOptions option) : IUserRepository
    {
        private readonly ElasticSearchOptions option = option;
        private readonly ElasticsearchClient _client = client;

        public Task CreateAsync(User user, CancellationToken cancellationToken) =>
            _client.IndexAsync(user, x => x.Index(option.IndexName), cancellationToken);

        public Task DeleteAsync(User user, CancellationToken cancelToken) =>
            _client.DeleteAsync(user, x => x.Index(option.IndexName), cancelToken);

        public async Task<UserVersion?> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync<User>(id, x => x.Index(option.IndexName), cancellationToken);

            return response.Source != null
                ? new(response.Source, new Version(response.SeqNo, response.PrimaryTerm))
                : null;
        }

        public async Task UpdateAsync(User user, IVersion? version, CancellationToken cancellationToken)
        {
            if (version is not Version v)
                throw new ArgumentException("Invalid version type", nameof(version));

            var response = await _client.UpdateAsync<User, User>(
                option.IndexName,
                user.Id,
                u => u.Doc(user).IfSeqNo(v.SeqNo).IfPrimaryTerm(v.PrimaryTerm),
                cancellationToken
            );

            if (!response.IsSuccess() && response.ElasticsearchServerError?.Status == 409)
                throw new AppConcurrencyException();
        }

    }
}
