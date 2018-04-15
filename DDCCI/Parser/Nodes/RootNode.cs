using System.Collections.Generic;

namespace DDCCI
{
    public class RootNode : INode
    {
        public IEnumerable<INode> Nodes { get; set; }

        public INode Parent { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            return null;
        }
    }
}
