using System;
using System.Collections.Generic;

/*TODO: Inventory mockup
4x10 spaces, 1 character horizontal padding

|-------------------46-----------------------|
|--------21---------|
                    |---------- 26-----------|

+-+-+-+-+-+-+-+-+-+-+------------------------+
|o|m| | | | | | | | | Potion                 |
+-+-+-+-+-+-+-+-+-+-+                        |
| | | | | | | | | | | Heals 10 HP.           |
+-+-+-+-+-+-+-+-+-+-+                        |
| | | | | | | | | | |                        |
+-+-+-+-+-+-+-+-+-+-+                        |
| | | | | | | | | | |                        |
+-+-+-+-+-+-+-+-+-+-+------------------------+
| Weapon: Flashy Sword                       |
| Armour: Dented Meat                        |
+--------------------------------------------+
*/

namespace fwod
{
    class InventoryManager
    {
        const int INV_ROW = 4;
        const int INV_COL = 10;
        const int INV_MAX = INV_COL * INV_ROW;
        const int INV_WIDTH = 46;

        public Weapon Weapon { get; set; }
        public Armor Armor { get; set; }
        List<Item> Items { get; }

        // Menu location on screen
        int _mx, _my;
        // Menu dimension
        int _mw, _mh;
        // Cursor location in the menu
        int _cx, _cy;

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
            _mx = (Utils.WindowWidth / 2) - INV_COL;
            _my = (Utils.WindowHeight / 2) - (INV_ROW * 2);

            Draw();

            //TODO: Inventory instructions
            //Game.Log("");
            
            while (Entry());

            Clear();
        }

        unsafe void Draw()
        {
            // Y position of the end of the grill.
            int by = (INV_ROW * 2) + 5;

            Utils.GenerateCustomBox(
                _mx, // x
                by, // y
                (INV_COL * 2) + 1, // w
                5, // h
                Borders.Bottom | Borders.Left | Borders.Right,
                Corners.BottomLeft | Corners.BottomRight
            );

            Utils.GenerateGrill(_mx, _my, INV_COL, INV_ROW);

            Console.SetCursorPosition(_mx + 1, by);
            Console.Write("W: " + Weapon.Name);
            Console.SetCursorPosition(_mx + 1, by + 1);
            Console.Write("A: " + Armor.Name);
        }

        void Update()
        { //TODO: Update(void)
            // 1 Dimensional indexer
            int d = (INV_ROW * _cy) + _cx;


        }

        void ClearDescriptionBox()
        { //TODO: ClearDescriptionBox()

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

                    // ...
            }

            return true;
        }

        void Select()
        { //TODO: InventoryManager::Select()

        }

        void Clear()
        { //TODO: InventoryManager::Clear()

        }

        public Item this[int index]
        {
            get
            {
                return Items[index];
            }
        }
    }
}
