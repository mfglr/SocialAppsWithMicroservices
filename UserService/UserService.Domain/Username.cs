using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace UserService.Domain
{
    [GenerateSerializer]
    [Alias("UserService.Domain.Username")]
    public class Username
    {
        private readonly static string _pattern = @"^(?!.*\.\.)(?!\.)(?!.*\.$)[a-z0-9._]{1,50}$";

        [Id(0)]
        public string Value { get; private set; }

        [JsonConstructor]
        public Username(string value)
        {
            if (IsNotValid(value))
                throw new InvalidUsernameException();
            Value = value;
        }

        public static Username GenerateRandom() => new($"user_{Guid.NewGuid().ToString().Replace("-", "")}");

        public static bool IsNotValid(string value) =>
            string.IsNullOrEmpty(value) ||
            !Regex.IsMatch(value, _pattern);
    }
}

