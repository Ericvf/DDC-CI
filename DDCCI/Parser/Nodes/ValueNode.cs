using System.Collections.Generic;

namespace DDCCI
{
    public class ValueNode : INode
    {
        public string Value { get; set; }

        public INode Parent { get; set; }

        public IEnumerable<INode> Nodes { get; set; }

        public override string ToString() =>
            Parent.ToString() != null
                ? Parent.ToString() + "_" + Value
                : Value;
    }
}
