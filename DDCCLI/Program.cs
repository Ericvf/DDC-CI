using DDCCI;
using System;
using System.Linq;

namespace DCCLI
{
    /// <summary>
    /// var cs = "(prot(monitor)type(lcd)27mu68cmds(01 02 03 0c e3 f3)vcp(02 04 05 08 10 12 14(05 08 0b ) 16 18 1a 52 60( 11 12 0f 10) ac ae b2 b6 c0 c6 c8 c9 d6(01 04) df 62 8d f4 f5(00 01 02) f6(00 01 02) 4d 4e 4f 15(01 07 08 11 13 14 15 18 19 20 22 23 24 28 29) f7(00 01 02 03) f8(00 01) f9 fd(00 01) fe(00 01 02) ff)mccs_ver(2.1)mswhql(1))";
    /// </summary>
    class Program
    {
        static DisplayService displayService = new DisplayService();
        static INodeFormatter nodeFormatter = new NodeFormatter();
        static ITokenizer tokenizer = new CapabilitiesTokenizer();
        static IParser parser = new CapabilitiesParser();

        static void Main(string[] args)
        {
            var monitors = displayService.GetMonitors();

            foreach (var monitor in monitors)
            {
                Console.WriteLine($"Monitor: {monitor.Name}");

                var cs = displayService.GetCapabilities(monitor);
                Console.WriteLine($"CapabilitiesString: {cs}");

                var tokens = tokenizer.GetTokens(cs);
                var node = parser.Parse(tokens);

                var vcpNode = node.Nodes.RecursiveSelect(n => n.Nodes)
                    .Single(n => n.Value == "vcp");

                ConsoleWrite(vcpNode);
            }
        }

        static void ConsoleWrite(INode node, int i = 0)
        {
            var formattedNode = nodeFormatter.FormatNode(node);

            if (!string.IsNullOrWhiteSpace(formattedNode))
            {
                var indents = string.Empty.PadLeft(i, ' ');
                Console.WriteLine(indents + formattedNode);
            }

            if (node.Nodes != null && node.Nodes.Any())
                foreach (var childNode in node.Nodes)
                    ConsoleWrite(childNode, i + 1);
        }
    }
}
