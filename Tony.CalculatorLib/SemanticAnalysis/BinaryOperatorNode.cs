using Tony.CalculatorLib.Definitions;
using Tony.CalculatorLib.LexicalAnalysis;

namespace Tony.CalculatorLib.SemanticAnalysis
{
    public class BinaryOperatorNode : IParseNode
    {
        public Token Token { get; }
        public BinaryOperatorDefinition Definition { get; }
        public IParseNode Left { get; set; }
        public IParseNode Right { get; set; }

        public BinaryOperatorNode(Token token, BinaryOperatorDefinition definition, IParseNode left, IParseNode right)
        {
            Token = token;
            Definition = definition;
            Left = left;
            Right = right;
        }

        public object Evaluate()
        {
            return Definition.Func(Left.Evaluate(), Right.Evaluate());
        }

        public override string ToString()
        {
            string operationSymbol = Definition?.Symbol ?? Token.Text.ToString();
            return $"{Left?.ToString()} {operationSymbol} {Right?.ToString()}";
        }
    }
}
