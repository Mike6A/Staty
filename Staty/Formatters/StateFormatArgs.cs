namespace Staty.Formatters
{
    public class StateFormatArgs
    {
        public StateFormatArgs(string value, int alignment)
        {
            Value = value;
            Alignment = alignment;
        }

        public int Alignment { get; set; }
        public string Value { get; set; }
    }
}