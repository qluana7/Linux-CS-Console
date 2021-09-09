using System;

namespace CsConsole
{
    public interface IConverter
    { }

    public interface IConverter<T> : IConverter
    {
        T Convert(string value);
    }
}