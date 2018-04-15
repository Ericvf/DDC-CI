using System.Collections.Generic;

namespace DDCCI
{
    public interface IParser
    {
        INode Parse(IEnumerable<IToken> tokens);
    }
}
