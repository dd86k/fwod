using System;
using System.Collections.Generic;

/*TODO: Inventory mockup
4x10 spaces, 1 character horizontal padding

|-------------------46-----------------------|
|--------21---------|
                    |---------- 26-----------|

+-+-+-+-+-+-+-+-+-+-+------------------------+ -
|o|m| | | | | | | | | Potion                 | |
+-+-+-+-+-+-+-+-+-+-+                        | |
| | | | | | | | | | | Heals 10 HP.           | |
+-+-+-+-+-+-+-+-+-+-+                        | |
| | | | | | | | | | |                        | |
+-+-+-+-+-+-+-+-+-+-+                        | 14
| | | | | | | | | | |                        | |
+-+-+-+-+-+-+-+-+-+-+------------------------+ |
| Weapon: Flashy Sword                       | |
| Armour: Dented Meat                        | |
+--------------------------------------------+ |
|                Return                      | |
+--------------------------------------------+ -
*/

namespace fwod
{
    class InventoryManager
    {
        const int INV_ROW = 4;
        const int INV_COL = 10;
        const int INV_MAX = INV_COL * INV_ROW;
        const int INV_WIDTH = 46;
        const int INV_HEIGHT = 14;

        public Weapon EquippedWeapon { get; set; }
        public Armor EquippedArmor { get; set; }
        List<Item> Items { get; }

        // Menu location on screen
        int _mx, _my;
        // Cursor location in the menu
        int _cx, _cy;
        // Past cursor location.
        int _ocx, _ocy;

        public InventoryManager()
        {
            Items = new List<Item>();
        }

        public bool AddItem(Item item)
        {
            bool added = Items.Count + 1 <= INV_MAX;

            if (added)
                Items.Add(item);

            return added;
        }

        public bool DropItem(int index)
        {
            Items.RemoveAt(index);

            //TODO: Place item on map

            return true;
        }

        public void Show()
        {
            _mx = (Utils.WindowWidth / 2) - (INV_WIDTH / 2);
            _my = (Utils.WindowHeight / 2) - (INV_ROW * 2);

            Draw();
            
            Game.Log("Press ESC to return.");
            
            while (Entry());

            Clear();
        }

        void Draw()
        {
            // Y position of the end of the grill.
            int by = (INV_ROW * 2) + 5;

            Utils.GenerateBox(_mx, _my, INV_WIDTH, INV_HEIGHT);

            Utils.GenerateGrill(_mx, _my, INV_COL, INV_ROW);

            Console.SetCursorPosition(_mx + (INV_WIDTH - 25), _my + (INV_HEIGHT - 6));
            Console.Write(new string('─', INV_WIDTH - (INV_COL * 2 ) - 2));
            Console.SetCursorPosition(_mx + 1, by + 2);
            Console.Write(new string('─', INV_WIDTH - 2));

            Console.SetCursorPosition(_mx + 1, by);
            Console.Write("W: " + EquippedWeapon.FullName);
            Console.SetCursorPosition(_mx + 1, by + 1);
            Console.Write("A: " + EquippedArmor.FullName);

            if (Items.Count > 0)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    int x = i / INV_COL,
                        y = i % INV_ROW;

                    Console.SetCursorPosition(_mx + x + 1, _my + y + 1);
                    Console.Write(Items[i].ToString()[0]);
                }
            }

            Update();
        }

        void Update()
        {
            // 1 Dimensional indexer
            int  d = (INV_ROW * _cy) + _cx,
                od = (INV_ROW * _ocy) + _ocx;

            // Clear old selected
            Console.SetCursorPosition(_mx + (_ocx * 2) + 1, _my + (_ocy * 2) + 1);
            if (od < Items.Count)
            {
                Console.Write(Items[od].ToString()[0]);
                InsertDescription(Items[od]);
            }
            else
            {
                Console.Write(' ');
                ClearDescriptionBox();
            }

            // New selection
            Console.SetCursorPosition(_mx + (_cx * 2) + 1, _my + (_cy * 2) + 1);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Gray;

            if (d < Items.Count)
            {
                Console.Write(Items[d].ToString()[0]);
                InsertDescription(Items[d]);
            }
            else
            {
                Console.Write(' ');
                ClearDescriptionBox();
            }

            Console.ResetColor();

            _ocx = _cx;
            _ocy = _cy;
        }

        void InsertDescription(Item item)
        {
            Console.ResetColor();
            Console.SetCursorPosition(_mx + (INV_COL * 2) + 2, _my + 1);

            if (item is Food)
            {
                Console.Write(item as Food);
            }
            else if (item is Weapon)
            {
                Console.Write(item as Weapon);
            }
            else if (item is Armor)
            {
                Console.Write(item as Armor);
            }
        }

        void ClearDescriptionBox()
        { //TODO: ClearDescriptionBox()
            // Description box inner width
            //int dw = INV_WIDTH -

            Console.SetCursorPosition(_mx + (INV_COL * 2) + 2, _my + 1);
        }

        bool Entry()
        { //TODO: InventoryManager::Entry()
            ConsoleKeyInfo ck = Console.ReadKey(true);

            switch (ck.Key)
            {
                case ConsoleKey.DownArrow:
                    if (_cy + 1 >= INV_ROW)
                        _cy = 0;
                    else
                        ++_cy;
                    Update();
                    break;
                case ConsoleKey.UpArrow:
                    if (_cy - 1 < 0)
                        _cy = INV_ROW - 1;
                    else
                        --_cy;
                    Update();
                    break;

                case ConsoleKey.RightArrow:
                    if (_cx + 1 >= INV_COL)
                        _cx = 0;
                    else
                        ++_cx;
                    Update();
                    break;
                case ConsoleKey.LeftArrow:
                    if (_cx - 1 < 0)
                        _cx = INV_COL - 1;
                    else
                        --_cx;
                    Update();
                    break;

                case ConsoleKey.Escape:
                    Clear();
                    return false;

                    // ...
            }

            return true;
        }

        void Select()
        { //TODO: InventoryManager::Select()

        }

        /// <summary>
        /// Clear the inventory menu.
        /// </summary>
        void Clear()
        {
            string b = new string(' ', INV_WIDTH);
            int l = INV_HEIGHT + _my;
            for (int y = _my; y < l; ++y)
            {
                Console.SetCursorPosition(_mx, y);
                Console.Write(b);
            }
        }

        public Item this[int index]
        {
            get
            {
                return Items[index];
            }
        }

        public int Count => Items.Count;
    }
}
