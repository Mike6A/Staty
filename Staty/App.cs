using System;
using System.Linq;
using Staty.Data;
using Staty.Handlers;
using Staty.Utils;

namespace Staty
{
    public class App
    {
        private static int WindowWidth => State.AlignNumbers.Sum() * -1 + 1;
        private readonly IProgramHandler _handler;
        private bool _running = true; 

        public App(IProgramHandler handler)
        {
            _handler = handler;
            _handler.OrderStatesBy(s => s.Name);

            Console.SetWindowSize(WindowWidth, 41);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);

            _handler.PrintDataModel();
        }

        public void Run()
        {
            while (_running)
            {
                if (Console.KeyAvailable) 
                    HandleKeyPressed(Console.ReadKey(true).Key);
            } 
        }

        private void HandleKeyPressed(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.PageUp: _handler.NextPage(); break;
                case ConsoleKey.PageDown: _handler.PrevPage(); break;
                case ConsoleKey.Backspace: _running = false; break;
                case ConsoleKey.Escape: _handler.FilerStates(ConsoleFilterAction.Reset); break;
                case ConsoleKey.F1: HelpWriter.Helper(); break;
                case ConsoleKey.F2: _handler.FilerStates(ConsoleFilterAction.ByContinent); break;
                case ConsoleKey.F3: _handler.FilerStates(ConsoleFilterAction.ByName); break;
                case ConsoleKey.F5: _handler.OrderStatesBy(s => s.Name); break;
                case ConsoleKey.F6: _handler.OrderStatesBy(s => s.Area); break;
                case ConsoleKey.F7: _handler.OrderStatesBy(s => s.Population); break;
                case ConsoleKey.F12: _handler.SetItemPerPage(); break;
            }
            _handler.PrintDataModel();
        }
    }
}