using Newtonsoft.Json;

namespace UserService.Domain
{
    [GenerateSerializer]
    [Alias("UserService.Domain.Gender")]
    public class Gender
    {
        private static class GenderNames
        {
            public readonly static string Unknown = "Unkown";
            public readonly static string Male = "Male";
            public readonly static string Female = "Female";
        }

        [Id(0)]
        public string Value { get; private set; }

        [JsonConstructor]
        private Gender(string value) => Value = value;

        public static Gender Unknown() => new(GenderNames.Unknown);
        public static Gender Male() => new(GenderNames.Male);
        public static Gender Female() => new(GenderNames.Female);

        public static bool operator ==(Gender x, Gender y) => x.Value == y.Value;
        public static bool operator !=(Gender x, Gender y) => x.Value != y.Value;

        public override bool Equals(object? obj) =>
            ReferenceEquals(this, obj) || (obj is not null && obj is Gender other && Value == other.Value);

        public override int GetHashCode() => Value.GetHashCode();
    }
}
