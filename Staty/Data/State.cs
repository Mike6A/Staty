using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Staty.Formatters;

namespace Staty.Data
{
    public class State
    {
        [ConsoleAlignment(-32)]
        public string Name { get; set; }

        [ConsoleAlignment(-20)]
        public string Continent { get; set; }

        [ConsoleAlignment(-9)]
        public string Shortcut { get; set; }

        [ConsoleAlignment(-26)]
        public string StateSystem { get; set; }

        [ConsoleAlignment(-31)]
        public string Capital { get; set; }

        [ConsoleAlignment(-15)]
        public uint Population { get; set; }

        [ConsoleAlignment(-13)]
        public int Area { get; set; }

        public static readonly IEnumerable<PropertyInfo> AllProperties =
            typeof(State)
                .GetProperties()
                .Where(p => p.IsDefined(typeof(ConsoleAlignmentAttribute), false));

        public static readonly IList<int> AlignNumbers = 
            AllProperties
                .Select(ConsoleAlignmentAttribute.GetAlignment) 
                .ToList();

        public override string ToString() => 
            StateFormatterHelper.BuildLine((p) => p.GetValue(this).ToString());

        public static State Empty => new State()
        {
            Name = string.Empty,
            Continent = string.Empty,
            Shortcut = string.Empty,
            StateSystem = string.Empty,
            Capital = string.Empty,
            Population = 0,
            Area = 0
        };
    }
}
