using System;

/*
    Menu system
*/

namespace fwod
{
    static class DisplayMenu
    {
        /// <summary>
        /// Main menu items
        /// </summary>
        internal static readonly MenuItem[] MainMenuItems =
        {
            new MenuItem("Return", MenuItem.MenuItemType.Return),
            new MenuItem(),
            new MenuItem("Statistics", MenuItem.MenuItemType.ShowStats),
            new MenuItem(),
            new MenuItem("Load", MenuItem.MenuItemType.Load),
            new MenuItem("Save", MenuItem.MenuItemType.Save),
            new MenuItem(),
            new MenuItem("Quit", MenuItem.MenuItemType.Quit),
        };

        internal static void Show(MenuItem[] pItems)
        {
            Menu m = new Menu(pItems);
            m.Show();
        }
    }

    class Menu
    {
        internal Menu(MenuItem[] pItems)
        {
            CurrentMenu = pItems;
        }

        #region Constants
        /// <summary>
        /// Width of the menu
        /// </summary>
        const int MENU_WIDTH = 30;
        /// <summary>
        /// Starting top position
        /// </summary>
        const int MENU_TOP = 4;
        #endregion

        #region Properties
        static MenuItem[] CurrentMenu;

        /// <summary>
        /// Is user in the menu
        /// </summary>
        bool inMenu;

        /// <summary>
        /// Present and past menu indexes
        /// </summary>
        int PastMenuIndex = 0, MenuIndex = 0;
        #endregion

        #region Show
        /// <summary>
        /// Show menu
        /// </summary>
        internal void Show()
        {
            inMenu = true;

            // Generate and print the menu
            string top = Game.Graphics.Lines.SingleCorner[0] + 
                new string(Game.Graphics.Lines.Single[1], MENU_WIDTH - 2) + 
                Game.Graphics.Lines.SingleCorner[1];
            string bottom = Game.Graphics.Lines.SingleCorner[3] + 
                new string(Game.Graphics.Lines.Single[1], MENU_WIDTH - 2) + 
                Game.Graphics.Lines.SingleCorner[2];

            // Generate menu
            ConsoleTools.WriteAndCenter(Core.Layer.Menu, top, MENU_TOP - 1);
            for (int i = 0; i < CurrentMenu.Length; i++)
            {
                // Get the item if..
                string item = (CurrentMenu[i].ItemType == MenuItem.MenuItemType.Seperator ?
                    // ..it's a MENU_SEPERATOR item
                    Game.Graphics.Lines.SingleConnector[3] + new string(Game.Graphics.Lines.Single[1], MENU_WIDTH - 2) + Game.Graphics.Lines.SingleConnector[0] :
                    // ..or just a regular item
                    Game.Graphics.Lines.Single[0] + ConsoleTools.CenterString(CurrentMenu[i].Text, MENU_WIDTH - 2) + Game.Graphics.Lines.Single[0]);

                // Print item
                ConsoleTools.WriteAndCenter(Core.Layer.Menu, item, MENU_TOP + i);
            }
            ConsoleTools.WriteAndCenter(Core.Layer.Menu, bottom, MENU_TOP + CurrentMenu.Length);

            // Select good starting index
            bool found = false;
            MenuIndex = -1;
            while (!found)
            {
                MenuIndex++;

                if (MenuIndex > CurrentMenu.Length - 1)
                    MenuIndex = 0;

                switch (CurrentMenu[MenuIndex].ItemType)
                {
                    case MenuItem.MenuItemType.Info:
                    case MenuItem.MenuItemType.Seperator:
                        break;
                    default:
                        found = true;
                        break;
                }
            }

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
        internal void Entry()
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
        internal void NextControl()
        {
            bool found = false;
            PastMenuIndex = MenuIndex;

            // Find a good index
            while (!found)
            {
                MenuIndex++;

                if (MenuIndex > CurrentMenu.Length - 1)
                    MenuIndex = 0;

                switch (CurrentMenu[MenuIndex].ItemType)
                {
                    case MenuItem.MenuItemType.Info:
                    case MenuItem.MenuItemType.Seperator:
                        break;
                    default:
                        found = true;
                        break;
                }
            }

            // Update screen
            UpdateMenuOnScreen();
        }

        /// <summary>
        /// Goes to the previous control
        /// </summary>
        internal void PreviousControl()
        {
            bool found = false;
            PastMenuIndex = MenuIndex;

            // Find a good index
            while (!found)
            {
                MenuIndex--;

                if (MenuIndex < 0)
                    MenuIndex = CurrentMenu.Length - 1;

                switch (CurrentMenu[MenuIndex].ItemType)
                {
                    case MenuItem.MenuItemType.Info:
                    case MenuItem.MenuItemType.Seperator:
                        break;
                    default:
                        found = true;
                        break;
                }
            }

            // Update screen
            UpdateMenuOnScreen();
        }

        /// <summary>
        /// Selects the item in the menu
        /// </summary>
        internal void Select()
        {
            switch (CurrentMenu[MenuIndex].ItemType)
            {
                case MenuItem.MenuItemType.Return:
                    inMenu = false;
                    break;

                case MenuItem.MenuItemType.ShowStats:
                    MenuItem[] StatisticsMenuItems = 
                    {
                        new MenuItem("Steps taken", MenuItem.MenuItemType.Info),
                        new MenuItem(string.Format("{0}", Game.StatStepsTaken), MenuItem.MenuItemType.Info),
                        new MenuItem("Monsters killed", MenuItem.MenuItemType.Info),
                        new MenuItem(string.Format("{0}", Game.StatEnemiesKilled), MenuItem.MenuItemType.Info),
                        new MenuItem("Damage dealt", MenuItem.MenuItemType.Info),
                        new MenuItem(string.Format("{0}", Game.StatDamageDealt), MenuItem.MenuItemType.Info),
                        new MenuItem("Damage received", MenuItem.MenuItemType.Info),
                        new MenuItem(string.Format("{0}", Game.StatDamageReceived), MenuItem.MenuItemType.Info),
                        new MenuItem("Money gain", MenuItem.MenuItemType.Info),
                        new MenuItem(string.Format("{0}$", Game.StatMoneyGained), MenuItem.MenuItemType.Info),
                        new MenuItem(),
                        //new MenuItem("Back", MenuItem.MenuItemType.Return),
                        new MenuItem("Return", MenuItem.MenuItemType.Return)
                    };
                    Menu s = new Menu(StatisticsMenuItems);
                    s.Show();
                    s.inMenu = false;
                    inMenu = false;
                    break;

                case MenuItem.MenuItemType.Save:
                    break;

                case MenuItem.MenuItemType.Load:
                    break;

                case MenuItem.MenuItemType.Quit:
                    inMenu = false;
                    Game.isPlaying = false;
                    break;
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Update menu on screen (Main)
        /// </summary>
        void UpdateMenuOnScreen()
        {
            // Get coords
            int MenuItemTop = MENU_TOP + MenuIndex;
            int MenuItemTopPast = MENU_TOP + PastMenuIndex;
            int MenuItemLeft = (ConsoleTools.BufferWidth / 2) - (MENU_WIDTH / 2);

            // Deselect old item
            if (MenuIndex != PastMenuIndex)
            {
                Console.SetCursorPosition(MenuItemLeft + 1, MenuItemTopPast);
                Console.Write(ConsoleTools.CenterString(CurrentMenu[PastMenuIndex].Text, MENU_WIDTH - 2));
            }

            // Apply new item's colors
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(MenuItemLeft + 1, MenuItemTop);
            Console.Write(ConsoleTools.CenterString(CurrentMenu[MenuIndex].Text, MENU_WIDTH - 2));

            // Revert to original colors
            Console.ForegroundColor = ConsoleTools.OriginalForegroundColor;
            Console.BackgroundColor = ConsoleTools.OriginalBackgroundColor;
        }
        #endregion

        #region ClearMenu
        /// <summary>
        /// Clears the menu and places things back on screen.
        /// </summary>
        internal void ClearMenu()
        {
            int startY = MENU_TOP - 1;
            int startX = (ConsoleTools.BufferWidth / 2) - (MENU_WIDTH / 2);
            int lengthY = CurrentMenu.Length + 5; // Yeah I know it's that odd
            int gamelayer = (int)Core.Layer.Game;

            for (int row = startY; row < lengthY; row++)
            {
                for (int col = startX; col < ConsoleTools.BufferWidth; col++)
                {
                    Console.SetCursorPosition(col, row); // Safety measure
                    Console.Write(Core.Layers[gamelayer][row, col] == '\0' ?
                        ' ' : Core.Layers[gamelayer][row, col]);
                }
            }

            // Place PEEPs back on screen
            foreach (Person enemy in Game.EnemyList)
                enemy.Initialize();

            foreach (Person p in Game.PeopleList)
                p.Initialize();

            Game.MainPlayer.Initialize();
        }
        #endregion
    }
    
    class MenuItem
    {
        internal string Text;
        internal MenuItemType ItemType;

        internal MenuItem()
            : this("", MenuItemType.Seperator)
        {

        }

        internal MenuItem(string pText, MenuItemType pAction)
        {
            Text = pText;
            ItemType = pAction;
        }

        internal enum MenuItemType
        {
            Info,
            Seperator,
            Return,
            ShowStats,
            Load,
            Save,
            Quit,
        }
    }
}