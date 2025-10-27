namespace Tony.Calculator.Definitions
{
    public class UnaryOperatorDefinition
    {
        public string Symbol { get; }
        public string Name { get; }
        public Func<object, object> Func { get; }
        public UnaryOperatorDefinition(string symbol, string name, Func<object, object> func)
        {
            Symbol = symbol;
            Name = name;
            Func = func;
        }

        public override string ToString()
        {
            return $"{Symbol} ({Name})";
        }
    }
}
