namespace Tony.Calculator.Definitions
{
    public class VariableDefinition
    {
        public string Name { get; }
        public object Value { get; }
        public VariableDefinition(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Name} : {Value}";
        }
    }
}
