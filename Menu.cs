using System;

/*
    Menu system
*/

namespace fwod
{
    internal class Menu
    {
        const int MENU_WIDTH = 60;
        const int MENU_STARTTOP = 2;
        const string MENU_SEPERATOR = "--";

        static string[] MenuItems = new string[]
        {
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

                // Get the item if..
                string item = (MenuItem == MENU_SEPERATOR ?
                    // ..it's a MENU_SEPERATOR item
                    Game.Graphics.Lines.SingleConnector[3] + new string(Game.Graphics.Lines.Single[1], MENU_WIDTH - 2) + Game.Graphics.Lines.SingleConnector[0] :
                    // ..or just a regular item
                    Game.Graphics.Lines.Single[0] + ConsoleTools.CenterString(MenuItem, MENU_WIDTH - 2) + Game.Graphics.Lines.Single[0]);

                // Print item
                ConsoleTools.WriteAndCenter(Core.Layer.Menu, item, MenuIndex);
            }
            ConsoleTools.WriteAndCenter(Core.Layer.Menu, bottom, MenuIndex + 1);

            // While in menu, do actions
            do
            {
                Entry();
            } while (inMenu);

            // Clear menu and reprint layer underneath
            ClearMenu();
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
                    inMenu = false;
                    break;
            }
        }

        static void NextControl()
        {
            // If index at Lenght of array...
            

            // Else increase index only if not MENU_SEP


            // Update screen

        }

        static void PreviousControl()
        {

        }

        static void Select()
        {
            // swtich(MenuIndex) [...]

        }

        static void UpdateMenuOnScreen()
        {
            // Place old text back


            // Render new text

        }

        /// <summary>
        /// Clears the menu and places things back on screen.
        /// </summary>
        static void ClearMenu()
        {
            int startY = MENU_STARTTOP;
            int startX = (ConsoleTools.BufferWidth / 2) - (MENU_WIDTH / 2);
            int lengthY = MenuItems.Length + 4; // don't ask about the +4 like idk mang
            int gamelayer = (int)Core.Layer.Game;

            for (int row = startY; row < lengthY; row++)
            {
                for (int col = startX; col < ConsoleTools.BufferWidth; col++)
                {
                    Console.SetCursorPosition(col, row);
                    Console.Write(Core.Layers[gamelayer][row, col]);
                }
            }

            // Place enemies and player back on screen
            foreach (Player enemy in Game.EnemyList)
            {
                enemy.Initialize();
            }

            MainClass.MainPlayer.Initialize();
        }
    }
}