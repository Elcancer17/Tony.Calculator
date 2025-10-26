using Tony.Calculator.SemanticAnalysis;
using Tony.Calculator.Definitions;
using Tony.Calculator.Plugins;
using Tony.Calculator.LexicalAnalysis;

namespace Tony.Calculator.Test
{
    [TestClass]
    public sealed class SemanticAnalyserTests
    {
        private SemanticAnalyser _semanticAnalyzer;
        [TestInitialize]
        public void TestInitialize()
        {
            DefinitionCollection definitions = new DefinitionCollection();
            ArithmeticPlugin plugin = new ArithmeticPlugin();
            definitions.AddPlugin(plugin);
            _semanticAnalyzer = new SemanticAnalyser(definitions);
        }

        [TestMethod]
        public void CanParseAddition()
        {
            string equation = "4 + 5";
            IReadOnlyList<Token> tokenStream = LexicalAnalyser.Analyse(equation);

            IParseNode root = _semanticAnalyzer.Parse(tokenStream, out List<SemanticError> errors);

            Assert.AreEqual(root.Print(), equation);
            Assert.IsTrue(errors.Count == 0);
        }
    }
}
