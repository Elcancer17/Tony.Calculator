namespace Tony.Calculator.Definitions
{
    public class BinaryOperatorDefinition
    {
        public string Symbol { get; }
        public int Priority { get; }
        public Func<object, object, object> Func { get; }
        public BinaryOperatorDefinition(string symbol, int priority, Func<object, object, object> func)
        {
            Symbol = symbol;
            Priority = priority;
            Func = func;
        }
    }
}
