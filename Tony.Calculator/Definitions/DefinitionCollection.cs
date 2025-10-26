using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony.Calculator.Plugins;

namespace Tony.Calculator.Definitions
{
    public class DefinitionCollection
    {
        public Dictionary<string, FunctionDefinition> Functions { get; init; } = new();
        public Dictionary<string, VariableDefinition> Variables { get; init; } = new();
        public Dictionary<string, UnaryOperatorDefinition> UnaryOperators { get; init; } = new();
        public Dictionary<string, BinaryOperatorDefinition> BinaryOperators { get; init; } = new();

        public void AddPlugin(IPlugin plugin)
        {
            plugin.AddVariables(Variables);
            plugin.AddFunctions(Functions);
            plugin.AddUnaryOperators(UnaryOperators);
            plugin.AddBinaryOperators(BinaryOperators);
        }
    }
}
