using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DDCCI
{
    public class CapabilitiesTokenizer : ITokenizer
    {
        private IEnumerable<ITokenFilter<IToken>> tokenFilters = new List<ITokenFilter<IToken>>()
        {
            new TokenFilter<WhitespaceToken>("^\\s+$"),
            new TokenFilter<OpenToken>("^\\($"),
            new TokenFilter<CloseToken>("^\\)$"),
            new TokenFilter<WordToken>("^\\w+$"),
        };

        public IEnumerable<IToken> GetTokens(string inputString)
        {
            var characterQueue = new Queue<char>(inputString);

            while (characterQueue.Count > 0)
            {
                var token = GetToken(characterQueue);
                if (token != null)
                    yield return token;
            }
        }   

        private IToken GetToken(Queue<char> characterQueue)
        {
            foreach (var tokenFilter in tokenFilters)
            {
                var peekChar = characterQueue.Peek();

                if (Regex.IsMatch(new string(peekChar, 1), tokenFilter.Pattern))
                {
                    var result = new string(characterQueue.Dequeue(), 1);

                    if (characterQueue.Any())
                    {
                        while (characterQueue.Any() && Regex.IsMatch(result + characterQueue.Peek(), tokenFilter.Pattern))
                        {
                            result += characterQueue.Dequeue();
                        }
                    }

                    return tokenFilter.GetToken(result);
                }
            }

            characterQueue.Dequeue();
            return null;
        }
    }
}
