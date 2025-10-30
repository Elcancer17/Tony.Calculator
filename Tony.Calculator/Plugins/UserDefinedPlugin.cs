using Tony.Calculator.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.Calculator.Plugins
{
    public class UserDefinedPlugin : IPlugin
    {
        IReadOnlyDictionary<string, FunctionDefinition> IPlugin.Functions => Functions;
        IReadOnlyDictionary<string, VariableDefinition> IPlugin.Variables => Variables;
        IReadOnlyDictionary<string, UnaryOperatorDefinition> IPlugin.UnaryOperators => UnaryOperators;
        IReadOnlyDictionary<string, BinaryOperatorDefinition> IPlugin.BinaryOperatos => BinaryOperatos;

        public Dictionary<string, FunctionDefinition> Functions { get; } = new Dictionary<string, FunctionDefinition>();
        public Dictionary<string, VariableDefinition> Variables { get; } = new Dictionary<string, VariableDefinition>();
        public Dictionary<string, UnaryOperatorDefinition> UnaryOperators { get; } = new Dictionary<string, UnaryOperatorDefinition>();
        public Dictionary<string, BinaryOperatorDefinition> BinaryOperatos { get; } = new Dictionary<string, BinaryOperatorDefinition>();
    }
}
