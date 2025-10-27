using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tony.Calculator.Definitions;
using Tony.Calculator.LexicalAnalysis;
using Tony.Calculator.SemanticAnalysis;
using static System.Net.Mime.MediaTypeNames;

namespace Tony.Calculator
{
    public class Calculator
    {
        public DefinitionCollection Definitions { get; }

        private LexicalAnalyser _lexicalAnalyzer;
        private SemanticAnalyser _semanticAnalyzer;
        public Calculator(DefinitionCollection definitions)
        {
            _lexicalAnalyzer = new LexicalAnalyser();
            _semanticAnalyzer = new SemanticAnalyser(definitions);
            Definitions = definitions;
        }

        public object Evaluate(string equation)
        {
            IReadOnlyList<Token> tokens = _lexicalAnalyzer.Analyse(equation);
            IParseNode root = _semanticAnalyzer.Parse(tokens, out List<SemanticError> errors);
            object value = root.Evaluate();
            return value;
        }
    }
}
