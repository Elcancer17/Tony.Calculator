namespace Tony.Calculator.Definitions
{
    public class BinaryOperatorDefinition
    {
        public string Symbol { get; }
        public string Name { get; }
        public int Priority { get; }
        public Func<object, object, object> Func { get; }
        public BinaryOperatorDefinition(string symbol, string name, int priority, Func<object, object, object> func)
        {
            Symbol = symbol;
            Name = name;
            Priority = priority;
            Func = func;
        }

        public override string ToString()
        {
            return $"{Symbol} ({Name})";
        }
    }
}
