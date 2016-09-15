using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fwod
{
    class Inventory
    {
        int INV_ROW = 4;
        int INV_COL = 10;

        public List<Item> Items { get; }

        public Inventory()
        {
            Items = new List<Item>();
        }

        public void Show()
        {

        }
    }
}
