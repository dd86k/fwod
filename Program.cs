using System;
using System.Collections.Generic;

/*
    Entry point of the program.
*/

//TODO: Error enumeration?

namespace fwod
{
    class MainClass
    {
        static readonly string ProjectVersion =
            $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Version}";
        static readonly string AssemblyName =
            $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}";

        /// <summary>
        /// Entry point.
        /// </summary>
        /// <param name="args">Arguments.</param>
        /// <returns>Error.</returns>
        internal static int Main(string[] args)
        {
            Game.MainPlayer =
                new Player((Utils.WindowWidth / 4) + (Utils.WindowWidth / 2), Utils.WindowHeight / 2);

            // Default values
            bool PlayIntro = true;

            // Applying CMD-like colors so it won't look weird later. (Mono fix)
            Console.ResetColor();

            try
            {
                // May crash on Mono.
                Console.CursorVisible = false;
            }
            finally { }

            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "/?":
                    case "--help":
                        ShowHelp();
                        return 0;

                    case "--version":
                        ShowVersion();
                        return 0;

                    case "-Pc":
                    case "-playerchar":
                        if (args[i + 1] != null)
                            Game.MainPlayer.Char = args[i + 1][0];
                        break;

                    case "-Pn":
                    case "-playername":
                        if (args[i + 1] != null)
                            Game.MainPlayer.Name = args[i + 1];
                        break;

                    case "-S":
                    case "-skipintro":
                        PlayIntro = false;
                        break;
#if DEBUG
                    case "--showmeme":
                        Misc.ShowMeme(); // :^)
                        return 0;
#endif
                }
            }

            // -- Before the game --
            Console.Clear();
            Console.Title = AssemblyName + " " + ProjectVersion;

            if (PlayIntro)
            {
                string BannerText = "* Welcome to " + AssemblyName + " *";
                string BannerOutline = new string('*', BannerText.Length);

                Utils.CenterAndWriteLine(BannerOutline);
                Utils.CenterAndWriteLine(BannerText);
                Utils.CenterAndWriteLine(BannerOutline);
                
                Console.WriteLine();
                Console.WriteLine("Every keystroke counts!");

#if DEBUG
                Console.WriteLine();
                Console.WriteLine("This is a development build, so expect bugs and crashes!");
#endif

                Console.WriteLine();
                Console.Write("Press a key to start!");

                Console.ReadKey(true);
                Console.Clear();
            }

            #region Initialization
            MapManager.Map = new char[Utils.WindowHeight, Utils.WindowWidth];

            // Generate the 'main' box
            MapManager.GenerateBox(
                1, 1, Utils.WindowWidth - 2, Utils.WindowHeight - 3
            );

            // Add stranger
            Person Stranger = new Person(
                Utils.WindowWidth / 4, Utils.WindowHeight / 2
            );
            Game.PeopleList = new List<List<Person>>();
            Game.PeopleList.Add(new List<Person>());
            Game.PeopleList[Game.CurrentFloor].Add(Stranger);

            // Initialize
            Game.MainPlayer.Initialize();

            Stranger.Char = 'S';
            Stranger.Initialize();
            
            Enemy TestRat = new Enemy(Utils.WindowWidth - 5, 5, EnemyType.Rat, 1);
            Game.PeopleList[Game.CurrentFloor].Add(TestRat);
            TestRat.Initialize();
            #endregion

            #region Intro
            if (PlayIntro)
            {
                Game.Log("Dialog...");

                Game.MainPlayer.Say("Ah! Where am I?");

                Stranger.Say("Oh, you're awake... What is your name?");
                
                if (Game.MainPlayer.Name == null) // Default name
                {
                    string tmp_name = null;
                    do
                    {
                        tmp_name = Game.MainPlayer.GetAnswer();
                        Stranger.Say("Say something!");
                    } while (tmp_name == null);
                    Game.MainPlayer.Name = tmp_name;
                }

                Game.MainPlayer.Say($"It's {Game.MainPlayer.Name}.");

                Stranger.Say($"Welcome to {AssemblyName}, {Game.MainPlayer.Name}. So...");

                Console.SetCursorPosition(27, 0);
                Console.Write("|");
                Game.MainPlayer.HP = 10;

                Stranger.Say("Here's your HP meter.");

                Game.MainPlayer.Say("My HP?");

                Stranger.Say("Health Points, obviously.");

                Game.MainPlayer.Say("Um.. Okay?");

                Console.SetCursorPosition(43, 0);
                Console.Write("|");
                Game.MainPlayer.Money = 12;

                Stranger.Say("And your money.");

                Game.MainPlayer.Say("Oh yeah, I forgot I have that much.");
                
                Stranger.Say($"I'll be back for you anyway, {Game.MainPlayer.Name}.");

                Game.MainPlayer.Say("Wait!");

                Stranger.Say("Time for me to go!");
                Stranger.Say("*POOF*");
                Stranger.Destroy();

                Game.MainPlayer.Say("I guess there's no helping him... Better get moving.");
            }
            else
            {
                Stranger.Destroy();
                Game.QuickInitialization();
            }
            #endregion

            do
            {
                Entry();
            } while (Game.IsPlaying);

            // -- The user is leaving the game --

            Console.Clear();

            return 0;
        }

        static void Entry()
        {
            ConsoleKeyInfo c = Console.ReadKey(true);

            switch (c.Key)
            {
                // Move up
                case ConsoleKey.UpArrow:
                    Game.MainPlayer.MoveUp();
                    break;

                // Move down
                case ConsoleKey.DownArrow:
                    Game.MainPlayer.MoveDown();
                    break;

                // Move left
                case ConsoleKey.LeftArrow:
                    Game.MainPlayer.MoveLeft();
                    break;

                // Move right
                case ConsoleKey.RightArrow:
                    Game.MainPlayer.MoveRight();
                    break;

                // Menu button
                case ConsoleKey.Escape:
                    Game.MainMenu.Show();
                    break;
            }

            // Game takes a turn.
            //Game.TakeTurn();
        }

        static void ShowHelp()
        {
            Console.WriteLine(" Usage:");
            Console.WriteLine("  fwod [<Options>]");
            Console.WriteLine();
            Console.WriteLine("  -Pc, -playerchar    Sets the player's character.");
            Console.WriteLine("  -Pn, -playername    Sets the player's name.");
            Console.WriteLine("  -S,  -skipintro     Skip intro and use defaults.");
#if DEBUG
            //Console.WriteLine("  --runtests          Run debugging tests.");
            //Console.WriteLine("  --speedtalktest     Run a speed dialog test.");
            //Console.WriteLine("  -say                Make the player say something and exit.");
            Console.WriteLine("  --showmeme          yeah");
#endif
            Console.WriteLine();
            Console.WriteLine("  --help, /?      Shows this screen");
            Console.WriteLine("  --version       Shows version");
            Console.WriteLine();
            Console.WriteLine("Have fun!");
        }

        static void ShowVersion()
        {
            Console.WriteLine(AssemblyName + " - " + ProjectVersion);
            Console.WriteLine("Copyright (c) 2015 DD~!/guitarxhero");
            Console.WriteLine("License: MIT License <http://opensource.org/licenses/MIT>");
            Console.WriteLine("Project page: <https://github.com/guitarxhero/fwod>");
            Console.WriteLine();
            Console.WriteLine(" -- Credits --");
            Console.WriteLine("DD~! (guitarxhero) - Original author");
        }
    }
}