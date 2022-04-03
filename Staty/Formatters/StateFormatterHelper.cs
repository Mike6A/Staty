using System;
using System.Reflection;
using System.Text;
using Staty.Data;

namespace Staty.Formatters
{
    public class StateFormatterHelper
    {
        public static string BuildLine(Func<PropertyInfo, string> useValue)
        {
            var sb = new StringBuilder();
            foreach (var p in State.AllProperties)
            {
                var val = useValue.Invoke(p);
                sb.Append(LineFormat(val, p));
            }
            return sb.ToString();
        }

        private static string LineFormat(string value, PropertyInfo info)
        {
            return string.Format(new StateFormatter(), 
                "{0}", 
                new StateFormatArgs(value, ConsoleAlignmentAttribute.GetAlignment(info)));
        }
    }
}