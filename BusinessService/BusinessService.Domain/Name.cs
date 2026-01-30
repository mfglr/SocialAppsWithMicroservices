namespace BusinessService.Domain
{
    public class Name
    {
        public readonly static int MinLength = 3;
        public readonly static int MaxLength = 256;

        public string Value { get; private set; }

        public Name(string value)
        {
            if (string.IsNullOrEmpty(value) || value.Length < MinLength || value.Length > MaxLength)
                throw new InvalidNameException();
            Value = value;
        }
    }
}
