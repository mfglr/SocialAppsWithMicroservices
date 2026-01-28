using Newtonsoft.Json;

namespace UserService.Domain
{
    [GenerateSerializer]
    [Alias("UserService.Domain.Metadata")]
    [method: JsonConstructor]
    public class Metadata(double width, double height, double duration)
    {
        [Id(0)]
        public double Width { get; private set; } = width;
        [Id(1)]
        public double Height { get; private set; } = height;
        [Id(2)]
        public double Duration { get; private set; } = duration;
    }
}
