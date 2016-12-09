using System;
using System.Collections.Generic;

/*
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

/*
 * Inventory manager.
 */

namespace fwod
{
    class InventoryManager
    {
        const int INV_ROW = 4;
        const int INV_COL = 10;
        const int INV_MAX = INV_COL * INV_ROW;
        const int INV_WIDTH = 46;
        const int INV_HEIGHT = 12;

        public Weapon EquippedWeapon { get; set; }
        public Armor EquippedArmor { get; set; }
        List<Item> Items { get; }

        public bool HasWeapon => EquippedWeapon != null;
        public bool HasArmor => EquippedArmor != null;

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
            
            Game.Message("Press ESC to return.");
            
            while (Entry());

            Clear();
        }

        void Draw()
        {
            // Y position of the end of the grill.
            int by = (INV_ROW * 2) + 5;

            Utils.GenerateBox(_mx, _my, INV_WIDTH, INV_HEIGHT);

            Utils.GenerateGrill(_mx, _my, INV_COL, INV_ROW);

            Console.SetCursorPosition(
                _mx + (INV_WIDTH - _dw - 1),
                _my + (INV_HEIGHT - 6)
            );
            Console.Write(new string('─', _dw));

            Console.SetCursorPosition(_mx + 1, by);
            Console.Write("W: " + (HasWeapon ?
                EquippedWeapon.FullName.PadRight(INV_WIDTH - 5) : "None"));

            Console.SetCursorPosition(_mx + 1, by + 1);
            Console.Write("A: " + (HasArmor ?
                EquippedArmor.FullName.PadRight(INV_WIDTH - 5) : "None"));

            if (Items.Count > 0)
            {
                for (int i = 0, x = 0, y = 0, ix = 0; i < Items.Count; ++i, x += 2, ++ix)
                {
                    if (ix >= INV_COL)
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
                Console.Write(Items[od][0]);
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
                Console.Write(Items[d][0]);
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
            int t_y = _my + 1;
            Console.ResetColor();
            Console.SetCursorPosition(_dx, t_y);
            Console.Write(item);
            Console.SetCursorPosition(_dx, t_y += 2);
            
            if (item is Food)
            {
                Food a = item as Food;

                Console.Write($"Heals {a.RestorePoints} HP.");
            }
            else if (item is Weapon)
            {
                Weapon a = item as Weapon;
                
                Console.Write($"Deals {a.Damage} AP.");
            }
            else if (item is Armor)
            {
                Armor a = item as Armor;
                
                Console.Write($"Protects for {a.ArmorPoints} AP.");
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
        {
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

                case ConsoleKey.Enter:
                    Select();
                    break;
            }

            return true;
        }

        void Select()
        {
            int d = (INV_COL * _cy) + _cx;

            if (d < Items.Count)
            {
                Item item = Items[d];
                string acstr;
                Action a;
                if (item is Food)
                {
                    acstr = "Consume";
                    a = () =>
                    {
                        Items.Remove(item);
                        Game.MainPlayer.HP += (item as Food).RestorePoints;
                    };
                }
                else
                {
                    acstr = "Equip";
                    if (item is Weapon)
                        a = () =>
                        {
                            EquippedWeapon = item as Weapon;
                            Items.Remove(item);
                        };
                    else
                        a = () =>
                        {
                            EquippedArmor = item as Armor;
                            Items.Remove(item);
                        };
                }

                Menu m = new Menu(
                    true, false,
                    new MenuItem(item),
                    new MenuItem(),
                    new MenuItem(acstr, a, MenuItemType.Return), //TODO: Fix inventory exit draw
                    new MenuItem("Drop"),
                    new MenuItem(),
                    new MenuItem("Cancel", MenuItemType.Return)
                );
                Draw();
            }
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

        public Item this[int index] => Items[index];

        public int Count => Items.Count;
    }
}
