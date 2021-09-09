using System;

namespace CsConsole
{
    public class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string dsec)
        { Description = dsec; }

        public string Description { get; }
    }
}