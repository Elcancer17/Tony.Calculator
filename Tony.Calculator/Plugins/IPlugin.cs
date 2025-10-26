using Tony.Calculator.Definitions;

namespace Tony.Calculator.Plugins
{
    public interface IPlugin
    {
        public void AddFunctions(Dictionary<string, FunctionDefinition> functions);
        public void AddVariables(Dictionary<string, VariableDefinition> variables);
        public void AddUnaryOperators(Dictionary<string, UnaryOperatorDefinition> unaryOperators);
        public void AddBinaryOperators(Dictionary<string, BinaryOperatorDefinition> binaryOperators);
    }
}
