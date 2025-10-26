using Tony.Calculator.LexicalAnalysis;

namespace Tony.Calculator.SemanticAnalysis
{
    public interface IParseNode
    {
        public Token Token { get; }
        public int IndexOffset { get; set; }
        public int CalculateEndIndex();
        public object Evaluate();
    }
}
