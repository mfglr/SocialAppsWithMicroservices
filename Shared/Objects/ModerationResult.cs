using System.Text.Json.Serialization;

namespace Shared.Objects
{
    public record ModerationResult(int Hate, int SelfHarm, int Sexual, int Violence)
    {
        [JsonIgnore]
        public bool IsValid => Sexual == 0;

        public static ModerationResult Max(IEnumerable<ModerationResult> results)
        {
            int maxHate = 0, maxSelfHarm = 0, maxSexual = 0, maxViolence = 0;
            foreach (var result in results)
            {
                if (result.Hate > maxHate)
                    maxHate = result.Hate;
                if (result.SelfHarm > maxSelfHarm)
                    maxSelfHarm = result.SelfHarm;
                if (result.Sexual > maxSexual)
                    maxSexual = result.Sexual;
                if (result.Violence > maxViolence)
                    maxViolence = result.Violence;
            }
            return new(maxHate, maxSelfHarm, maxSexual, maxViolence);
        }
    }
}
