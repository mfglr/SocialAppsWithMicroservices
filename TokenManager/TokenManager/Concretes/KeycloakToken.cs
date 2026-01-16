using System.Text.Json.Serialization;

namespace TokenManager.Concretes
{
    public class KeycloakToken(string accessToken)
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; private set; } = accessToken;
    }
}
