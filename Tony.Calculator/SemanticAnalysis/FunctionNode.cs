using Tony.Calculator.Definitions;
using Tony.Calculator.LexicalAnalysis;

namespace Tony.Calculator.SemanticAnalysis
{
    public class FunctionNode : IParseNode
    {
        public Token Token { get; }
        public int IndexOffset { get; set; }
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
            int endIndex;
            if(Parameters.Count == 0)
            {
                endIndex = Token.Index + 2;
            }
            else
            {
                endIndex = Parameters[^1].CalculateEndIndex() + 1;
            }
            return endIndex + IndexOffset;
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
