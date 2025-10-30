using Tony.CalculatorLib.LexicalAnalysis;

namespace Tony.CalculatorLib.SemanticAnalysis
{
    public interface IParseNode
    {
        public Token Token { get; }
        public object Evaluate();
    }
}
