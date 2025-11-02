using Tony.CalculatorLib.Definitions;
using Tony.CalculatorLib.LexicalAnalysis;

namespace Tony.CalculatorLib.SemanticAnalysis
{
    public class ParenthesisNode : IParseNode
    {
        public Token Token { get; }
        public IParseNode Node { get; set; }

        public ParenthesisNode(Token token, IParseNode node)
        {
            Token = token;
            Node = node;
        }

        public object Evaluate()
        {
            return Node.Evaluate();
        }

        public override string ToString()
        {
            return $"({Node})";
        }
    }
}
