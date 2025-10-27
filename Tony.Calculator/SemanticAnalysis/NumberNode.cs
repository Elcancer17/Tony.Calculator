using Tony.Calculator.LexicalAnalysis;
using System.Text.RegularExpressions;

namespace Tony.Calculator.SemanticAnalysis
{
    public class NumberNode : IParseNode
    {
        private static Regex TrimRegex;
        static NumberNode()
        {
            TrimRegex = new Regex(@"_", RegexOptions.Compiled, TimeSpan.FromSeconds(1));
        }
        public Token Token { get; }
        public double Number { get; }
        public NumberNode(Token token)
        {
            Token = token;
            Number = double.Parse(TrimRegex.Replace(token.Text.ToString(), ""));
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
