using System;

namespace Staty.Formatters
{
    public class StateFormatter : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            return formatType == typeof(ICustomFormatter) ? this : null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            if (arg.GetType() != typeof(StateFormatArgs))
                return string.Empty;

            var stateArgs = (StateFormatArgs)arg;
            return string.Format("{0," + stateArgs.Alignment + "}", stateArgs.Value);
        }

    }
}