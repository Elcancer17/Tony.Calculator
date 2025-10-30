using Tony.CalculatorLib.Definitions;
using Tony.CalculatorLib.LexicalAnalysis;

namespace Tony.CalculatorLib.SemanticAnalysis
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

        public object Evaluate()
        {
            return Definition.Value;
        }

        public override string ToString()
        {
            return Definition.Value.ToString();
        }
    }
}
