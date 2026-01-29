namespace PostService.Domain
{
    public record Metadata(double Width, double Height, double Duration)
    {
        public bool IsValid() => Duration >= 0 && Duration <= 300;
    }
}
