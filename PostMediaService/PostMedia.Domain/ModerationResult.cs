using Newtonsoft.Json;

namespace PostMedia.Domain
{
    [GenerateSerializer]
    [method: JsonConstructor]
    [Alias("PostMedia.Domain.ModerationResult")]
    public class ModerationResult(int hate, int selfHarm, int sexual, int violence)
    {
        [Id(0)]
        public int Hate { get; private set; } = hate;
        [Id(1)]
        public int SelfHarm { get; private set; } = selfHarm;
        [Id(2)]
        public int Sexual { get; private set; } = sexual;
        [Id(3)]
        public int Violence { get; private set; } = violence;

        public bool IsValid() => Sexual == 0;
    }
}
