namespace QueryService.Domain.PostDomain
{
    public class InvalidVersionException() : Exception("Version must be greater than 0.New version of a post must be greater than previous version!");
}
