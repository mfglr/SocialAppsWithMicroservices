namespace BusinessService.Domain
{
    public class Metadata(double width, double height, double duration)
    {
        public double Width { get; private set; } = width;
        public double Height { get; private set; } = height;
        public double Duration { get; private set; } = duration;
    }
}
