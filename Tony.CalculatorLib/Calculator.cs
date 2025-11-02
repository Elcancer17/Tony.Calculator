using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony.CalculatorLib.Definitions;
using Tony.CalculatorLib.LexicalAnalysis;
using Tony.CalculatorLib.SemanticAnalysis;

namespace Tony.CalculatorLib
{
    public class Calculator
    {
        public DefinitionCollection Definitions { get; }

        private LexicalAnalyser _lexicalAnalyzer;
        private SemanticAnalyser _semanticAnalyzer;

        private string _cachedEquation;
        private IParseNode _cachedRoot;
        public Calculator(DefinitionCollection definitions)
        {
            _lexicalAnalyzer = new LexicalAnalyser();
            _semanticAnalyzer = new SemanticAnalyser(definitions);
            Definitions = definitions;
        }

        private void ComputeAndCacheEquation(string equation)
        {
            if (!string.Equals(_cachedEquation, equation, StringComparison.OrdinalIgnoreCase))
            {
                IReadOnlyList<Token> tokens = _lexicalAnalyzer.Analyse(equation);
                _cachedRoot = _semanticAnalyzer.Parse(tokens, out List<SemanticError> errors);
                _cachedEquation = equation;
            }
        }

        public object Evaluate(string equation)
        {
            ComputeAndCacheEquation(equation);
            object value = _cachedRoot.Evaluate();
            return value;
        }

        public string FormatEquation(string equation)
        {
            ComputeAndCacheEquation(equation);
            return _cachedRoot.ToString();
        }
    }
}
