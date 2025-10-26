using Tony.Calculator.Definitions;
using Tony.Calculator.LexicalAnalysis;

namespace Tony.Calculator.SemanticAnalysis
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

        public int CalculateEndIndex()
        {
            if(Parameters.Count == 0)
            {
                return Token.Index + 2;
            }
            return Parameters[^1].CalculateEndIndex() + 1;
        }

        public object Evaluate()
        {
            object[] parameters = Parameters.Select(x => x.Evaluate()).ToArray();
            return Definition.Func(parameters);
        }

        public string Print()
        {
            string[] parameters = Parameters.Select(x => x.Print()).ToArray();
            return $"{Definition.Name}({string.Join(", ", parameters)})";
        }
    }
}
