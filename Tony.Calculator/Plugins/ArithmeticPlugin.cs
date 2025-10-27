using Tony.Calculator.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tony.Calculator.Plugins
{
    public class ArithmeticPlugin : IPlugin
    {
        public static IReadOnlyDictionary<string, FunctionDefinition> Functions = new Dictionary<string, FunctionDefinition>()
        {
            { "max", new FunctionDefinition(nameof(Math.Max), Max, 2) },
            { "min", new FunctionDefinition(nameof(Math.Min), Min, 2) },
        };

        public static IReadOnlyDictionary<string, VariableDefinition> Variables = new Dictionary<string, VariableDefinition>()
        {
            { "tau", new VariableDefinition(nameof(Math.Tau), Math.Tau) },
            { "pi", new VariableDefinition(nameof(Math.PI), Math.PI) },
            { "e", new VariableDefinition(nameof(Math.E), Math.E) },
        };

        public static IReadOnlyDictionary<string, UnaryOperatorDefinition> UnaryOperators = new Dictionary<string, UnaryOperatorDefinition>()
        {
            { "-", new UnaryOperatorDefinition("-", nameof(Negative), Negative) },
        };

        public static IReadOnlyDictionary<string, BinaryOperatorDefinition> BinaryOperatos = new Dictionary<string, BinaryOperatorDefinition>()
        {
            { "+", new BinaryOperatorDefinition("+", nameof(Addition),          1, Addition) },
            { "-", new BinaryOperatorDefinition("-", nameof(Substraction),      1, Substraction) },
            { "*", new BinaryOperatorDefinition("*", nameof(Multiplication),    2, Multiplication) },
            { "/", new BinaryOperatorDefinition("/", nameof(Division),          2, Division) },
            { "%", new BinaryOperatorDefinition("%", nameof(Modulo),            2, Modulo) },
            { "^", new BinaryOperatorDefinition("^", nameof(Exponant),          3, Exponant) },
        };


        #region interface
        public void AddFunctions(Dictionary<string, FunctionDefinition> functions)
        {
            foreach((string name, FunctionDefinition definition) in Functions)
            {
                functions.Add(name, definition);
            }
        }
        public void AddVariables(Dictionary<string, VariableDefinition> variables)
        {
            foreach ((string name, VariableDefinition definition) in Variables)
            {
                variables.Add(name, definition);
            }
        }
        public void AddUnaryOperators(Dictionary<string, UnaryOperatorDefinition> unaryOperators)
        {
            foreach ((string name, UnaryOperatorDefinition definition) in UnaryOperators)
            {
                unaryOperators.Add(name, definition);
            }
        }
        public void AddBinaryOperators(Dictionary<string, BinaryOperatorDefinition> binaryOperators)
        {
            foreach ((string name, BinaryOperatorDefinition definition) in BinaryOperatos)
            {
                binaryOperators.Add(name, definition);
            }
        }
        #endregion

        #region function
        private static object Max(object[] args)
        {
            if (PluginHelper.TryCastDouble(args, out double[] doubles))
            {
                return Math.Max(doubles[0], doubles[1]);
            }
            else if (PluginHelper.TryCastLong(args, out long[] longs))
            {
                return Math.Max(longs[0], longs[1]);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
        private static object Min(object[] args)
        {
            if (PluginHelper.TryCastDouble(args, out double[] doubles))
            {
                return Math.Min(doubles[0], doubles[1]);
            }
            else if (PluginHelper.TryCastLong(args, out long[] longs))
            {
                return Math.Min(longs[0], longs[1]);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
        #endregion

        #region unary
        private static object Negative(object arg)
        {
            if (PluginHelper.TryCastDouble(arg, out double double1))
            {
                return -double1;
            }
            else if (PluginHelper.TryCastLong(arg, out long long1))
            {
                return -long1;
            }
            else
            {
                throw new NotSupportedException();
            }
        }
        #endregion

        #region binary
        private static object Addition(object arg1, object arg2)
        {
            if (PluginHelper.TryCastDouble(arg1, arg2, out double double1, out double double2))
            {
                return double1 + double2;
            }
            else if (PluginHelper.TryCastLong(arg1, arg2, out long long1, out long long2))
            {
                return long1 + long2;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private static object Substraction(object arg1, object arg2)
        {
            if (PluginHelper.TryCastDouble(arg1, arg2, out double double1, out double double2))
            {
                return double1 - double2;
            }
            else if (PluginHelper.TryCastLong(arg1, arg2, out long long1, out long long2))
            {
                return long1 - long2;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private static object Multiplication(object arg1, object arg2)
        {
            if (PluginHelper.TryCastDouble(arg1, arg2, out double double1, out double double2))
            {
                return double1 * double2;
            }
            else if (PluginHelper.TryCastLong(arg1, arg2, out long long1, out long long2))
            {
                return long1 * long2;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private static object Division(object arg1, object arg2)
        {
            if (PluginHelper.TryCastDouble(arg1, arg2, out double double1, out double double2))
            {
                return double1 / double2;
            }
            else if (PluginHelper.TryCastLong(arg1, arg2, out long long1, out long long2))
            {
                return long1 / long2;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private static object Modulo(object arg1, object arg2)
        {
            if (PluginHelper.TryCastDouble(arg1, arg2, out double double1, out double double2))
            {
                return double1 % double2;
            }
            else if (PluginHelper.TryCastLong(arg1, arg2, out long long1, out long long2))
            {
                return long1 % long2;
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private static object Exponant(object arg1, object arg2)
        {
            if (PluginHelper.TryCastDouble(arg1, arg2, out double double1, out double double2))
            {
                return Math.Pow(double1, double2);
            }
            else if (PluginHelper.TryCastLong(arg1, arg2, out long long1, out long long2))
            {
                return Math.Pow(long1, long2);
            }
            else
            {
                throw new NotSupportedException();
            }
        }
        #endregion
    }
}
