using Tony.Calculator.Definitions;
using Tony.Calculator.LexicalAnalysis;

namespace Tony.Calculator.SemanticAnalysis
{
    public class UnaryOperatorNode : IParseNode
    {
        public Token Token { get; }
        public UnaryOperatorDefinition Definition { get; }
        public IParseNode Operand { get; }
        public UnaryOperatorNode(Token token, UnaryOperatorDefinition definition, IParseNode operand)
        {
            Token = token;
            Definition = definition;
            Operand = operand;
        }

        public int CalculateEndIndex()
        {
            return Operand.CalculateEndIndex();
        }

        public object Evaluate()
        {
            return Definition.Func(Operand.Evaluate());
        }

        public override string ToString()
        {
            return $"{Definition.Symbol}({Operand.ToString()})";
        }
    }
}
