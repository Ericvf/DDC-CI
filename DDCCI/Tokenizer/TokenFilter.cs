namespace DDCCI
{
    public class TokenFilter<T> : ITokenFilter<T>
        where T : IToken, new()
    {
        public TokenFilter(string pattern)
        {
            Name = typeof(T).Name;
            Pattern = pattern;
        }

        public string Name { get; set; }

        public string Pattern { get; set; }

        public T GetToken(string value)
        {
            return new T
            {
                Type = Name,
                Value = value
            };
        }
    }
}
