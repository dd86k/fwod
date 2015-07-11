using System;

namespace fwod
{
    internal class Menu
    {
        const int MENU_WIDTH = 60;
        const int MENU_STARTTOP = 2;
        const string MENU_SEPERATOR = "-";

        static string[] MenuItems = new string[]
        { // "-" is a separator
            "Return",
            MENU_SEPERATOR,
            "Save",
            "Load",
            MENU_SEPERATOR,
            "Quit"
        };

        static bool inMenu = true;
        static int MenuIndex = 0;

        static internal void Show()
        {
            //UNDONE: Menu.Show()

            // Generate and print the menu
            int MenuIndex = MENU_STARTTOP;
            string top = Game.Graphics.Lines.SingleCorner[0] + ConsoleTools.RepeatChar(Game.Graphics.Lines.Single[1], MENU_WIDTH - 2) + Game.Graphics.Lines.SingleCorner[1];
            string bottom = Game.Graphics.Lines.SingleCorner[3] + ConsoleTools.RepeatChar(Game.Graphics.Lines.Single[1], MENU_WIDTH - 2) + Game.Graphics.Lines.SingleCorner[2];

            ConsoleTools.WriteAndCenter(Core.Layer.Menu, top, MenuIndex);
            foreach (string MenuItem in MenuItems)
            {
                MenuIndex++;

                string item = (MenuItem == MENU_SEPERATOR ?
                    // MENU_SEPERATOR item
                    Game.Graphics.Lines.SingleConnector[3] + new string(Game.Graphics.Lines.Single[1], MENU_WIDTH - 2) + Game.Graphics.Lines.SingleConnector[0] :
                    // Regular item
                    Game.Graphics.Lines.Single[0] + ConsoleTools.CenterString(MenuItem, MENU_WIDTH - 2) + Game.Graphics.Lines.Single[0]);

                ConsoleTools.WriteAndCenter(Core.Layer.Menu, item, MenuIndex);
            }
            ConsoleTools.WriteAndCenter(Core.Layer.Menu, bottom, MenuIndex + 1);

            // While in menu, do actions
            do
            {
                Entry();
            } while (inMenu);

            // Clear menu and reprint layer underneath

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

                // Select an item
                case ConsoleKey.Spacebar:
                case ConsoleKey.Enter:
                    Select();
                    break;

                // Quitting menu
                case ConsoleKey.Escape:
                    ClearMenu();
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

        static void ClearMenu()
        {
            int startY = MENU_STARTTOP;
            int startX = (ConsoleTools.BufferWidth / 2) - (MENU_WIDTH / 2);
            int lengthY = MenuItems.Length + 4; // don't ask about the +4 like idk mang
            for (int row = startY; row < lengthY; row++)
            {
                for (int col = startX; col < ConsoleTools.BufferWidth; col++)
                {
                    Console.SetCursorPosition(col, row);
                    Console.Write(Core.Layers[(int)Core.Layer.Game][row, col]);
                    Console.SetCursorPosition(col, row);
                    Console.Write(Core.Layers[(int)Core.Layer.Player][row, col]);
                }
            }
        }
    }
}