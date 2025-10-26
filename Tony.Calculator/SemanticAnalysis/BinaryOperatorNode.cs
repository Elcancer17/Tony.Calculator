using Tony.Calculator.Definitions;
using Tony.Calculator.LexicalAnalysis;

namespace Tony.Calculator.SemanticAnalysis
{
    public class BinaryOperatorNode : IParseNode
    {
        public Token Token { get; }
        public int IndexOffset { get; set; }
        public BinaryOperatorDefinition Definition { get; }
        public IParseNode Left { get; }
        public IParseNode Right { get; }

        public BinaryOperatorNode(Token token, BinaryOperatorDefinition definition, IParseNode left, IParseNode right)
        {
            Token = token;
            Definition = definition;
            Left = left;
            Right = right;
        }

        public int CalculateEndIndex()
        {
            return Right.CalculateEndIndex() + IndexOffset;
        }

        public object Evaluate()
        {
            return Definition.Func(Left.Evaluate(), Right.Evaluate());
        }

        public override string ToString()
        {
            return $"({Left.ToString()}{Definition.Symbol}{Right.ToString()})";
        }
    }
}
