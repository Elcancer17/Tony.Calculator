namespace Tony.CalculatorLib.Definitions
{
    public class VariableDefinition
    {
        public string Identifier { get; }
        public object Value { get; }
        public VariableDefinition(string identifier, object value)
        {
            Identifier = identifier;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Identifier} : {Value}";
        }
    }
}
