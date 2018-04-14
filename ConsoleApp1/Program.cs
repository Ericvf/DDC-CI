using DDCCI;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var cs = "(prot(monitor)type(lcd)27mu68cmds(01 02 03 0c e3 f3)vcp(02 04 05 08 10 12 14(05 08 0b ) 16 18 1a 52 60( 11 12 0f 10) ac ae b2 b6 c0 c6 c8 c9 d6(01 04) df 62 8d f4 f5(00 01 02) f6(00 01 02) 4d 4e 4f 15(01 07 08 11 13 14 15 18 19 20 22 23 24 28 29) f7(00 01 02 03) f8(00 01) f9 fd(00 01) fe(00 01 02) ff)mccs_ver(2.1)mswhql(1))";

            var tokenizer = new CapabilitiesTokenizer();
            var parser = new CapabilitiesParser();

            var tokens = tokenizer.GetTokens(cs);
            var node = parser.Parse(tokens);

            Print(node);
        }

        private static void Print(INode node, int i = 0)
        {
            Console.WriteLine(string.Empty.PadLeft(i, ' ') + node.ToString());

            if (node is IGroupNode groupNode)
            {
                foreach (var childNode in groupNode.Nodes)
                {
                    Print(childNode, i + 1);
                }
            }
        }
    }
}
