using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.CalculatorLib.Definitions
{
    public class FunctionDefinition
    {
        public string Name { get; }
        public int ParameterCount { get; }
        public string Description { get; }

        private readonly Func<object[], object> _func;
        /// <param name="parameterCount">-1 to bypass the parameter count validation</param>
        public FunctionDefinition(string name, Func<object[], object> func, int parameterCount, string description)
        {
            Name = name;
            _func = func;
            ParameterCount = parameterCount;
            Description = description;
        }
        public object Execute(object[] args) 
        {
            if (ParameterCount != -1 && args.Length != ParameterCount)
            {
                throw new Exception($"Expected {ParameterCount} parameter(s) but found {args.Length} for function {Name}().");
            }
            return _func(args);
        }


        public override string ToString()
        {
            return Name;
        }
    }
}
