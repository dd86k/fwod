using System;

/*
    Menu system
*/

namespace fwod
{
    internal class Menu
    {
        const int MENU_WIDTH = 60;
        const int MENU_STARTTOP = 4;
        const string MENU_SEPERATOR = "--";
        static readonly string[] MenuItems =
        {
            "Return",
            MENU_SEPERATOR,
            "Save",
            "Load",
            MENU_SEPERATOR,
            "Quit"
        };

        static bool inMenu = true;
        static int PastMenuIndex = 0, MenuIndex = 0;

        static internal void Show()
        {
            // Generate and print the menu
            string top = Game.Graphics.Lines.SingleCorner[0] + ConsoleTools.RepeatChar(Game.Graphics.Lines.Single[1], MENU_WIDTH - 2) + Game.Graphics.Lines.SingleCorner[1];
            string bottom = Game.Graphics.Lines.SingleCorner[3] + ConsoleTools.RepeatChar(Game.Graphics.Lines.Single[1], MENU_WIDTH - 2) + Game.Graphics.Lines.SingleCorner[2];

            // Generate menu
            ConsoleTools.WriteAndCenter(Core.Layer.Menu, top, MENU_STARTTOP - 1);
            for (int i = 0; i < MenuItems.Length; i++)
            {
                // Get the item if..
                string item = (MenuItems[i] == MENU_SEPERATOR ?
                    // ..it's a MENU_SEPERATOR item
                    Game.Graphics.Lines.SingleConnector[3] + new string(Game.Graphics.Lines.Single[1], MENU_WIDTH - 2) + Game.Graphics.Lines.SingleConnector[0] :
                    // ..or just a regular item
                    Game.Graphics.Lines.Single[0] + ConsoleTools.CenterString(MenuItems[i], MENU_WIDTH - 2) + Game.Graphics.Lines.Single[0]);

                // Print item
                ConsoleTools.WriteAndCenter(Core.Layer.Menu, item, MENU_STARTTOP + i);
            }
            ConsoleTools.WriteAndCenter(Core.Layer.Menu, bottom, MENU_STARTTOP + MenuItems.Length);

            // "Select" item
            UpdateMenuOnScreen();

            // While in menu, do actions
            do
            {
                Entry();
            } while (inMenu);

            // Clear menu and reprint layer underneath
            ClearMenu();

            // Returning to game!
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

        /// <summary>
        /// Goes to the next control
        /// </summary>
        static void NextControl()
        {
            bool found = false;
            PastMenuIndex = MenuIndex;

            // Find a good index
            while (!found)
            {
                MenuIndex++;

                if (MenuIndex >= MenuItems.Length)
                {
                    MenuIndex = 0;
                    found = true;
                }

                if (MenuItems[MenuIndex] != MENU_SEPERATOR)
                    found = true;
            }

            // Update screen
            UpdateMenuOnScreen();
        }

        /// <summary>
        /// Goes to the previous control
        /// </summary>
        static void PreviousControl()
        {
            bool found = false;
            PastMenuIndex = MenuIndex;

            // Find a good index
            while (!found)
            {
                MenuIndex--;

                if (MenuIndex < 0)
                {
                    MenuIndex = MenuItems.Length - 1;
                    found = true;
                }

                if (MenuItems[MenuIndex] != MENU_SEPERATOR)
                    found = true;
            }

            // Update screen
            UpdateMenuOnScreen();
        }

        /// <summary>
        /// Selects the 
        /// </summary>
        static void Select()
        {
            switch (MenuIndex)
            { //UNDONE: switch (MenuIndex)
                // Return
                case 0:
                    inMenu = false;
                    break;
                // Quit
                case 5:

                    break;
            }
        }

        /// <summary>
        /// Update menu on screen
        /// </summary>
        static void UpdateMenuOnScreen()
        {
            // Get coords
            int MenuItemTop = MENU_STARTTOP + MenuIndex;
            int MenuItemTopPast = MENU_STARTTOP + PastMenuIndex;
            int MenuItemLeft = (ConsoleTools.BufferWidth / 2) - (MENU_WIDTH / 2);

            // Remove old item's style
            Console.SetCursorPosition(MenuItemLeft + 1, MenuItemTopPast);
            Console.Write(ConsoleTools.CenterString(MenuItems[PastMenuIndex], MENU_WIDTH - 2));

            // Apply colors
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            // Apply new item's style
            Console.SetCursorPosition(MenuItemLeft + 1, MenuItemTop);
            Console.Write(ConsoleTools.CenterString(MenuItems[MenuIndex], MENU_WIDTH - 2));

            // Revert to original colors
            Console.ForegroundColor = ConsoleTools.OriginalForegroundColor;
            Console.BackgroundColor = ConsoleTools.OriginalBackgroundColor;
        }

        /// <summary>
        /// Clears the menu and places things back on screen.
        /// </summary>
        static void ClearMenu()
        {
            int startY = MENU_STARTTOP - 1;
            int startX = (ConsoleTools.BufferWidth / 2) - (MENU_WIDTH / 2);
            int lengthY = MenuItems.Length + 5; // don't ask about the +4 like idk mang
            int gamelayer = (int)Core.Layer.Game;

            for (int row = startY; row < lengthY; row++)
            {
                for (int col = startX; col < ConsoleTools.BufferWidth; col++)
                {
                    Console.SetCursorPosition(col, row); // Safety measure
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