namespace Tony.CalculatorLib.Definitions
{
    public class UnaryOperatorDefinition
    {
        public string Symbol { get; }
        public Func<object, object> Func { get; }
        public string DisplayName { get; init; }
        public UnaryOperatorDefinition(string symbol, Func<object, object> func)
        {
            Symbol = symbol;
            Func = func;
        }

        public override string ToString()
        {
            return $"{Symbol} ({DisplayName})";
        }
    }
}
