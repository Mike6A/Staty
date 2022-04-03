using System;

namespace Staty.Utils
{
    internal class HelpWriter
    {
        public static void Helper()
        {
            Console.Clear();
            Console.WriteLine("NÁPOVĚDA");
            Console.WriteLine("\nOvládání:");
            Console.WriteLine(
                "F1 - help (nápověda, o programu)" +
                "\nF2 – filtr podle kontinentu"+
                "\nF3 - filtr podle názvu/značky"+
                "\nF5 - řazení podle názvu" +
                "\nF6 - řazení podle rozlohy" +
                "\nF7 - řazení podle počtu obyvatel" +
                "\nF12 - nastavní počtu zobrazovaných řádků" +
                "\n\nPgUp - předchozí stránka" +
                "\nPgDown – další stránka" +
                "\nEsc – zruš všechny filtry a seřaď podle jména státu" +
                "\nBackspace - Ukončí program");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n\nPro návrat na aktuální stránku států zmáčkněte kterékoliv tlačítko");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey(true);
        }
    }
}
