using System;
using System.Collections.Generic;

/*
 * Menu system
 */

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

    [Flags]
    public enum MenuResponse
    {
        Yes, No, Cancel,
        Quit
    }

    class Menu
    {
        public static Menu GameMenu = new Menu(
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

        //public static Menu StartMenu = new Menu();
        
        const int MENU_WIDTH = 40;
        
        int _pastindex, _index, _xpos, _ypos;
        
        public MenuResponse Response { get; private set; }
        public List<MenuItem> Items { get; }

        public Menu() : this(false, true, null) {}

        public Menu(params MenuItem[] items) : this(false, true, items) {}

        public Menu(bool show, bool redraw = true, params MenuItem[] items)
        {
            if (items != null)
                Items = new List<MenuItem>(items);
            else
                Items = new List<MenuItem>();

            _xpos = (Utils.WindowWidth / 2) - (MENU_WIDTH / 2);
            _ypos = (Utils.WindowHeight / 2) - (items.Length / 2) - 2;

            if (show)
                Show(redraw);
        }

        /// <summary>
        /// Show menu
        /// </summary>
        public void Show(bool redraw = true)
        {
            Draw();

            // Select good starting index
            _index = -1;
            bool s = true;
            while (s)
            {
                if (++_index > Items.Count - 1)
                    _index = 0;

                switch (Items[_index].Type)
                {
                    case MenuItemType.Information:
                    case MenuItemType.Seperator: break;
                    default: s = false; break;
                }
            }

            // "Select" item
            Update();

            // While in menu, do actions
            while (Entry());

            if (redraw)
                // Clear menu and reprint layer underneath
                ClearMenu();
        }

        /// <summary>
        /// Entry point for menu
        /// </summary>
        public bool Entry()
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
                    return Select();

                case ConsoleKey.Escape:
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Goes to the next control
        /// </summary>
        public void NextControl()
        {
            bool s = true;
            _pastindex = _index;

            // Find a good index
            while (s)
            {
                _index++;

                if (_index > Items.Count - 1)
                    _index = 0;

                switch (Items[_index].Type)
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
            _pastindex = _index;

            // Find a good index
            while (s)
            {
                _index--;

                if (_index < 0)
                    _index = Items.Count - 1;

                switch (Items[_index].Type)
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
        public bool Select()
        {
            MenuItem item = Items[_index];
            
            item.Action?.Invoke();

            switch (item.Type) // Old system for compatibility.
            {
                case MenuItemType.Yes:
                    Response = MenuResponse.Yes;
                    return false;
                case MenuItemType.No:
                    Response = MenuResponse.No;
                    return false;
                case MenuItemType.Cancel:
                    Response = MenuResponse.Cancel;
                    return false;
                    
                case MenuItemType.Return:
                    return false;

                case MenuItemType.ShowInventory:
                    ClearMenu(false);
                    Game.MainPlayer.Inventory.Show();
                    Draw();
                    Update();
                    break;

                case MenuItemType.ShowStatistics:
                    ClearMenu(false);
                    new Menu(true, false,
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
                    Response = MenuResponse.Quit;
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Update menu on screen (Main)
        /// </summary>
        void Update()
        {
            // Get coords
            int itemY = _ypos + _index + 1;
            int pastItemY = _ypos + _pastindex + 1;
            int textX = _xpos + 1;

            // Deselect old item
            if (_index != _pastindex)
            {
                Console.SetCursorPosition(textX, pastItemY);
                Console.Write(Items[_pastindex].Text.Center(MENU_WIDTH - 2));
            }

            // Apply new item's colors
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;
            Console.SetCursorPosition(textX, itemY);
            Console.Write(Items[_index].Text.Center(MENU_WIDTH - 2));

            // Revert to original colors
            Console.ResetColor();
        }

        void Draw()
        {
            // Item width
            int iw = MENU_WIDTH - 2;
            string line = new string('─', iw);
            string fline = $"├{line}┤";
            int c = Items.Count;

            Console.SetCursorPosition(_xpos, _ypos);
            Console.Write($"┌{line}┐");
            for (int i = 0; i < c; i++)
            {
                Console.SetCursorPosition(_xpos, _ypos + i + 1);

                switch (Items[i].Type)
                {
                    case MenuItemType.Seperator:
                        Console.Write(fline);
                        break;
                    default:
                        // TODO: "Disabled" buttons

                        Console.Write($"│{Items[i].Text.Center(iw)}│");
                        break;
                }
            }
            Console.SetCursorPosition(_xpos, _ypos + c + 1);
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
                    _ypos,
                    MENU_WIDTH,
                    Items.Count + 2
                );
            else
            {
                string b = new string(' ', MENU_WIDTH);
                int ly = _ypos + Items.Count + 2;
                int x = (Utils.WindowWidth / 2) - (MENU_WIDTH / 2);

                for (int y = _ypos; y < ly; y++)
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
        /// <summary>
        /// Custom action.
        /// </summary>
        public Action Action { get; }

        public MenuItem()
            : this(null, MenuItemType.Seperator) {}

        public MenuItem(string text)
            : this(text, MenuItemType.Information) {}

        public MenuItem(MenuItemType t)
            : this(t.ToString(), t) {}

        public MenuItem(string text, MenuItemType type)
        {
            Text = text;
            Type = type;
        }

        public MenuItem(string text, Action action)
        {
            Text = text;
            Action = action;
        }

        public MenuItem(string text, Action action, MenuItemType type)
        {
            Text = text;
            Action = action;
            Type = type;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}