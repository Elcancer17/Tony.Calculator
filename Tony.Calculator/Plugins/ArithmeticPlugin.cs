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
        public IReadOnlyDictionary<string, FunctionDefinition> Functions { get; } = new Dictionary<string, FunctionDefinition>()
        {
            { nameof(Math.Max), new FunctionDefinition(nameof(Math.Max), Max, 2) },
            { nameof(Math.Min), new FunctionDefinition(nameof(Math.Min), Min, 2) },
        };

        public IReadOnlyDictionary<string, VariableDefinition> Variables { get; } = new Dictionary<string, VariableDefinition>()
        {
            { nameof(Math.Tau), new VariableDefinition(nameof(Math.Tau), Math.Tau) },
            { nameof(Math.PI), new VariableDefinition(nameof(Math.PI), Math.PI) },
            { nameof(Math.E), new VariableDefinition(nameof(Math.E), Math.E) },
        };

        public IReadOnlyDictionary<string, UnaryOperatorDefinition> UnaryOperators { get; } = new Dictionary<string, UnaryOperatorDefinition>()
        {
            { "-", new UnaryOperatorDefinition("-", nameof(Negative), Negative) },
        };

        public IReadOnlyDictionary<string, BinaryOperatorDefinition> BinaryOperatos { get; } = new Dictionary<string, BinaryOperatorDefinition>()
        {
            { "+", new BinaryOperatorDefinition("+", nameof(Addition),          1, Addition) },
            { "-", new BinaryOperatorDefinition("-", nameof(Substraction),      1, Substraction) },
            { "*", new BinaryOperatorDefinition("*", nameof(Multiplication),    2, Multiplication) },
            { "/", new BinaryOperatorDefinition("/", nameof(Division),          2, Division) },
            { "%", new BinaryOperatorDefinition("%", nameof(Modulo),            2, Modulo) },
            { "^", new BinaryOperatorDefinition("^", nameof(Exponant),          3, Exponant) },
        };


        #region function
        private static object Max(object[] args)
        {
            if (args[0] is double double1)
            {
                if (args[1] is double double2)
                    return Math.Max(double1, double2);
                if (args[1] is int int2)
                    return Math.Max(double1, int2);
            }
            if (args[0] is int int1)
            {
                if (args[1] is double double2)
                    return Math.Max(int1, double2);
                if (args[1] is int int2)
                    return Math.Max(int1, int2);
            }
            throw new NotSupportedException();
        }
        private static object Min(object[] args)
        {
            if (args[0] is double double1)
            {
                if (args[1] is double double2)
                    return Math.Min(double1, double2);
                if (args[1] is int int2)
                    return Math.Min(double1, int2);
            }
            if (args[0] is int int1)
            {
                if (args[1] is double double2)
                    return Math.Min(int1, double2);
                if (args[1] is int int2)
                    return Math.Min(int1, int2);
            }
            throw new NotSupportedException();
        }
        #endregion

        #region unary
        private static object Negative(object arg)
        {
            if (arg is double double1)
            {
                return -double1;
            }
            if (arg is int int1)
            {
                return -int1;
            }
            throw new NotSupportedException();
        }
        #endregion

        #region binary
        private static object Addition(object arg1, object arg2)
        {
            if(arg1 is double double1)
            {
                if (arg2 is double double2)
                    return double1 + double2;
                if (arg2 is int int2)
                    return double1 + int2;
            }
            if (arg1 is int int1)
            {
                if (arg2 is double double2)
                    return int1 + double2;
                if (arg2 is int int2)
                    return int1 + int2;
            }
            throw new NotSupportedException();
        }

        private static object Substraction(object arg1, object arg2)
        {
            if (arg1 is double double1)
            {
                if (arg2 is double double2)
                    return double1 - double2;
                if (arg2 is int int2)
                    return double1 - int2;
            }
            if (arg1 is int int1)
            {
                if (arg2 is double double2)
                    return int1 - double2;
                if (arg2 is int int2)
                    return int1 - int2;
            }
            throw new NotSupportedException();
        }

        private static object Multiplication(object arg1, object arg2)
        {
            if (arg1 is double double1)
            {
                if (arg2 is double double2)
                    return double1 * double2;
                if (arg2 is int int2)
                    return double1 * int2;
            }
            if (arg1 is int int1)
            {
                if (arg2 is double double2)
                    return int1 * double2;
                if (arg2 is int int2)
                    return int1 * int2;
            }
            throw new NotSupportedException();
        }

        private static object Division(object arg1, object arg2)
        {
            if (arg1 is double double1)
            {
                if (arg2 is double double2)
                    return double1 / double2;
                if (arg2 is int int2)
                    return double1 / int2;
            }
            if (arg1 is int int1)
            {
                if (arg2 is double double2)
                    return int1 / double2;
                if (arg2 is int int2)
                    return int1 / int2;
            }
            throw new NotSupportedException();
        }

        private static object Modulo(object arg1, object arg2)
        {
            if (arg1 is double double1)
            {
                if (arg2 is double double2)
                    return double1 % double2;
                if (arg2 is int int2)
                    return double1 % int2;
            }
            if (arg1 is int int1)
            {
                if (arg2 is double double2)
                    return int1 % double2;
                if (arg2 is int int2)
                    return int1 % int2;
            }
            throw new NotSupportedException();
        }

        private static object Exponant(object arg1, object arg2)
        {
            if (arg1 is double double1)
            {
                if (arg2 is double double2)
                    return Math.Pow(double1, double2);
                if (arg2 is int int2)
                    return Math.Pow(double1, int2);
            }
            if (arg1 is int int1)
            {
                if (arg2 is double double2)
                    return Math.Pow(int1, double2);
                if (arg2 is int int2)
                    return Math.Pow(int1, int2);
            }
            throw new NotSupportedException();
        }
        #endregion
    }
}
