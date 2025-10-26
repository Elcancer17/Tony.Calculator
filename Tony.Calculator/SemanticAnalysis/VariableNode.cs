using Tony.Calculator.Definitions;
using Tony.Calculator.LexicalAnalysis;

namespace Tony.Calculator.SemanticAnalysis
{
    public class VariableNode : IParseNode
    {
        public Token Token { get; }
        public VariableDefinition Definition { get; }
        public VariableNode(Token token, VariableDefinition definition)
        {
            Token = token;
            Definition = definition;
        }

        public int CalculateEndIndex()
        {
            return Token.Index;
        }

        public object Evaluate()
        {
            return Definition.Value;
        }

        public string Print()
        {
            return Definition.Value.ToString();
        }
    }
}
