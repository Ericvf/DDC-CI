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

            var rootNode = new RootNode();
            ParseTokens(tokenQueue, nodeStack, rootNode);
            rootNode.Nodes = nodeStack;

            return rootNode;
        }

        private void ParseTokens(Queue<IToken> tokenQueue, Stack<INode> nodeStack, INode parentNode)
        {
            INode node;

            while (tokenQueue.Any())
            {
                var token = tokenQueue.Dequeue();
                switch (token)
                {
                    case WordToken wordToken:
                        node = ParseWordToken(wordToken, parentNode);
                        nodeStack.Push(node);
                        break;

                    case OpenToken openToken:
                        node = ParseOpenToken(openToken, tokenQueue, nodeStack, parentNode);
                        nodeStack.Push(node);
                        break;

                    case CloseToken closeToken:
                        return;
                }
            }
        }

        private INode ParseOpenToken(OpenToken openToken, Queue<IToken> tokenQueue, Stack<INode> nodeStack, INode parent)
        {
            var outputQueue = new Stack<INode>();

            INode previousNode = null;
            if (nodeStack.Any())
            {
                previousNode = nodeStack.Pop();
            }

            var node = new GroupValueNode()
            {
                Value = previousNode?.Value,
                Parent = parent,
            };

            ParseTokens(tokenQueue, outputQueue, node);

            var currentNodes = outputQueue.ToList();
            currentNodes.Reverse();

            node.Nodes = currentNodes;

            return node;
        }

        private INode ParseWordToken(WordToken wordToken, INode parent)
        {
            return new ValueNode()
            {
                Value = wordToken.Value,
                Parent = parent
            };
        }
    }
}
