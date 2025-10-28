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
            { nameof(Double), new FunctionDefinition("double(arg)", Double, 1, "Cast number to double.")},
            { nameof(Int), new FunctionDefinition("int(arg)", Int, 1, "Cast number to int.")},
            { nameof(Mean), new FunctionDefinition("mean(args...)", Mean, -1, "Mean of list of numbers.")},
            { nameof(Math.Max), new FunctionDefinition("max(arg1, arg2)", Max, 2, "Max between two numbers.") },
            { nameof(Math.Min), new FunctionDefinition("min(arg1, arg2)", Min, 2, "Min between two numbers.") },
            { nameof(Round), new FunctionDefinition("round(arg, digits)", Round, 2, "Cast number to int.")},
            { nameof(Math.Log), new FunctionDefinition("log(arg, base)", Log, 2, "Parametrable base logarithm.")},
            { "ln", new FunctionDefinition("ln(arg)", NatualLog, 1, "Base e logarithm.")},
            { nameof(Math.Log2), new FunctionDefinition("log2(arg)", Log2, 1, "Base 2 logarithm.")},
            { nameof(Math.Log10), new FunctionDefinition("log10(arg)", Log10, 1, "Base 10 logarithm.")},
            { nameof(Math.Sqrt), new FunctionDefinition("sqrt(arg)", SquareRoot, 1, "Square root.")},
            { nameof(Math.Abs), new FunctionDefinition("abs(arg)", AbsoluteValue, 1, "Absolute value.")},
        };

        public IReadOnlyDictionary<string, VariableDefinition> Variables { get; } = new Dictionary<string, VariableDefinition>()
        {
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
        private static object Double(object[] args)
        {
            if (args[0] is double doubleValue)
            {
                return doubleValue;
            }
            if (args[0] is int intValue)
            {
                return (double)intValue;
            }
            throw new NotSupportedException();
        }

        private static object Int(object[] args)
        {
            if (args[0] is double doubleValue)
            {
                return (int)doubleValue;
            }
            if (args[0] is int intValue)
            {
                return intValue;
            }
            throw new NotSupportedException();
        }

        private static object Mean(object[] args)
        {
            double total = 0;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is double double1)
                {
                    total += double1;
                }
                else if (args[i] is int int1)
                {
                    total += int1;
                }
                else throw new NotSupportedException();
            }
            return total/args.Length;
        }

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

        private static object Round(object[] args)
        {
            if (args[1] is int numberOfDigits)
            {
                if (args[0] is double doubleValue)
                    return Math.Round(doubleValue, numberOfDigits);
                if (args[0] is int intValue)
                    return Math.Round((double)intValue, numberOfDigits);
            }
            throw new NotSupportedException();
        }

        private static object Log(object[] args)
        {
            if (args[0] is double doubleArg)
            {
                if (args[1] is double doubleBase)
                    return Math.Log(doubleArg, doubleBase);
                if (args[1] is int intBase)
                    return Math.Log(doubleArg, intBase);
            }
            if (args[0] is int intArg)
            {
                if (args[1] is double doubleBase)
                    return Math.Log(intArg, doubleBase);
                if (args[1] is int intBase)
                    return Math.Log(intArg, intBase);
            }
            throw new NotSupportedException();
        }

        private static object NatualLog(object[] args)
        {
            return Log([args[0], Math.E]);
        }

        private static object Log2(object[] args)
        {
            return Log([args[0], 2]);
        }

        private static object Log10(object[] args)
        {
            return Log([args[0], 10]);
        }

        private static object SquareRoot(object[] args)
        {
            if (args[0] is double doubleArg)
            {
                return Math.Sqrt(doubleArg);
            }
            if (args[0] is int intArg)
            {
                return Math.Sqrt(intArg);
            }
            throw new NotSupportedException();
        }

        private static object AbsoluteValue(object[] args)
        {
            if (args[0] is double doubleArg)
            {
                return Math.Abs(doubleArg);
            }
            if (args[0] is int intArg)
            {
                return Math.Abs(intArg);
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
