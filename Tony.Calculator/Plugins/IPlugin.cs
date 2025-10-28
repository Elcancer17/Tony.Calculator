using Tony.Calculator.Definitions;

namespace Tony.Calculator.Plugins
{
    public interface IPlugin
    {
        public IReadOnlyDictionary<string, FunctionDefinition> Functions { get; }
        public IReadOnlyDictionary<string, VariableDefinition> Variables { get; }
        public IReadOnlyDictionary<string, UnaryOperatorDefinition> UnaryOperators { get; }
        public IReadOnlyDictionary<string, BinaryOperatorDefinition> BinaryOperatos { get; }
    }
}
