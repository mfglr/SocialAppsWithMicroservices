namespace TokenManager.Concretes
{
    public record Client(
        string ClientId,
        string ClientSecret
    );

    public record KeycloakAuthOptions(
        string HostName,
        string RealmName,
        IEnumerable<Client> Clients
    );
}
