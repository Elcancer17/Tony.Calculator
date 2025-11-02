using Tony.CalculatorLib.SemanticAnalysis;
using Tony.CalculatorLib.Definitions;
using Tony.CalculatorLib.Plugins;
using Tony.CalculatorLib.LexicalAnalysis;
using System.Diagnostics;

namespace Tony.CalculatorLib.Test
{
    [TestClass]
    public sealed class SemanticAnalyserFormatingTests
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
        public void RemovesRootParentheses()
        {
            const string EXPECTED_VALUE = "1";
            string equation = "((1))";
            IReadOnlyList<Token> tokenStream = _lexicalAnalyzer.Analyse(equation);
            IParseNode root = _semanticAnalyzer.Parse(tokenStream, out List<SemanticError> errors);

            root = _semanticAnalyzer.OptimiseSyntaxTree(root);

            string result = root.ToString();
            Trace.WriteLine($"{equation} = {result}");
            Assert.AreEqual(EXPECTED_VALUE, result);
            Assert.IsTrue(errors.Count == 0);
        }

        [TestMethod]
        public void RemovesPriorityIslandParentheses()
        {
            const string EXPECTED_VALUE = "1 + 2 * 3 - 4";
            string equation = "1 + (2 * 3) - 4";
            IReadOnlyList<Token> tokenStream = _lexicalAnalyzer.Analyse(equation);
            IParseNode root = _semanticAnalyzer.Parse(tokenStream, out List<SemanticError> errors);

            root = _semanticAnalyzer.OptimiseSyntaxTree(root);

            string result = root.ToString();
            Trace.WriteLine($"{equation} = {result}");
            Assert.AreEqual(EXPECTED_VALUE, result);
            Assert.IsTrue(errors.Count == 0);
        }

        [TestMethod]
        public void DoesntRemoveFakePriorityIslandParentheses()
        {
            const string EXPECTED_VALUE = "1 + (2 * 3) * 4";
            string equation = "1 + (2 * 3) * 4";
            IReadOnlyList<Token> tokenStream = _lexicalAnalyzer.Analyse(equation);
            IParseNode root = _semanticAnalyzer.Parse(tokenStream, out List<SemanticError> errors);

            root = _semanticAnalyzer.OptimiseSyntaxTree(root);

            string result = root.ToString();
            Trace.WriteLine($"{equation} = {result}");
            Assert.AreEqual(EXPECTED_VALUE, result);
            Assert.IsTrue(errors.Count == 0);
        }

        [TestMethod]
        public void RemovesParenthesesInsideUnaryOperator()
        {
            const string EXPECTED_VALUE = "-(1 + 2 * 3 - 4)";
            string equation = "-((1 + (2 * 3) - 4))";
            IReadOnlyList<Token> tokenStream = _lexicalAnalyzer.Analyse(equation);
            IParseNode root = _semanticAnalyzer.Parse(tokenStream, out List<SemanticError> errors);

            root = _semanticAnalyzer.OptimiseSyntaxTree(root);

            string result = root.ToString();
            Trace.WriteLine($"{equation} = {result}");
            Assert.AreEqual(EXPECTED_VALUE, result);
            Assert.IsTrue(errors.Count == 0);
        }
    }
}
