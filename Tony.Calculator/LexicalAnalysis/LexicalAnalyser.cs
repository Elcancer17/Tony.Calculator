using System.Text.RegularExpressions;

namespace Tony.Calculator.LexicalAnalysis
{
    public static class LexicalAnalyser
    {
        public static TokenRule[] Rules = [
            new TokenRule(TokenTypes.Number, @"[0-9](?:[0-9\s]*[0-9])?(?:\.[0-9\s]*[0-9])?"),
            new TokenRule(TokenTypes.Identifier, @"[A-Za-z][A-Za-z0-9_]*"),
            new TokenRule(TokenTypes.Operator, @"([^A-Za-z0-9\s()\.])\1{0,2}"),
            new TokenRule(TokenTypes.L_Parenthesis, @"\("),
            new TokenRule(TokenTypes.R_Parenthesis, @"\)"),
            new TokenRule(TokenTypes.Colon, @","),
            new TokenRule(TokenTypes.Whitespace, @"\s+"),
            new TokenRule(TokenTypes.Unknown, @"."),
        ];

        public static IReadOnlyList<Token> Analyse(string text)
        {
            ReadOnlyMemory<char> memory = text.AsMemory();
            List<Token> tokens = new List<Token>();

            int index = 0;

            while(index < memory.Length)
            {
                for (int i = 0; i < Rules.Length; i++)
                {
                    Match result = Rules[i].Regex.Match(text, index);
                    if(result.Success && result.Index == index)
                    {
                        tokens.Add(new Token()
                        {
                            Index = index,
                            Text = memory.Slice(index, result.Length), 
                            Type = Rules[i].Type
                        });
                        index += result.Length;
                        break;
                    }
                }
            }

            return tokens;
        }
    }
}
