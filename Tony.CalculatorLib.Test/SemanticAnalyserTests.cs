using Tony.CalculatorLib.SemanticAnalysis;
using Tony.CalculatorLib.Definitions;
using Tony.CalculatorLib.Plugins;
using Tony.CalculatorLib.LexicalAnalysis;
using System.Diagnostics;

namespace Tony.CalculatorLib.Test
{
    [TestClass]
    public sealed class SemanticAnalyserTests
    {
        private LexicalAnalyser _lexicalAnalyzer;
        private SemanticAnalyser _semanticAnalyzer;
        [TestInitialize]
        public void TestInitialize()
        {
            _lexicalAnalyzer = new LexicalAnalyser();

            DefinitionCollection definitions = new DefinitionCollection();
            ArithmeticPlugin plugin = new ArithmeticPlugin();
            definitions.AddPlugin(plugin);
            _semanticAnalyzer = new SemanticAnalyser(definitions);
        }

        [TestMethod]
        public void CanParseSimpleNumber()
        {
            const double EXPECTED_VALUE = 12;
            string equation = "12";
            IReadOnlyList<Token> tokenStream = _lexicalAnalyzer.Analyse(equation);

            IParseNode root = _semanticAnalyzer.Parse(tokenStream, out List<SemanticError> errors);

            double result = (double)root.Evaluate();
            Trace.WriteLine($"{root.ToString()}={result}");
            Assert.AreEqual(EXPECTED_VALUE, result);
            Assert.IsTrue(errors.Count == 0);
        }

        [TestMethod]
        public void CanParseNumberWithDecimals()
        {
            const double EXPECTED_VALUE = 123.456;
            string equation = "123.456";
            IReadOnlyList<Token> tokenStream = _lexicalAnalyzer.Analyse(equation);

            IParseNode root = _semanticAnalyzer.Parse(tokenStream, out List<SemanticError> errors);

            double result = (double)root.Evaluate();
            Trace.WriteLine($"{root.ToString()}={result}");
            Assert.AreEqual(EXPECTED_VALUE, result);
            Assert.IsTrue(errors.Count == 0);
        }

        [TestMethod]
        public void CanParseNumberWithSpacers()
        {
            const double EXPECTED_VALUE = 123.456;
            string equation = "1_2_3.4_5_6";
            IReadOnlyList<Token> tokenStream = _lexicalAnalyzer.Analyse(equation);

            IParseNode root = _semanticAnalyzer.Parse(tokenStream, out List<SemanticError> errors);

            double result = (double)root.Evaluate();
            Trace.WriteLine($"{root.ToString()}={result}");
            Assert.AreEqual(EXPECTED_VALUE, result);
            Assert.IsTrue(errors.Count == 0);
        }


        [TestMethod]
        public void CanParseBinaryOperator()
        {
            const double EXPECTED_VALUE = 9;
            string equation = "4 + 5";
            IReadOnlyList<Token> tokenStream = _lexicalAnalyzer.Analyse(equation);

            IParseNode root = _semanticAnalyzer.Parse(tokenStream, out List<SemanticError> errors);

            double result = (double)root.Evaluate();
            Trace.WriteLine($"{root.ToString()}={result}");
            Assert.AreEqual(EXPECTED_VALUE, result);
            Assert.IsTrue(errors.Count == 0);
        }


        [TestMethod]
        public void CanParseUnaryOperator()
        {
            const double EXPECTED_VALUE = -5;
            string equation = "- 5";
            IReadOnlyList<Token> tokenStream = _lexicalAnalyzer.Analyse(equation);

            IParseNode root = _semanticAnalyzer.Parse(tokenStream, out List<SemanticError> errors);

            double result = (double)root.Evaluate();
            Trace.WriteLine($"{root.ToString()}={result}");
            Assert.AreEqual(EXPECTED_VALUE, result);
            Assert.IsTrue(errors.Count == 0);
        }

        [TestMethod]
        public void CanParseParentheses()
        {
            const double EXPECTED_VALUE = -10;
            string equation = "4 - (5 + 9)";
            IReadOnlyList<Token> tokenStream = _lexicalAnalyzer.Analyse(equation);

            IParseNode root = _semanticAnalyzer.Parse(tokenStream, out List<SemanticError> errors);

            double result = (double)root.Evaluate();
            Trace.WriteLine($"{root.ToString()}={result}");
            Assert.AreEqual(EXPECTED_VALUE, result);
            Assert.IsTrue(errors.Count == 0);
        }

        [TestMethod]
        public void PrioriseUnaryOperatorOverBinaryOperator()
        {
            const double EXPECTED_VALUE = 1;
            string equation = "-5 + 6";
            IReadOnlyList<Token> tokenStream = _lexicalAnalyzer.Analyse(equation);

            IParseNode root = _semanticAnalyzer.Parse(tokenStream, out List<SemanticError> errors);

            double result = (double)root.Evaluate();
            Trace.WriteLine($"{root.ToString()}={result}");
            Assert.AreEqual(EXPECTED_VALUE, result);
            Assert.IsTrue(errors.Count == 0);
        }

        [TestMethod]
        public void CanParseFunction()
        {
            const double EXPECTED_VALUE = 2;
            string equation = "max(1,2)";
            IReadOnlyList<Token> tokenStream = _lexicalAnalyzer.Analyse(equation);

            IParseNode root = _semanticAnalyzer.Parse(tokenStream, out List<SemanticError> errors);

            double result = (double)root.Evaluate();
            Trace.WriteLine($"{root.ToString()}={result}");
            Assert.AreEqual(EXPECTED_VALUE, result);
            Assert.IsTrue(errors.Count == 0);
        }

        [TestMethod]
        public void BinaryOperatorUsesPriority()
        {
            const double EXPECTED_VALUE = 2;
            string equation = "1-2+3";
            IReadOnlyList<Token> tokenStream = _lexicalAnalyzer.Analyse(equation);

            IParseNode root = _semanticAnalyzer.Parse(tokenStream, out List<SemanticError> errors);

            double result = (double)root.Evaluate();
            Trace.WriteLine($"{root.ToString()}={result}");
            Assert.AreEqual(EXPECTED_VALUE, result);
            Assert.IsTrue(errors.Count == 0);
        }
    }
}
