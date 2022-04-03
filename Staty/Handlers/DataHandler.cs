using System;
using Staty.Data;

namespace Staty.Handlers
{
    public class DataHandler : IDataHandler
    {
        public TableDataModel DataModel { get; set; }

        /// <summary>
        /// S uchování předchozího směru seřazení
        /// </summary>
        public void OrderStatesBy(Func<State, object> selector)
            => DataModel.OrderBy(selector);

        public void OrderStatesByDesc(Func<State, object> selector)
            => DataModel.OrderByDesc(selector);
    }
}