namespace Tony.CalculatorLib.LexicalAnalysis
{
    public struct Token
    {
        public int TokenIndex { get; set; }
        public int TextIndex { get; set; }
        public ReadOnlyMemory<char> Text { get; init; }
        public TokenTypes Type { get; init; }

        public override string ToString()
        {
            return $"{Type} (\"{Text}\")";
        }
    }
}
