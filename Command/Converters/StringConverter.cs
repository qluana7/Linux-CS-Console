namespace CsConsole
{
    public class StringConverter : IConverter<string>
    {
        public string Convert(string value)
            => value;
    }
}