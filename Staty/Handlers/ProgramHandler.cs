using System;
using Staty.Data;
using Staty.Services;
using Staty.Utils;

namespace Staty.Handlers
{
    public class ProgramHandler : IProgramHandler
    {
        private readonly IDataHandler _dataHandler;
        private readonly IPager _pager;
        private readonly IConsoleHandler _consoleHandler;

        public ProgramHandler(IStateService stateService, 
            IDataHandler dh, 
            IPager pager, 
            IConsoleHandler consoleHandler)
        {
            _dataHandler = dh;
            _pager = pager;
            _consoleHandler = consoleHandler;

            _dataHandler.DataModel = stateService.GetAll(false);
        }

        /// <summary>
        /// S uchování předchozího směru seřazení
        /// </summary>
        public void OrderStatesBy(Func<State, object> selector) => _dataHandler.OrderStatesBy(selector);

        public void NextPage() => _pager.NextPage();

        public void PrevPage() => _pager.PrevPage();

        public void PrintDataModel() => _pager.PrintDataModel();

        public void SetItemPerPage() => _consoleHandler.SetItemsPerPage();

        public void FilerStates(ConsoleFilterAction action)
        {
            switch (action)
            {
                case ConsoleFilterAction.Reset:
                    _consoleHandler.ResetAllFilters();
                    break;
                case ConsoleFilterAction.ByName:
                    _consoleHandler.NameFilter();
                    break;
                case ConsoleFilterAction.ByContinent:
                    _consoleHandler.ContinentFilter();
                    break;
            }
        }
    }
}