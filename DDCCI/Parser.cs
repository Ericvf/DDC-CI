using System.Collections.Generic;
using System.Linq;

namespace DDCCI
{
    public class CapabilitiesParser : IParser
    {
        public INode Parse(IEnumerable<IToken> tokens)
        {
            var tokenQueue = new Queue<IToken>(tokens);
            var nodeStack = new Stack<INode>();

            ParseTokens(tokenQueue, nodeStack);
            return new RootNode
            {
                Nodes = nodeStack // ParseTokens(tokenQueue, outputQueue).ToList()
            };
        }

        private void ParseTokens(Queue<IToken> tokenQueue, Stack<INode> nodes)
        {
            while (tokenQueue.Any())
            {
                var token = tokenQueue.Dequeue();
                switch (token)
                {
                    case WordToken wordToken:
                        nodes.Push(ParseWordToken(wordToken));
                        break;

                    case OpenToken openToken:
                        nodes.Push(ParseOpenToken(openToken, tokenQueue, nodes));
                        break;

                    case CloseToken closeToken:
                        return;
                }
            }
        }

        private INode ParseOpenToken(OpenToken openToken, Queue<IToken> tokenQueue, Stack<INode> nodes)
        {
            var outputQueue = new Stack<INode>();
            ParseTokens(tokenQueue, outputQueue);

            INode node = null;
            if (nodes.Any())
            {
                node = nodes.Pop();
            }

            var currentNodes = outputQueue.ToList();
            currentNodes.Reverse();

            return new GroupValueNode()
            {
                Value = node?.Value,
                Nodes = currentNodes
            };
        }

        private INode ParseWordToken(WordToken wordToken)
        {
            return new ValueNode()
            {
                Value = wordToken.Value
            };
        }
    }

    public interface IParser
    {
        INode Parse(IEnumerable<IToken> tokens);
    }

    public class RootNode : IGroupNode
    {
        public IEnumerable<INode> Nodes { get; set; }

        public string Value { get; set; }
    }

    public class ValueNode : INode
    {
        public string Value { get; set; }

        public override string ToString() => Value;
    }

    public class GroupValueNode : IGroupNode
    {
        public string Value { get; set; }

        public IEnumerable<INode> Nodes { get; set; }

        public override string ToString() => Value;
    }

    public interface IGroupNode : INode
    {
        IEnumerable<INode> Nodes { get; set; }
    }

    public interface INode
    {
        string Value { get; set; }
    }
}
