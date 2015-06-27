using System;

namespace FWoD
{
    internal class Menu
    {
        string MenuLayout = "Exit";
        static internal void Show()
        {
            System.IO.TextWriter twMenu = new System.IO.TextWriter();
            Console.OpenStandardOutput(twMenu);


        }

        static internal void Hide()
        {

        }

        static internal void NextControl()
        {

        }

        static internal void PreviousControl()
        {

        }
    }
}