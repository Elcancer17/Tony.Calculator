using Tony.CalculatorLib.Definitions;
using Tony.CalculatorLib.LexicalAnalysis;

namespace Tony.CalculatorLib.SemanticAnalysis
{
    public class FunctionNode : IParseNode
    {
        public Token Token { get; }
        public FunctionDefinition Definition { get; }
        public IReadOnlyList<IParseNode> Parameters { get; }
        public FunctionNode(Token token, FunctionDefinition definition, IReadOnlyList<IParseNode> parameters)
        {
            Token = token;
            Definition = definition;
            Parameters = parameters;
        }

        public object Evaluate()
        {
            object[] parameters = Parameters.Select(x => x.Evaluate()).ToArray();
            return Definition.Execute(parameters);
        }

        public override string ToString()
        {
            string[] parameters = Parameters.Select(x => x.ToString()).ToArray();
            return $"{Definition.Name}({string.Join(", ", parameters)})";
        }
    }
}
