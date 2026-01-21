using System.Text.Json.Serialization;

namespace Shared.Objects
{
    public record Metadata(double Width, double Height, double Duration)
    {
        [JsonIgnore]
        public bool IsValid => 0 <= Duration && Duration <= 180;
    }
}
