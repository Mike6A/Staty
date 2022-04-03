using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Staty.Filters
{ 
    public class StateFilter : BaseFilter
    {
        public string Name { get; set; }
        public string Continent { get; set; }
        public string Shortcut { get; set; }

        private static readonly IEnumerable<PropertyInfo> AllStringProperties =
            typeof(StateFilter)
                .GetProperties()
                .Where(p => p.PropertyType == typeof(string));

        public void MakePropertiesUpperCase() => 
            AllStringProperties
                .Where(x => x.GetValue(this) != null)
                .ToList()
                .ForEach(x => x.SetValue(this, ((string)x.GetValue(this)).ToUpper()));
    }
}