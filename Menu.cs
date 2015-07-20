﻿using System;

/*
    Menu system
*/

namespace fwod
{
    internal class Menu
    {
        #region Constants
        /// <summary>
        /// Width of the menu
        /// </summary>
        const int MENU_WIDTH = 40;
        /// <summary>
        /// Starting top position
        /// </summary>
        const int MENU_STARTTOP = 4;
        /// <summary>
        /// Menu seperator item
        /// </summary>
        const string MENU_SEPERATOR = "--";
        #endregion

        #region Properties
        /// <summary>
        /// Menu items
        /// </summary>
        static readonly MenuItem[] MenuItems =
        {
            new MenuItem("Return", MenuItemAction.Return),
            new MenuItem(MENU_SEPERATOR, MenuItemAction.None),
            new MenuItem("Load", MenuItemAction.Load),
            new MenuItem("Save", MenuItemAction.Save),
            new MenuItem(MENU_SEPERATOR, MenuItemAction.None),
            new MenuItem("Quit", MenuItemAction.Quit),
        };

        /// <summary>
        /// MenuItem object
        /// </summary>
        class MenuItem
        {
            internal string Text;
            internal MenuItemAction Action;

            internal MenuItem(string pText, MenuItemAction pAction)
            {
                Text = pText;
                Action = pAction;
            }
        }

        enum MenuItemAction
        {
            None,
            Load,
            Quit,
            Return,
            Save,
        }

        /// <summary>
        /// Is user in the menu
        /// </summary>
        static bool inMenu;

        /// <summary>
        /// Present and past menu indexes
        /// </summary>
        static int PastMenuIndex = 0, MenuIndex = 0;
        #endregion

        #region Show
        /// <summary>
        /// Show menu
        /// </summary>
        static internal void Show()
        {
            inMenu = true;
            MenuItem gg = new MenuItem("Return", MenuItemAction.Return);

            // Generate and print the menu
            string top = Game.Graphics.Lines.SingleCorner[0] + ConsoleTools.RepeatChar(Game.Graphics.Lines.Single[1], MENU_WIDTH - 2) + Game.Graphics.Lines.SingleCorner[1];
            string bottom = Game.Graphics.Lines.SingleCorner[3] + ConsoleTools.RepeatChar(Game.Graphics.Lines.Single[1], MENU_WIDTH - 2) + Game.Graphics.Lines.SingleCorner[2];

            // Generate menu
            ConsoleTools.WriteAndCenter(Core.Layer.Menu, top, MENU_STARTTOP - 1);
            for (int i = 0; i < MenuItems.Length; i++)
            {
                // Get the item if..
                string item = (MenuItems[i].Text == MENU_SEPERATOR ?
                    // ..it's a MENU_SEPERATOR item
                    Game.Graphics.Lines.SingleConnector[3] + new string(Game.Graphics.Lines.Single[1], MENU_WIDTH - 2) + Game.Graphics.Lines.SingleConnector[0] :
                    // ..or just a regular item
                    Game.Graphics.Lines.Single[0] + ConsoleTools.CenterString(MenuItems[i].Text, MENU_WIDTH - 2) + Game.Graphics.Lines.Single[0]);

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
        }
        #endregion

        #region Entry
        /// <summary>
        /// Entry point for menu
        /// </summary>
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
        #endregion

        #region Controls
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

                if (MenuItems[MenuIndex].Text != MENU_SEPERATOR)
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

                if (MenuItems[MenuIndex].Text != MENU_SEPERATOR)
                    found = true;
            }

            // Update screen
            UpdateMenuOnScreen();
        }

        /// <summary>
        /// Selects the item in the menu
        /// </summary>
        static void Select()
        {
            switch (MenuItems[MenuIndex].Action)
            {
                case MenuItemAction.Save:
                    break;

                case MenuItemAction.Load:
                    break;

                case MenuItemAction.Quit:
                    inMenu = false;
                    MainClass.isPlaying = false;
                    break;

                case MenuItemAction.Return:
                    inMenu = false;
                    break;
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Update menu on screen
        /// </summary>
        static void UpdateMenuOnScreen()
        {
            // Get coords
            int MenuItemTop = MENU_STARTTOP + MenuIndex;
            int MenuItemTopPast = MENU_STARTTOP + PastMenuIndex;
            int MenuItemLeft = (ConsoleTools.BufferWidth / 2) - (MENU_WIDTH / 2);

            // Deselect old item
            Console.SetCursorPosition(MenuItemLeft + 1, MenuItemTopPast);
            Console.Write(ConsoleTools.CenterString(MenuItems[PastMenuIndex].Text, MENU_WIDTH - 2));

            // Apply new item's style
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(MenuItemLeft + 1, MenuItemTop);
            Console.Write(ConsoleTools.CenterString(MenuItems[MenuIndex].Text, MENU_WIDTH - 2));

            // Revert to original colors
            Console.ForegroundColor = ConsoleTools.OriginalForegroundColor;
            Console.BackgroundColor = ConsoleTools.OriginalBackgroundColor;
        }
        #endregion

        #region ClearMenu
        /// <summary>
        /// Clears the menu and places things back on screen.
        /// </summary>
        static void ClearMenu()
        {
            int startY = MENU_STARTTOP - 1;
            int startX = (ConsoleTools.BufferWidth / 2) - (MENU_WIDTH / 2);
            int lengthY = MenuItems.Length + 5; // Yeah I know it's that odd
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

            Game.MainPlayer.Initialize();
        }
        #endregion
    }
}