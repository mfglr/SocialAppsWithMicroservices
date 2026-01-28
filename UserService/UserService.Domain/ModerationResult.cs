using Newtonsoft.Json;

namespace UserService.Domain
{
    [GenerateSerializer]
    [Alias("UserService.Domain.ModerationResult")]
    [method: JsonConstructor]
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
    }
}
