namespace DDCCI
{
    public interface ITokenFilter<out T>
        where T : IToken
    {
        string Name { get; set; }

        string Pattern { get; set; }

        T GetToken(string value);
    }
}
