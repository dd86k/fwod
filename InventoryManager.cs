using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fwod
{
    class InventoryManager
    {
        const int INV_ROW = 4;
        const int INV_COL = 20;
        const int MaximumNumberOfItems = INV_COL * INV_ROW;

        public Weapon Weapon { get; set; }
        public Armor Armor { get; set; }
        public List<Item> Items { get; }

        public InventoryManager()
        {
            Items = new List<Item>();
        }

        public void Show()
        {
            Draw();

            Game.Log("Enter=Submenu");

            bool c = true;
            while (c = Entry());

            Clear();
        }

        unsafe void Draw()
        {
            int x = (Utils.WindowWidth / 2) - INV_COL;
            int y = (Utils.WindowHeight / 2) - (INV_ROW * 2);
            
            Utils.GenerateGrill(x, y, INV_COL, INV_ROW);

            Utils.GenerateCustomBox(
                x, // x
                (INV_ROW * 2) + 4, // y
                (INV_COL * 2) + 1, // w
                3, // h
                Borders.Bottom | Borders.Left | Borders.Right,
                Corners.BottomLeft | Corners.BottomRight
            );
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
