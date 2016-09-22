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
| | | | | | | | | | |                        | 12
+-+-+-+-+-+-+-+-+-+-+                        | |
| | | | | | | | | | |                        | |
+-+-+-+-+-+-+-+-+-+-+------------------------+ |
| Weapon: Flashy Sword                       | |
| Armour: Dented Meat                        | |
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

        /*
            These numbers are saved to avoid re-calculating
            them every time.
        */

        // Menu location on screen
        int _mx, _my;
        // Cursor location in the menu
        int _cx, _cy;
        // Past cursor location.
        int _ocx, _ocy;
        // Description inner box width
        int _dw;
        // Description inner X
        int _dx;

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
            _dw = INV_WIDTH - (INV_COL * 2) - 2;
            _dx = _mx + (INV_COL * 2) + 2;

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

            Console.SetCursorPosition(_mx + (INV_WIDTH - _dw - 1), _my + (INV_HEIGHT - 6));
            Console.Write(new string('─', _dw));

            Console.SetCursorPosition(_mx + 1, by);
            Console.Write("W: " + EquippedWeapon.FullName);
            Console.SetCursorPosition(_mx + 1, by + 1);
            Console.Write("A: " + EquippedArmor.FullName);

            if (Items.Count > 0)
            {
                for (int i = 0, x = 0, y = 0, ix = 0; i < Items.Count; ++i, x += 2, ++ix)
                {
                    if (ix + 1 > INV_COL)
                    {
                        y += 2;
                        x = ix = 0;
                    }
                    Console.SetCursorPosition(_mx + x + 1, _my + y + 1);
                    Console.Write(Items[i].ToString()[0]);

                }
            }

            Update();
        }

        void Update()
        {
            // 1 Dimensional indexer
            int  d = (INV_COL * _cy) + _cx,
                od = (INV_COL * _ocy) + _ocx;

            ClearDescriptionBox();

            // Clear old selected
            Console.SetCursorPosition(_mx + (_ocx * 2) + 1, _my + (_ocy * 2) + 1);
            if (od < Items.Count)
            {
                Console.Write(Items[od].ToString()[0]);
                //InsertDescription(Items[od]);
            }
            else
            {
                Console.Write(' ');
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
            }

            Console.ResetColor();

            _ocx = _cx;
            _ocy = _cy;
        }

        void InsertDescription(Item item)
        {
            Console.ResetColor();
            Console.SetCursorPosition(_dx, _my + 1);

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
        {
            string b = new string(' ', _dw);
            int ym = _my + (INV_ROW * 2);
            for (int i = _my + 1; i < ym; ++i)
            {
                Console.SetCursorPosition(_dx - 1, i);
                Console.Write(b);
            }
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
