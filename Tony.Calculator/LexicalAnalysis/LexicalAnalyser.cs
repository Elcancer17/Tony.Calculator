using System.Text.RegularExpressions;

namespace Tony.Calculator.LexicalAnalysis
{
    public class LexicalAnalyser
    {
        public static TokenRule[] Rules = [
            new TokenRule(TokenTypes.Number, @"[0-9](?:[0-9_]*[0-9])?(?:\.[0-9](?:[0-9_]*[0-9])?)?"),
            new TokenRule(TokenTypes.Identifier, @"[A-Za-z][A-Za-z0-9_]*"),
            new TokenRule(TokenTypes.Operator, @"([^A-Za-z0-9\s()\.,])\1{0,2}"),
            new TokenRule(TokenTypes.L_Parenthesis, @"\("),
            new TokenRule(TokenTypes.R_Parenthesis, @"\)"),
            new TokenRule(TokenTypes.Colon, @","),
            new TokenRule(TokenTypes.Whitespace, @"\s+"),
            new TokenRule(TokenTypes.Unknown, @"."),
        ];

        public IReadOnlyList<Token> Analyse(string equation)
        {
            ReadOnlyMemory<char> memory = equation.AsMemory();
            List<Token> tokens = new List<Token>();

            int index = 0;
            while(index < memory.Length)
            {
                for (int i = 0; i < Rules.Length; i++)
                {
                    TokenRule rule = Rules[i];
                    Match result = rule.Regex.Match(equation, index);
                    if(result.Success && result.Index == index)
                    {
                        Token newToken = new Token()
                        {
                            TextIndex = index,
                            TokenIndex = tokens.Count,
                            Text = memory.Slice(index, result.Length),
                            Type = rule.Type
                        };
                        index += result.Length;
                        if (rule.Type != TokenTypes.Whitespace)
                        {
                            tokens.Add(newToken);
                        }
                        break;
                    }
                }
            }

            return tokens;
        }
    }
}
