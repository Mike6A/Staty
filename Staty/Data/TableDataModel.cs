using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Staty.Formatters;

namespace Staty.Data
{
    public class TableDataModel
    {
        public List<State> States { get; set; } = new List<State>();

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
        public void OrderBy(Expression<Func<State, object>> selector)
        {
            var res = _prevOrder.SetPrev(selector);
            OrderBy(selector.Compile(), res);
        }

        public void OrderByDesc(Func<State, object> selector) =>
            States = States.OrderBy(selector).ToList();

        public TableDataModel Clone() => 
            (TableDataModel)MemberwiseClone();

        private void OrderBy(Func<State, object> selector, bool desc)
        {
            States = desc ? States.OrderByDescending(selector).ToList() : States.OrderBy(selector).ToList();
        }

        public static TableDataModel Empty = new TableDataModel();
        private readonly SavedPrevOrder _prevOrder = new SavedPrevOrder();

        private class SavedPrevOrder
        {
            private static bool DefaultPreferredOrder(Type t) => t != typeof(string);

            private bool _desc;
            private MemberExpression _prevMemberExpression;

            /// <returns>Nadcházející seřazení</returns>
            public bool SetPrev(Expression<Func<State, object>> expr)
            {
                if (!(expr.Body is MemberExpression member))
                {
                    var body = (UnaryExpression)expr.Body;
                    member = body.Operand as MemberExpression; 
                }

                if (member == null)
                    return true;

                var res = DefaultPreferredOrder(member.Type);
                if (member.Member.Name == _prevMemberExpression?.Member.Name)
                    res = _desc;

                _prevMemberExpression = member;
                _desc = !res;

                return _desc;
            }
        }
    }
}