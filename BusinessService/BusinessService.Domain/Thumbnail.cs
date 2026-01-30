namespace BusinessService.Domain
{
    public class Thumbnail(string blobName, double resolution, bool isSquare)
    {
        public string BlobName { get; private set; } = blobName;
        public double Resolution { get; private set; } = resolution;
        public bool IsSquare { get; private set; } = isSquare;
    }
}
