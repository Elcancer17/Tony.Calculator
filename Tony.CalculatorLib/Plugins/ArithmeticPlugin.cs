using Tony.CalculatorLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics;

namespace Tony.CalculatorLib.Plugins
{
    public class ArithmeticPlugin : IPlugin
    {
        /*
            to do:
            fib 	Fibonacci number 	fib(x)
            gcd 	Greatest common divisor 	gcd(a1; a2; ...; an)
            lcm 	Least common multiple 	lcm(a1; a2; ...; an)
        */
        public IReadOnlyDictionary<string, FunctionDefinition> Functions { get; } = new Dictionary<string, FunctionDefinition>()
        {
            { "double", new FunctionDefinition("double", Double, 1)
                { 
                    DisplayName = "double(arg)",
                    Description = "Cast number to double."
                } 
            },
            { "int", new FunctionDefinition("int", Int, 1)
                { 
                    DisplayName = "int(arg)",
                    Description = "Cast number to int." 
                }
            },
            { "mean", new FunctionDefinition("mean", Mean, -1)
                {
                    DisplayName = "mean(args...)",
                    Description = "Mean of list of numbers."
                }
            },
            { "max", new FunctionDefinition("max", Max, 2)
                {
                    DisplayName = "max(arg1, arg2)",
                    Description = "Max between two numbers." 
                }
            },
            { "min", new FunctionDefinition("min", Min, 2)
                { 
                    DisplayName = "min(arg1, arg2)", 
                    Description = "Min between two numbers."
                } 
            },
            { "round", new FunctionDefinition("round", Round, 2)
                {
                    DisplayName = "round(arg, digits)",
                    Description = "Cast number to int."
                }
            },
            { "log", new FunctionDefinition("log", Log, 2)
                {
                    DisplayName = "log(arg, base)",
                    Description = "Parametrable base logarithm."
                }
            },
            { "ln", new FunctionDefinition("ln", NatualLog, 1)
                {
                    DisplayName = "ln(arg)",
                    Description = "Base e logarithm."
                }
            },
            { "log2", new FunctionDefinition("log2", Log2, 1)
                {
                    DisplayName = "log2(arg)",
                    Description = "Base 2 logarithm."
                }
            },
            { "log10", new FunctionDefinition("log10", Log10, 1)
                {
                    DisplayName = "log10(arg)",
                    Description = "Base 10 logarithm."
                }
            },
            { "sqrt", new FunctionDefinition("sqrt", SquareRoot, 1)
                {
                    DisplayName = "sqrt(arg)",
                    Description = "Square root."
                }
            },
            { "abs", new FunctionDefinition("abs", AbsoluteValue, 1)
                {
                    DisplayName = "abs(arg)",
                    Description = "Absolute value."
                }
            },
            { "sign", new FunctionDefinition("sign", Signum, 1)
                {
                    DisplayName = "sign(arg)",
                    Description = "Signum function. Return -1, 0 or 1 depending on the signe of the parameter."
                }
            },
            { "floor", new FunctionDefinition("floor", Floor, 1)
                {
                    DisplayName = "floor(arg)",
                    Description = "Round down to the nearest integer."
                }
            },
            { "ceil", new FunctionDefinition("ceil", Ceiling, 1)
                {
                    DisplayName = "ceil(arg)",
                    Description = "Round up to the nearest integer."
                }
            },
            { "mod", new FunctionDefinition("mod", ModuloFunc, 2)
                {
                    DisplayName = "mod(arg1, arg2)",
                    Description = "Remainder of division."
                }
            },
            { "fact", new FunctionDefinition("fact", FactorialFunc, 1)
                {
                    DisplayName = "fact(arg)",
                    Description = "."
                }
            },
        };

        public IReadOnlyDictionary<string, VariableDefinition> Variables { get; } = new Dictionary<string, VariableDefinition>()
        {
            { nameof(Math.E), new VariableDefinition(nameof(Math.E), Math.E) },
            { nameof(Math.PI), new VariableDefinition(nameof(Math.PI), Math.PI) },
            { nameof(Math.Tau), new VariableDefinition(nameof(Math.Tau), Math.Tau) },
        };

        public IReadOnlyDictionary<string, UnaryOperatorDefinition> UnaryOperators { get; } = new Dictionary<string, UnaryOperatorDefinition>()
        {
            { "-", new UnaryOperatorDefinition("-", Negative) 
                {
                    DisplayName = "Negative"
                } 
            },
            { "!", new UnaryOperatorDefinition("!", FactorialOp)
                {
                    DisplayName = "Factorial"
                }
            },
        };

        public IReadOnlyDictionary<string, BinaryOperatorDefinition> BinaryOperatos { get; } = new Dictionary<string, BinaryOperatorDefinition>()
        {
            { "+", new BinaryOperatorDefinition("+", 1, Addition)
                {
                    DisplayName = "Addition"
                }
            },
            { "-", new BinaryOperatorDefinition("-", 1, Substraction)
                {
                    DisplayName = "Substraction"
                }
            },
            { "*", new BinaryOperatorDefinition("*", 2, Multiplication)
                {
                    DisplayName = "Multiplication"
                }
            },
            { "/", new BinaryOperatorDefinition("/", 2, Division)
                {
                    DisplayName = "Division"
                }
            },
            { "%", new BinaryOperatorDefinition("%", 2, ModuloBinaryOp)
                {
                    DisplayName = "Modulo"
                }
            },
            { "^", new BinaryOperatorDefinition("^", 3, Exponant)
                {
                    DisplayName = "Exponant"
                }
            },
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

        private static object Signum(object[] args)
        {
            if (args[0] is double doubleArg)
            {
                return Math.Sign(doubleArg);
            }
            if (args[0] is int intArg)
            {
                return Math.Sign(intArg);
            }
            throw new NotSupportedException();
        }

        private static object Floor(object[] args)
        {
            if (args[0] is double doubleArg)
                return Math.Floor(doubleArg);
            if (args[0] is int intArg)
                return Math.Floor((double)intArg);
            throw new NotSupportedException();
        }

        private static object Ceiling(object[] args)
        {
            if (args[0] is double doubleArg)
                return Math.Ceiling(doubleArg);
            if (args[0] is int intArg)
                return Math.Ceiling((double)intArg);
            throw new NotSupportedException();
        }

        private static object ModuloFunc(object[] args)
        {
            return ModuloBinaryOp(args[0], args[1]);
        }

        private static object FactorialFunc(object[] args)
        {
            return FactorialOp(args[0]);
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
        private static object FactorialOp(object arg)
        {
            if (arg is double doubleArg)
                return SpecialFunctions.Gamma(doubleArg + 1);
            if (arg is int intArg)
                return SpecialFunctions.Factorial(intArg);
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

        private static object ModuloBinaryOp(object arg1, object arg2)
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
