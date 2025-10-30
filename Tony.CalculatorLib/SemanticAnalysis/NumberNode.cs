using Tony.CalculatorLib.LexicalAnalysis;
using System.Text.RegularExpressions;
using System.Text;

namespace Tony.CalculatorLib.SemanticAnalysis
{
    public class NumberNode : IParseNode
    {
        private static Regex TrimRegex;
        static NumberNode()
        {
            TrimRegex = new Regex(@"_", RegexOptions.Compiled, TimeSpan.FromSeconds(1));
        }
        public Token Token { get; }
        public object Number { get; }
        public NumberNode(Token token)
        {
            Token = token;
            bool parseToDouble = false;
            string text = token.Text.ToString();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if(c != '_')
                {
                    if(c == '.')
                    {
                        parseToDouble = true;
                    }
                    sb.Append(c);
                }
            }
            if (parseToDouble)
            {
                Number = double.Parse(sb.ToString());
            }
            else
            {
                Number = int.Parse(sb.ToString());
            }
        }

        public object Evaluate()
        {
            return Number;
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
