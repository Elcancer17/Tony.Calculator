namespace Tony.Calculator.LexicalAnalysis
{
    public struct Token
    {
        public int Index {  get; init; }
        public ReadOnlyMemory<char> Text {  get; init; }
        public TokenTypes Type { get; init; }

        public override string ToString()
        {
            return $"{Type} (\"{Text}\")";
        }
    }
}
