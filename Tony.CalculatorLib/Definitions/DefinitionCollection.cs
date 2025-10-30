using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony.CalculatorLib.Plugins;

namespace Tony.CalculatorLib.Definitions
{
    public class DefinitionCollection
    {
        public static DefinitionCollection Arithmetic { get; }
        static DefinitionCollection()
        {
            Arithmetic = new DefinitionCollection();
            Arithmetic.AddPlugin(new ArithmeticPlugin());
        }

        private Dictionary<string, FunctionDefinition> _functions = new(StringComparer.InvariantCultureIgnoreCase);
        public IReadOnlyDictionary<string, FunctionDefinition> Functions => _functions;

        private Dictionary<string, VariableDefinition> _variables = new(StringComparer.InvariantCultureIgnoreCase);
        public IReadOnlyDictionary<string, VariableDefinition> Variables => _variables;

        private Dictionary<string, UnaryOperatorDefinition> _unaryOperators = new(StringComparer.InvariantCultureIgnoreCase);
        public IReadOnlyDictionary<string, UnaryOperatorDefinition> UnaryOperators => _unaryOperators;

        private Dictionary<string, BinaryOperatorDefinition> _binaryOperators = new(StringComparer.InvariantCultureIgnoreCase);
        public IReadOnlyDictionary<string, BinaryOperatorDefinition> BinaryOperators => _binaryOperators;

        public void AddPlugin(IPlugin plugin)
        {
            foreach ((string name, FunctionDefinition definition) in plugin.Functions)
            {
                _functions.Add(name, definition);
            }
            foreach ((string name, VariableDefinition definition) in plugin.Variables)
            {
                _variables.Add(name, definition);
            }
            foreach ((string name, UnaryOperatorDefinition definition) in plugin.UnaryOperators)
            {
                _unaryOperators.Add(name, definition);
            }
            foreach ((string name, BinaryOperatorDefinition definition) in plugin.BinaryOperatos)
            {
                _binaryOperators.Add(name, definition);
            }
        }
    }
}
