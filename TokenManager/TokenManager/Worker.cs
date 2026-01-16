using TokenManager.Abstracts;
using TokenManager.Concretes;

namespace TokenManager
{
    public class Worker(IAccessTokenCache accessTokenCache, IAccessTokenProvider accessTokenProvider, KeycloakAuthOptions options, ILogger<Worker> logger) : BackgroundService
    {
        private readonly static int _expiresIn = 900;
        private readonly static int _safetyWindow = 60;

        private readonly ILogger<Worker> _logger = logger;
        private readonly IAccessTokenCache _accessTokenCache = accessTokenCache;
        private readonly IAccessTokenProvider _accessTokenProvider = accessTokenProvider;
        private readonly KeycloakAuthOptions _options = options;
        private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(_expiresIn - _safetyWindow));

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                foreach (var client in _options.Clients)
                {
                    var _ = 
                        Task
                            .Run(
                                async () => {
                                    var accessToken = await _accessTokenProvider.GetAccessTokenAsync(client.ClientId, client.ClientSecret, stoppingToken);
                                    _accessTokenCache.Set(client.ClientId, accessToken);
                                },
                                stoppingToken
                            );
                    
                    _logger.Log(LogLevel.Information, $"{client.ClientId} has been authenticated.");
                }
            } while (await _timer.WaitForNextTickAsync(stoppingToken));
        }
    }
}
