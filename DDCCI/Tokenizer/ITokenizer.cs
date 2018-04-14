using System.Collections.Generic;

namespace DDCCI
{
    public interface ITokenizer
    {
        IEnumerable<IToken> GetTokens(string inputString);
    }
}
