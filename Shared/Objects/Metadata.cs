namespace Shared.Objects
{
    public record Metadata(double Width, double Height, double Duration)
    {
        public bool IsValid => 0 <= Duration && Duration <= 180;
    }
}
