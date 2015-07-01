using System;

namespace fwod
{
    internal class Menu
    {
        string[] MenuItems = new string[]
        { // "-" is a separator
            "Return",
            "-",
            "Save",
            "Load",
            "-",
            "Quit"
        };

        const int MenuItemWidth = 50;
        static bool inMenu = true;

        static internal void Show()
        {
            //UNDONE: Menu.Show()

            // Print the menu

            // While in menu, do actions
            do
            {
                Entry();
            } while (inMenu);

            // Clear menu

            // Reprint layer underneath
        }

        static internal void Entry()
        {
            ConsoleKeyInfo cki = Console.ReadKey(true);

            switch (cki.Key)
            {
                case ConsoleKey.UpArrow:
                    PreviousControl();
                    break;
                case ConsoleKey.DownArrow:
                    NextControl();
                    break;
                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    Select();
                    break;
                case ConsoleKey.Escape:
                    inMenu = false;
                    break;
            }
        }

        static internal void NextControl()
        {

        }

        static internal void PreviousControl()
        {

        }

        static void Select()
        {

        }
    }
}