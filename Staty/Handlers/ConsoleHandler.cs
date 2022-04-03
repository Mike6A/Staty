using System;
using System.Linq;
using Staty.Filters;
using Staty.Services;
using Staty.Utils;

namespace Staty.Handlers
{
    public class ConsoleHandler : IConsoleHandler
    {
        private readonly IPager _pager;
        private readonly IDataHandler _dataHandler;
        private readonly IStateService _stateService;

        public ConsoleHandler(IPager pager, IDataHandler dataHandler, IStateService stateService)
        {
            _pager = pager;
            _dataHandler = dataHandler;
            _stateService = stateService;
        }

        public void ContinentFilter()
        {
            const string consoleMessage = "Zadejte název kontinentu, podle kterého chcete filtrovat: ";

            OuterFilterFunction(input =>
            {
                _dataHandler.DataModel = _stateService.Filer(new StateFilter() { Continent = input });
            }, consoleMessage);
        }

        public void NameFilter()
        {
            const string consoleMessage = "Zadejte náezv názvu státu/zkratku, podle kterých chcete filtrovat: ";

            OuterFilterFunction(input =>
            {
                var model = _stateService.Filer(new StateFilter() { Name = input });

                if (input.Length <= 2)
                    model.States.AddRange(_stateService.Filer(new StateFilter() { Shortcut = input }).States);

                model.States = model.States.Distinct().ToList();
                _dataHandler.DataModel = model;
            }, consoleMessage);
        }

        private void OuterFilterFunction(Action<string> filterAction, string consoleMessage)
        {
            Console.Write(consoleMessage);

            var input = string.Empty;
            while (InnerFilterHandleKey(ref input)) 
            {
                filterAction.Invoke(input);

                AfterApplyFilter();
                Console.Write(consoleMessage + input);
            }
        }

        /// <returns>True - Pokračovat ve filtrování, False - Ukončit filtrování</returns>
        private bool InnerFilterHandleKey(ref string input)
        {
            var keyPressed = Console.ReadKey().Key;
            switch (keyPressed)
            {
                case ConsoleKey.Enter:
                case ConsoleKey.Backspace when input.Length == 0:
                    return false;

                case ConsoleKey.Backspace:
                    input = input.Remove(input.Length - 1);
                    return true;

                case ConsoleKey.Escape:
                    ResetAllFilters();
                    return false;

                default:
                {
                    if (char.IsLetter((char)keyPressed))
                        input += keyPressed;
                    return true;
                }
            }
        }

        public void ResetAllFilters()
        {
            _dataHandler.DataModel = _stateService.Filer(new StateFilter());
            AfterApplyFilter();
        }

        public void SetItemsPerPage()
        {
            Console.Write("Nastavit počet řádků na (5-50): ");
            var input = Console.ReadLine();
            var res = int.TryParse(input, out var value);
            if (!res || value < 5 || value > 50)
                return;

            Console.SetWindowSize(Console.WindowWidth, value + UiHelper.InfoRowsCount);
            Console.SetBufferSize(Console.WindowWidth, value + UiHelper.InfoRowsCount);
        }

        private void AfterApplyFilter()
        {
            _dataHandler.OrderStatesByDesc(s => s.Name);
            _pager.SetPage(0);
        }
    }
}
