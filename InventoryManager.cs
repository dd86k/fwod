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

        public void Show()
        {
            Draw();

            //TODO: Inventory instructions
            //Game.Log("");

            bool c = true;
            while (c = Entry());

            Clear();
        }

        unsafe void Draw()
        {
            _mx = (Utils.WindowWidth / 2) - INV_COL;
            _my = (Utils.WindowHeight / 2) - (INV_ROW * 2);
            
            Utils.GenerateGrill(_mx, _my, INV_COL, INV_ROW);

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
        { //TODO: ClearDescriptionBox(void)

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

                    // ...
            }

            return true;
        }

        void Clear()
        {

        }
    }
}
