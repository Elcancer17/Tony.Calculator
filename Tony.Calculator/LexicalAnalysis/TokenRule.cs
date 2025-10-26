using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace Tony.Calculator.LexicalAnalysis
{
    public struct TokenRule
    {
        public TokenTypes Type;
        public string RegexPattern;
        public Regex Regex;
        public TokenRule(TokenTypes type, [StringSyntax(StringSyntaxAttribute.Regex)] string regexPattern)
        {
            Type = type;
            RegexPattern = regexPattern;
            Regex = new Regex(RegexPattern, RegexOptions.Compiled, TimeSpan.FromSeconds(1));
        }

        public override string ToString()
        {
            return $"{Type} (\"{RegexPattern}\")";
        }
    }
}
