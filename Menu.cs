using System;

/*
    Menu system
*/

namespace fwod
{
    internal enum MenuItemType : byte
    {
        Information,
        Seperator,
        Return,
        ShowStats,
        Load,
        Save,
        Quit,
    }

    class Menu
    {
        /// <summary>
        /// Main menu items
        /// </summary>
        static readonly MenuItem[] MainMenuItems =
        {
            new MenuItem("Return", MenuItemType.Return),
            new MenuItem(),
            new MenuItem("Statistics", MenuItemType.ShowStats),
            new MenuItem(),
            new MenuItem("Load", MenuItemType.Load),
            new MenuItem("Save", MenuItemType.Save),
            new MenuItem(),
            new MenuItem("Quit", MenuItemType.Quit),
        };

        internal Menu()
        {
            CurrentMenu = MainMenuItems;
        }

        internal Menu(MenuItem[] pItems)
        {
            CurrentMenu = pItems;
        }

        static internal void Show()
        {
            new Menu().Display();
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

        #region Display on screen
        /// <summary>
        /// Show menu
        /// </summary>
        internal void Display()
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
            Utils.WriteAndCenter(Renderer.Layer.Menu, top, MENU_TOP - 1);
            for (int i = 0; i < CurrentMenu.Length; i++)
            {
                // Get the item if..
                string item = CurrentMenu[i].ItemType == MenuItemType.Seperator ?
                    // ..it's a MENU_SEPERATOR item
                    Game.Graphics.Lines.SingleConnector[3] + new string(Game.Graphics.Lines.Single[1], MENU_WIDTH - 2) + Game.Graphics.Lines.SingleConnector[0] :
                    // ..or just a regular item
                    Game.Graphics.Lines.Single[0] + Utils.CenterString(CurrentMenu[i].Text, MENU_WIDTH - 2) + Game.Graphics.Lines.Single[0];

                // Print item
                Utils.WriteAndCenter(Renderer.Layer.Menu, item, MENU_TOP + i);
            }
            Utils.WriteAndCenter(Renderer.Layer.Menu, bottom, MENU_TOP + CurrentMenu.Length);

            // Select good starting index
            bool found = false;
            MenuIndex = -1;
            while (!found) //TODO: Make this a for(;;) instead
            {
                MenuIndex++;

                if (MenuIndex > CurrentMenu.Length - 1)
                    MenuIndex = 0;

                switch (CurrentMenu[MenuIndex].ItemType)
                {
                    case MenuItemType.Information:
                    case MenuItemType.Seperator:
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
                    case MenuItemType.Information:
                    case MenuItemType.Seperator:
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
                    case MenuItemType.Information:
                    case MenuItemType.Seperator:
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
                case MenuItemType.Return:
                    inMenu = false;
                    break;

                case MenuItemType.ShowStats:
                    MenuItem[] StatisticsMenuItems = 
                    {
                        new MenuItem("Steps taken", MenuItemType.Information),
                        new MenuItem(Game.Statistics.StatStepsTaken.ToString(), MenuItemType.Information),
                        new MenuItem("Monsters killed", MenuItemType.Information),
                        new MenuItem(Game.Statistics.StatEnemiesKilled.ToString(), MenuItemType.Information),
                        new MenuItem("Damage dealt", MenuItemType.Information),
                        new MenuItem(Game.Statistics.StatEnemiesKilled.ToString(), MenuItemType.Information),
                        new MenuItem("Damage received", MenuItemType.Information),
                        new MenuItem(Game.Statistics.StatEnemiesKilled.ToString(), MenuItemType.Information),
                        new MenuItem("Money gain", MenuItemType.Information),
                        new MenuItem($"{Game.Statistics.StatMoneyGained}$", MenuItemType.Information),
                        new MenuItem(),
                        new MenuItem("Return", MenuItemType.Return)
                    };
                    Menu s = new Menu(StatisticsMenuItems);
                    s.Display();
                    s.inMenu = false;
                    inMenu = false;
                    break;

                case MenuItemType.Save:
                    break;

                case MenuItemType.Load:
                    break;

                case MenuItemType.Quit:
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
            int MenuItemLeft = (Utils.WindowWidth / 2) - (MENU_WIDTH / 2);

            // Deselect old item
            if (MenuIndex != PastMenuIndex)
            {
                Console.SetCursorPosition(MenuItemLeft + 1, MenuItemTopPast);
                Console.Write(Utils.CenterString(CurrentMenu[PastMenuIndex].Text, MENU_WIDTH - 2));
            }

            // Apply new item's colors
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(MenuItemLeft + 1, MenuItemTop);
            Console.Write(Utils.CenterString(CurrentMenu[MenuIndex].Text, MENU_WIDTH - 2));

            // Revert to original colors
            Console.ResetColor();
        }
        #endregion

        #region ClearMenu
        /// <summary>
        /// Clears the menu and places things back on screen.
        /// </summary>
        internal void ClearMenu()
        {
            int startY = MENU_TOP - 1;
            int startX = (Utils.WindowWidth / 2) - (MENU_WIDTH / 2);
            int lengthY = CurrentMenu.Length + 5; // Yeah I know it's that odd
            int gamelayer = (int)Renderer.Layer.Game;

            for (int row = startY; row < lengthY; row++)
            {
                for (int col = startX; col < Utils.WindowWidth; col++)
                {
                    Console.SetCursorPosition(col, row); // Safety measure
                    Console.Write(Renderer.Layers[gamelayer][row, col] == '\0' ?
                        ' ' : Renderer.Layers[gamelayer][row, col]);
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
            : this(string.Empty, MenuItemType.Seperator)
        {

        }

        internal MenuItem(string pText)
            : this(pText, MenuItemType.Information)
        {

        }

        internal MenuItem(string pText, MenuItemType pAction)
        {
            Text = pText;
            ItemType = pAction;
        }
    }
}