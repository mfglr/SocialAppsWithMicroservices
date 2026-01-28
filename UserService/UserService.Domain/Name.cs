using Newtonsoft.Json;

namespace UserService.Domain
{
    [GenerateSerializer]
    [Alias("UserService.Domain.Name")]
    public class Name
    {
        public readonly static int MaxLenght = 256;

        [Id(0)]
        public string Value { get; private set; }

        [JsonConstructor]
        public Name(string value)
        {
            if (IsNotValid(value))
                throw new InvalidNameException();
            Value = value;
        }

        public static bool IsNotValid(string value) =>
            string.IsNullOrEmpty(value) ||
            value.Length > MaxLenght;
    }
}
