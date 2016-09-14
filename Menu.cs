using System;
using System.Collections.Generic;

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
        ShowInventory
    }

    class Menu
    {
        public static Menu GetMainMenu()
        {
            return new Menu(
                new MenuItem("Return", MenuItemType.Return),
                new MenuItem(),
                new MenuItem("Inventory", MenuItemType.ShowInventory),
                new MenuItem(),
                new MenuItem("Statistics", MenuItemType.ShowStats),
                new MenuItem(),
                new MenuItem("Load", MenuItemType.Load),
                new MenuItem("Save", MenuItemType.Save),
                new MenuItem(),
                new MenuItem("Quit", MenuItemType.Quit)
            );
        }
        
        const int MENU_WIDTH = 30;
        const int MENU_TOP = 4;
        public bool InMenu { get; private set; }
        List<MenuItem> MenuItemList { get; }

        /// <summary>
        /// Present and past menu indexes
        /// </summary>
        int PastMenuIndex = 0, MenuIndex = 0;

        public Menu(params MenuItem[] items) : this(false, items) {}

        public Menu(bool show, params MenuItem[] items)
        {
            MenuItemList = new List<MenuItem>(items);

            if (show)
                Show();
        }

        /// <summary>
        /// Show menu
        /// </summary>
        public void Show()
        {
            InMenu = true;

            Draw();

            // Select good starting index
            bool s = true;
            MenuIndex = -1;
            while (s)
            {
                MenuIndex++;

                if (MenuIndex > MenuItemList.Count - 1)
                    MenuIndex = 0;

                switch (MenuItemList[MenuIndex].ItemType)
                {
                    case MenuItemType.Information:
                    case MenuItemType.Seperator: break;
                    default: s = false; break;
                }
            }

            // "Select" item
            Update();

            // While in menu, do actions
            do
            {
                Entry();
            } while (InMenu);

            // Clear menu and reprint layer underneath
            ClearMenu();
        }

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
                    InMenu = false;
                    break;
            }
        }

        /// <summary>
        /// Goes to the next control
        /// </summary>
        internal void NextControl()
        {
            bool s = true;
            PastMenuIndex = MenuIndex;

            // Find a good index
            while (s)
            {
                MenuIndex++;

                if (MenuIndex > MenuItemList.Count - 1)
                    MenuIndex = 0;

                switch (MenuItemList[MenuIndex].ItemType)
                {
                    case MenuItemType.Information:
                    case MenuItemType.Seperator: break;
                    default: s = false; break;
                }
            }
            
            Update();
        }

        /// <summary>
        /// Goes to the previous control
        /// </summary>
        internal void PreviousControl()
        {
            bool s = true;
            PastMenuIndex = MenuIndex;

            // Find a good index
            while (s)
            {
                MenuIndex--;

                if (MenuIndex < 0)
                    MenuIndex = MenuItemList.Count - 1;

                switch (MenuItemList[MenuIndex].ItemType)
                {
                    case MenuItemType.Information:
                    case MenuItemType.Seperator: break;
                    default: s = false; break;
                }
            }
            
            Update();
        }

        /// <summary>
        /// Selects the item in the menu
        /// </summary>
        internal void Select()
        {
            switch (MenuItemList[MenuIndex].ItemType)
            {
                case MenuItemType.Return:
                    InMenu = false;
                    break;

                case MenuItemType.ShowInventory:
                    Game.MainPlayer.ShowInventory();
                    break;

                case MenuItemType.ShowStats:
                    new Menu(true,
                        new MenuItem("Steps taken", MenuItemType.Information),
                        new MenuItem($"{Game.Statistics.StatStepsTaken}", MenuItemType.Information),
                        new MenuItem("Monsters killed", MenuItemType.Information),
                        new MenuItem($"{Game.Statistics.StatEnemiesKilled}", MenuItemType.Information),
                        new MenuItem("Damage dealt", MenuItemType.Information),
                        new MenuItem($"{Game.Statistics.StatEnemiesKilled}", MenuItemType.Information),
                        new MenuItem("Damage received", MenuItemType.Information),
                        new MenuItem($"{Game.Statistics.StatEnemiesKilled}", MenuItemType.Information),
                        new MenuItem("Money gain", MenuItemType.Information),
                        new MenuItem($"{Game.Statistics.StatMoneyGained}$", MenuItemType.Information),
                        new MenuItem(),
                        new MenuItem("Return", MenuItemType.Return)
                    );
                    Draw();
                    Update();
                    break;

                case MenuItemType.Save:
                    break;

                case MenuItemType.Load:
                    break;

                case MenuItemType.Quit:
                    InMenu = Game.isPlaying = false;
                    break;
            }
        }

        /// <summary>
        /// Update menu on screen (Main)
        /// </summary>
        void Update()
        {
            // Get coords
            int MenuItemTop = MENU_TOP + MenuIndex + 1;
            int MenuItemTopPast = MENU_TOP + PastMenuIndex + 1;
            int MenuItemLeft = (Utils.WindowWidth / 2) - (MENU_WIDTH / 2);

            // Deselect old item
            if (MenuIndex != PastMenuIndex)
            {
                Console.SetCursorPosition(MenuItemLeft + 1, MenuItemTopPast);
                Console.Write(Utils.CenterString(MenuItemList[PastMenuIndex].Text, MENU_WIDTH - 2));
            }

            // Apply new item's colors
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(MenuItemLeft + 1, MenuItemTop);
            Console.Write(Utils.CenterString(MenuItemList[MenuIndex].Text, MENU_WIDTH - 2));

            // Revert to original colors
            Console.ResetColor();
        }

        void Draw()
        {
            string line = new string('─', MENU_WIDTH - 2);

            int i = 1;
            Utils.WriteAndCenter(Renderer.Layer.Menu, $"┌{line}┐", MENU_TOP);
            foreach (MenuItem item in MenuItemList)
            {
                Utils.WriteAndCenter(
                    Renderer.Layer.Menu,
                    item.ItemType == MenuItemType.Seperator ?
                    $"├{line}┤" : $"│{Utils.CenterString(item.Text, MENU_WIDTH - 2)}│",
                    MENU_TOP + i++);
            }
            Utils.WriteAndCenter(Renderer.Layer.Menu, $"└{line}┘", MENU_TOP + MenuItemList.Count + 1);
        }

        /// <summary>
        /// Clears the menu and places things back on screen.
        /// </summary>
        public void ClearMenu()
        {
            int startY = MENU_TOP;
            int startX = (Utils.WindowWidth / 2) - (MENU_WIDTH / 2);
            int lengthY = MenuItemList.Count + 6; // Yeah I know it's that odd
            int gamelayer = (int)Renderer.Layer.Game;

            //TODO: Optimize this with a pointer probably
            for (int row = startY; row < lengthY; row++)
            {
                for (int col = startX; col < Utils.WindowWidth; col++)
                {
                    Console.SetCursorPosition(col, row); // Safety measure
                    Console.Write(
                        Renderer.Layers[gamelayer][row, col] == '\0' ?
                        ' ' : Renderer.Layers[gamelayer][row, col]);
                }
            }

            // Place the people back on screen.
            foreach (Person e in Game.EnemyList)
                e.Initialize();
            foreach (Person p in Game.PeopleList)
                p.Initialize();

            Game.MainPlayer.Initialize();
        }
    }
    
    class MenuItem
    {
        public string Text { get; }
        public MenuItemType ItemType { get; }

        public MenuItem()
            : this(string.Empty, MenuItemType.Seperator) {}

        public MenuItem(string pText)
            : this(pText, MenuItemType.Information) {}

        public MenuItem(string pText, MenuItemType pAction)
        {
            Text = pText;
            ItemType = pAction;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}