namespace Tony.Calculator.Definitions
{
    public class UnaryOperatorDefinition
    {
        public string Symbol { get; }
        public Func<object, object> Func { get; }
        public UnaryOperatorDefinition(string symbol, Func<object, object> func)
        {
            Symbol = symbol;
            Func = func;
        }
    }
}
