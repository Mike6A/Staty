using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Staty.Formatters;

namespace Staty.Data
{
    public class TableDataModel
    {
        public List<State> States { get; set; } = new List<State>();

        private (Func<State, object> selector, bool? desc) _prevOrder = (null, null);
        private readonly Dictionary<PropertyInfo, string> _descriptions = new Dictionary<PropertyInfo, string>();

        public void SetDescriptionFor(string propertyName, string description)
        {
            var info = typeof(State).GetProperty(propertyName);
            if (info == null)
                return;

            if (_descriptions.ContainsKey(info))
                _descriptions[info] = description;
            else
                _descriptions.Add(info, description);
        }

        public string GetDescriptionLine() => 
            StateFormatterHelper.BuildLine((p) => _descriptions[p]);

        /// <summary>
        /// S uchování předchozího směru seřazení
        /// </summary>
        public void OrderBy(Func<State, object> selector) => 
            OrderBy(selector, !_prevOrder.desc ?? true);

        public void OrderByDesc(Func<State, object> selector) => 
            States = States.OrderBy(selector).ToList();

        public TableDataModel Clone() => 
            (TableDataModel)MemberwiseClone();

        private void OrderBy(Func<State, object> selector, bool desc)
        {
            States = desc ? States.OrderByDescending(selector).ToList() : States.OrderBy(selector).ToList();
            _prevOrder = (selector, desc);
        }

        public static TableDataModel Empty = new TableDataModel();
    }
}