using System;
using System.Linq;
using Staty.Data;

namespace Staty.Utils
{
    public class UiHelper
    {
        public static int InfoRowsCount = 5;

        private static void WriteHeader(TableDataModel dataModel)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("STATY");
            Console.BackgroundColor = ConsoleColor.DarkBlue;

            Console.WriteLine(dataModel.GetDescriptionLine());

            Console.BackgroundColor = ConsoleColor.Black;
        }

        private static void WriteFooter(Pager pager, TableDataModel dataModel)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("F1 - Nápověda                   PgUp - Další stránka         PgDown - Předchozí stránka                               Backspace - Ukončit program ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;

            var shownRecordLowerLimit = dataModel.States.Count <= 0 ? pager.SkipItemsCount : pager.SkipItemsCount + 1;

            Console.WriteLine("Nalezeno {0} záznamů, zobrazeny záznamy {1} - {2}, strana {3} z {4}", 
                dataModel.States.Count, 
                shownRecordLowerLimit, 
                pager.SkipItemsCount + pager.ItemsPerPage, 
                pager.ActivePageIndex+1, 
                pager.MaxPageIndex+1);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void PrintDataModel(Pager pager, TableDataModel dataModel)
        {
            WriteHeader(dataModel);

            var s = dataModel.States.Skip(pager.SkipItemsCount).Take(pager.ItemsPerPage).ToList();
            s.ForEach(Console.WriteLine);

            WriteFooter(pager, dataModel);
        }
    }
}
