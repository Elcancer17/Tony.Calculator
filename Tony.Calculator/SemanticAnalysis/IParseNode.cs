using Tony.Calculator.LexicalAnalysis;

namespace Tony.Calculator.SemanticAnalysis
{
    public interface IParseNode
    {
        public Token Token { get; }
        public int CalculateEndIndex();
        public string Print();
        public object Evaluate();
    }
}
