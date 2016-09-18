using System;
using System.Collections.Generic;

/*
    Menu system
*/

//TODO: DialogResponse

namespace fwod
{
    public enum MenuItemType : byte
    {
        Information,
        Seperator,
        Return,
        ShowStatistics,
        Load,
        Save,
        Quit,
        ShowInventory,
        Yes, No, Cancel // "Dialog"-type
    }

    public enum MenuResponse
    {
        Yes, No, Cancel
    }

    class Menu
    {
        public static Menu GetMainMenu()
        {
            return new Menu(
                new MenuItem("Return", MenuItemType.Return),
                new MenuItem(),
                new MenuItem("Abilities"),
                new MenuItem("Inventory", MenuItemType.ShowInventory),
                new MenuItem(),
                new MenuItem("Statistics", MenuItemType.ShowStatistics),
                new MenuItem(),
                new MenuItem("Load"),// MenuItemType.Load),
                new MenuItem("Save"),// MenuItemType.Save),
                new MenuItem(),
                new MenuItem("Settings"),
                new MenuItem(),
                new MenuItem("Quit", MenuItemType.Quit)
            );
        }
        
        const int MENU_WIDTH = 40;
        const int MENU_TOP = 4;
        public bool InMenu { get; private set; }
        public MenuResponse Response { get; private set; }
        List<MenuItem> MenuItemList { get; }
        
        int PastMenuIndex = 0, MenuIndex = 0, LeftPosition = 0;

        public Menu(params MenuItem[] items) : this(false, items) {}

        public Menu(bool show, params MenuItem[] items)
        {
            MenuItemList = new List<MenuItem>(items);

            LeftPosition = (Utils.WindowWidth / 2) - (MENU_WIDTH / 2);

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
            MenuIndex = -1;
            bool s = true;
            while (s)
            {
                MenuIndex++;

                if (MenuIndex > MenuItemList.Count - 1)
                    MenuIndex = 0;

                switch (MenuItemList[MenuIndex].Type)
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
        public void Entry()
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
        public void NextControl()
        {
            bool s = true;
            PastMenuIndex = MenuIndex;

            // Find a good index
            while (s)
            {
                MenuIndex++;

                if (MenuIndex > MenuItemList.Count - 1)
                    MenuIndex = 0;

                switch (MenuItemList[MenuIndex].Type)
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
        public void PreviousControl()
        {
            bool s = true;
            PastMenuIndex = MenuIndex;

            // Find a good index
            while (s)
            {
                MenuIndex--;

                if (MenuIndex < 0)
                    MenuIndex = MenuItemList.Count - 1;

                switch (MenuItemList[MenuIndex].Type)
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
        public void Select()
        {
            switch (MenuItemList[MenuIndex].Type)
            {
                case MenuItemType.Yes:
                    
                    break;
                case MenuItemType.Return:
                    InMenu = false;
                    break;

                case MenuItemType.ShowInventory:
                    ClearMenu(false);
                    Game.MainPlayer.Inventory.Show();
                    break;

                case MenuItemType.ShowStatistics:
                    ClearMenu(false);
                    new Menu(true,
                        new MenuItem($"Steps taken: {Game.Statistics.StepsTaken}"),
                        new MenuItem($"Monsters killed: {Game.Statistics.EnemiesKilled}"),
                        new MenuItem($"Damage dealt {Game.Statistics.EnemiesKilled}"),
                        new MenuItem($"Damage received {Game.Statistics.EnemiesKilled}"),
                        new MenuItem($"Money gain: {Game.Statistics.MoneyGained}$"),
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
                    InMenu = Game.IsPlaying = false;
                    break;
            }
        }

        /// <summary>
        /// Update menu on screen (Main)
        /// </summary>
        void Update()
        {
            // Get coords
            int itemY = MENU_TOP + MenuIndex + 1;
            int pastItemY = MENU_TOP + PastMenuIndex + 1;
            int textX = LeftPosition + 1;

            // Deselect old item
            if (MenuIndex != PastMenuIndex)
            {
                Console.SetCursorPosition(textX, pastItemY);
                Console.Write(Utils.Center(MenuItemList[PastMenuIndex].Text, MENU_WIDTH - 2));
            }

            // Apply new item's colors
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(textX, itemY);
            Console.Write(Utils.Center(MenuItemList[MenuIndex].Text, MENU_WIDTH - 2));

            // Revert to original colors
            Console.ResetColor();
        }

        void Draw()
        {
            string line = new string('─', MENU_WIDTH - 2);
            string fline = $"├{line}┤";
            int c = MenuItemList.Count;

            Console.SetCursorPosition(LeftPosition, MENU_TOP);
            Console.Write($"┌{line}┐");
            for (int i = 0; i < c; i++)
            {
                Console.SetCursorPosition(LeftPosition, MENU_TOP + i + 1);

                switch (MenuItemList[i].Type)
                {
                    case MenuItemType.Seperator:
                        Console.Write(fline);
                        break;
                    default:
                        Console.Write($"│{MenuItemList[i].Text.Center(MENU_WIDTH - 2)}│");
                        break;
                }
            }
            Console.SetCursorPosition(LeftPosition, MENU_TOP + c + 1);
            Console.Write($"└{line}┘");
        }

        /// <summary>
        /// Clears the menu and redraw map.
        /// </summary>
        public void ClearMenu(bool redraw = true)
        {
            if (redraw)
                MapManager.RedrawMap(
                    (Utils.WindowWidth / 2) - (MENU_WIDTH / 2),
                    MENU_TOP,
                    MENU_WIDTH,
                    MenuItemList.Count + 2
                );
            else
            {
                string b = new string(' ', MENU_WIDTH);
                int ly = MENU_TOP + MenuItemList.Count + 2;
                int x = (Utils.WindowWidth / 2) - (MENU_WIDTH / 2);

                for (int y = MENU_TOP; y < ly; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(b);
                }
            }
        }
    }
    
    class MenuItem
    {
        public string Text { get; }
        public MenuItemType Type { get; }

        public MenuItem()
            : this(null, MenuItemType.Seperator) {}

        public MenuItem(string text)
            : this(text, MenuItemType.Information) {}

        public MenuItem(string text, MenuItemType type)
        {
            Text = text;
            Type = type;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}