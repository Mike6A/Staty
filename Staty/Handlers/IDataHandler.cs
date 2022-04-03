using System;
using System.Linq.Expressions;
using Staty.Data;

namespace Staty.Handlers
{
    public interface IDataHandler
    {
        TableDataModel DataModel { get; set; }

        /// <summary>
        /// S uchování předchozího směru seřazení
        /// </summary>
        void OrderStatesBy(Expression<Func<State, object>> selector);
        void OrderStatesByDesc(Func<State, object> selector);
    }
}