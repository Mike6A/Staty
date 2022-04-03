using System;
using System.Reflection;

namespace Staty.Data
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ConsoleAlignmentAttribute : Attribute
    {
        public static readonly int DefaultAlignment = 0;

        public int Alignment { get; set; }

        public ConsoleAlignmentAttribute(int alignment)
        {
            Alignment = alignment;
        }

        public static ConsoleAlignmentAttribute GetAttribute(PropertyInfo propertyInfo) => 
            propertyInfo.GetCustomAttribute<ConsoleAlignmentAttribute>(false);

        public static int GetAlignment(PropertyInfo propertyInfo) => 
            GetAttribute(propertyInfo)?.Alignment ?? DefaultAlignment;
    }
}