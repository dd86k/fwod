using System;
using System.Collections.Generic;

/*TODO: Inventory mockup

+-+-+-+-+----------------+
|o|m| | | Potion         |
+-+-+-+-+                |
| | | | | Mysterious.    |
+-+-+-+-+                |
| | | | |                |
+-+-+-+-+ Heals 4 HP.    |
| | | | |                |
+-+-+-+-+----------------+
| Weapon: Flashy Sword   |
| Armour: Dented Meat    |
+------------------------+
 
*/

namespace fwod
{
    class InventoryManager
    {
        const int INV_ROW = 4;
        const int INV_COL = 10;
        const int MaximumNumberOfItems = INV_COL * INV_ROW;

        public Weapon Weapon { get; set; }
        public Armor Armor { get; set; }
        public List<Item> Items { get; }

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

            // Vertical length
            int vl = (INV_ROW * 2) + 3;

            Utils.GenerateCustomBox(
                _mx, // x
                vl, // y
                (INV_COL * 2) + 1, // w
                5, // h
                Borders.Bottom | Borders.Left | Borders.Right,
                Corners.BottomLeft | Corners.BottomRight
            );

            Console.SetCursorPosition(_mx + 1, vl + 2);
            Console.Write("W: " + Weapon.Name);
            Console.SetCursorPosition(_mx + 1, vl + 3);
            Console.Write("A: " + Armor.Name);
        }

        void Update()
        {

        }

        bool Entry()
        {
            ConsoleKeyInfo ck = Console.ReadKey(true);

            switch (ck.Key)
            {

            }

            return true;
        }

        void Clear()
        {

        }
    }
}
