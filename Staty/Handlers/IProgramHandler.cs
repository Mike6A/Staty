using System;
using Staty.Data;

namespace Staty.Handlers
{
    public interface IProgramHandler
    {
        /// <summary>
        /// S uchování předchozího směru seřazení
        /// </summary>
        void OrderStatesBy(Func<State, object> selector);

        void NextPage();
        void PrevPage();
        void PrintDataModel();
        void FilerStates(ConsoleFilterAction action);
        void SetItemPerPage();
    }
}