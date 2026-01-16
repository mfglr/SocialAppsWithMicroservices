namespace BlobService.Api.Concretes
{
    internal class UniqNameGenerator
    {
        public string Generate(string? extention = null)
        {
            if (extention == null)
                return $"{Guid.NewGuid()}_{DateTime.UtcNow.Ticks}";
            return $"{Guid.NewGuid()}_{DateTime.UtcNow.Ticks}.{extention}";
        }
    }
}
