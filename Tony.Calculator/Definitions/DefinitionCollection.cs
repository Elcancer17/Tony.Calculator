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
        public static DefinitionCollection Arithmetic { get; }
        static DefinitionCollection()
        {
            Arithmetic = new DefinitionCollection();
            Arithmetic.AddPlugin(new ArithmeticPlugin());
        }

        public Dictionary<string, FunctionDefinition> Functions { get; } = new(StringComparer.InvariantCultureIgnoreCase);
        public Dictionary<string, VariableDefinition> Variables { get; } = new(StringComparer.InvariantCultureIgnoreCase);
        public Dictionary<string, UnaryOperatorDefinition> UnaryOperators { get; } = new(StringComparer.InvariantCultureIgnoreCase);
        public Dictionary<string, BinaryOperatorDefinition> BinaryOperators { get; } = new(StringComparer.InvariantCultureIgnoreCase);

        public void AddPlugin(IPlugin plugin)
        {
            plugin.AddVariables(Variables);
            plugin.AddFunctions(Functions);
            plugin.AddUnaryOperators(UnaryOperators);
            plugin.AddBinaryOperators(BinaryOperators);
        }
    }
}
