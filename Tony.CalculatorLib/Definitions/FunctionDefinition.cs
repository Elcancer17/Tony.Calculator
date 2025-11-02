using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.CalculatorLib.Definitions
{
    public class FunctionDefinition
    {
        public string Identifier { get; }
        public int ParameterCount { get; }
        public string DisplayName { get; init; }
        public string Description { get; init; }

        private readonly Func<object[], object> _func;
        /// <param name="parameterCount">-1 to bypass the parameter count validation</param>
        public FunctionDefinition(string identifier, Func<object[], object> func, int parameterCount)
        {
            Identifier = identifier;
            _func = func;
            ParameterCount = parameterCount;
        }
        public object Execute(object[] args) 
        {
            if (ParameterCount != -1 && args.Length != ParameterCount)
            {
                throw new Exception($"Expected {ParameterCount} parameter(s) but found {args.Length} for function {DisplayName ?? $"{Identifier}()"}.");
            }
            return _func(args);
        }


        public override string ToString()
        {
            return DisplayName;
        }
    }
}
