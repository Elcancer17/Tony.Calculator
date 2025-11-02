namespace Tony.CalculatorLib.Definitions
{
    public class BinaryOperatorDefinition
    {
        public string Symbol { get; }
        public int Priority { get; }
        public Func<object, object, object> Func { get; }
        public string DisplayName { get; init; }
        public BinaryOperatorDefinition(string symbol,int priority, Func<object, object, object> func)
        {
            Symbol = symbol;
            Priority = priority;
            Func = func;
        }

        public override string ToString()
        {
            return $"{Symbol} ({DisplayName})";
        }
    }
}
