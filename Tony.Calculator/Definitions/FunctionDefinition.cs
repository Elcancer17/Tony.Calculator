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
        public Func<object[], object> Func { get; }
        public FunctionDefinition(string name, Func<object[], object> func)
        {
            Name = name;
            Func = func;
        }
    }
}
