using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.Calculator.Definitions
{
    public class FunctionDefinition
    {
        public string Name { get; }
        public int ParameterCount { get; }

        private readonly Func<object[], object> _func;
        public FunctionDefinition(string name, Func<object[], object> func, int parameterCount)
        {
            Name = name;
            _func = func;
            ParameterCount = parameterCount;
        }
        public object Execute(object[] args) 
        {
            if (args.Length != ParameterCount)
            {
                throw new Exception($"Expected {ParameterCount} parameter(s) but found {args.Length} for function {Name}().");
            }
            return _func(args);
        }


        public override string ToString()
        {
            string parameterString = string.Empty;
            for(int i = 0; i < ParameterCount; i++)
            {
                parameterString += nameof(Object);
                if(i < ParameterCount - 1)
                {
                    parameterString += ", ";
                }
            }
            return $"{Name}({parameterString})";
        }
    }
}
