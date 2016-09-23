using System;
using System.Reflection;

/*
    Entry point of the program.
*/

//TODO: Fatal Error enumeration?
//TODO: StartMenu
//TODO: Consider making modifiers a flag (???)
//TODO: SettingsManager (with scrolling page)

namespace fwod
{
    class Program
    {
        static readonly AssemblyName AssemblyInfo =
            Assembly.GetExecutingAssembly().GetName();


        public static int Main(string[] args)
        {
            // Default values
            bool PlayIntro = true;

            // Applying CMD-like colors so it won't look weird later. (Mono fix)
            Console.ResetColor();

#if DEBUG
            args = new string[] { "-S" };
#endif

            try
            {
                // May crash on Mono.
                Console.CursorVisible = false;
            }
            catch { }

            Game.MainPlayer = new Player(44, 12);
            
            for (int i = 0; i < args.Length; ++i)
            {
                switch (args[i])
                {
                    case "/?":
                    case "-h":
                    case "--help":
                        ShowHelp();
                        return 0;

                    case "--version":
                        ShowVersion();
                        return 0;

                    case "-Pc":
                    case "-playerchar":
                        if (i + 1 < args.Length)
                            Game.MainPlayer.Char = args[i + 1][0];
                        break;

                    case "-Pn":
                    case "-playername":
                        if (i + 1 < args.Length)
                            Game.MainPlayer.Name = args[i + 1];
                        break;

                    case "-S":
                    case "-skipintro":
                        PlayIntro = false;
                        break;

                    /*case "-L":
                    case "-load":

                        break;*/

                    case "--showmeme":
                        Misc.Wunk(); // :^)
                        return 0;
                        
                    case "-tsay":
                        if (i + 1 < args.Length)
                        {
                            Console.Clear();
                            new Person(
                                Console.WindowWidth / 2,
                                Console.WindowHeight / 2,
                                c: Game.MainPlayer.Char,
                                init: true
                            ).Say(args[i + 1], true, false);
                        }
                        return 0;
                }
            }

            // -- Before the game --
            Console.Clear();
#if DEBUG
            Console.Title = $"{AssemblyInfo.Name} {AssemblyInfo.Version}-dev";
#else
            Console.Title = $"{AssemblyInfo.Name} {AssemblyInfo.Version}";
#endif
            MapManager.Map = new char[Utils.WindowHeight, Utils.WindowWidth];

            if (PlayIntro)
            {
                string BannerText = $"* Welcome to {AssemblyInfo.Name} *";
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
            
            // Generate the 'main' box
            MapManager.GenerateBox(30, 10, 20, 5);

            // Add stranger

            // Initialize
            Game.MainPlayer.Initialize();
            Game.People = new PeopleManager();


            Game.People[0].Add(new Enemy(47, 13, EnemyType.Rat, 1));

#region Intro
            if (PlayIntro)
            {
                Game.People.CreatePerson(34, 12, 1, 'S', "Stranger");

                Game.Log("Dialog...");

                Game.MainPlayer.Say("Ah! Where am I?");

                Person Stranger = Game.People["Stranger"];

                Stranger.Say("Oh, you're awake... What's your name?");

                if (Game.MainPlayer.Name == null) // Default name
                {
                    string tmp_name = null;
                    do
                    {
                        tmp_name = Game.MainPlayer.GetAnswer();
                        Stranger.Say("Say something!");
                    } while (tmp_name.Length == 0);
                    Game.MainPlayer.Name = tmp_name;
                }
                else
                {
                    Console.SetCursorPosition(1, 0);
                    Console.Write(Game.MainPlayer.Name);
                }

                Game.MainPlayer.Say($"It's {Game.MainPlayer.Name}.");

                Stranger.Say($"Ah. Welcome to {AssemblyInfo.Name}, {Game.MainPlayer.Name}. So...");

                Console.SetCursorPosition(27, 0);
                Console.Write("|");
                Game.MainPlayer.HP = 10;

                Stranger.Say("Here's your HP.");

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
                Stranger.Destroy();

                Game.MainPlayer.Say("How the heck did he vanish?");
                Game.MainPlayer.Say("I guess there's no helping him anyway... Better get moving.");
                Game.Log("Use the arrow keys to navigate.");
            }
            else
            {
                Game.QuickIntro();
            }

#if DEBUG
            Game.MainPlayer.Inventory.AddItem(new Food(FoodType.Energy_Drink));
            Game.MainPlayer.Inventory.AddItem(new Armor(ArmorType.Body_Armor));
            Game.MainPlayer.Inventory.AddItem(new Armor(ArmorType.Body_Armor));
            Game.MainPlayer.Inventory.AddItem(new Armor(ArmorType.Body_Armor));
            Game.MainPlayer.Inventory.AddItem(new Armor(ArmorType.Body_Armor));
            Game.MainPlayer.Inventory.AddItem(new Armor(ArmorType.Body_Armor));
            Game.MainPlayer.Inventory.AddItem(new Armor(ArmorType.Body_Armor));
            Game.MainPlayer.Inventory.AddItem(new Armor(ArmorType.Body_Armor));
            Game.MainPlayer.Inventory.AddItem(new Armor(ArmorType.Body_Armor));
            Game.MainPlayer.Inventory.AddItem(new Armor(ArmorType.Body_Armor));
            Game.MainPlayer.Inventory.AddItem(new Armor(ArmorType.Body_Armor));
            Game.MainPlayer.Inventory.AddItem(new Armor(ArmorType.Body_Armor));
            Game.MainPlayer.Inventory.AddItem(new Armor(ArmorType.Body_Armor));
            Game.MainPlayer.Inventory.AddItem(new Armor(ArmorType.Body_Armor));
            Game.MainPlayer.Inventory.AddItem(new Armor(ArmorType.Body_Armor));
#endif
#endregion

            // Player controls the game
            while (Entry());

            Console.Clear();

            return 0;
        }

        static bool Entry()
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

                // Select
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    //TODO: Select()

                    break;

                // Menu button
                case ConsoleKey.Escape:
                    Game.MainMenu.Show();
                    return Game.MainMenu.Response != MenuResponse.Quit;
            }

            // People take a turn.
            //Game.TakeTurn();

            return true;
        }

        static void ShowHelp()
        {
            Console.WriteLine(" Any requested help ");
            Console.WriteLine(" Usage:");
            Console.WriteLine("  fwod [<Options>]");
            Console.WriteLine();
            Console.WriteLine("  -Pc, -playerchar    Sets the player's character.");
            Console.WriteLine("  -Pn, -playername    Sets the player's name.");
            Console.WriteLine("  -S,  -skipintro     Skip intro and use defaults.");
            //Console.WriteLine("  --runtests          Run debugging tests.");
            //Console.WriteLine("  --speedtalktest     Run a speed dialog test.");
            Console.WriteLine();
            Console.WriteLine("  --help, /?      Shows this screen and exit.");
            Console.WriteLine("  --version       Shows version and exit.");
            Console.WriteLine();
            Console.WriteLine("Have fun!");
        }

        static void ShowVersion()
        {
#if DEBUG
            Console.WriteLine($"{AssemblyInfo.Name} - {AssemblyInfo.Version}-dev");
#else
            Console.WriteLine($"{AssemblyInfo.Name} - {AssemblyInfo.Version}");
#endif
            Console.WriteLine("Copyright (c) 2015 DD~!/guitarxhero");
            Console.WriteLine("License: MIT License <http://opensource.org/licenses/MIT>");
            Console.WriteLine("Project page: <https://github.com/guitarxhero/fwod>");
            Console.WriteLine();
            Console.WriteLine(" -- Credits --");
            Console.WriteLine("DD~! (guitarxhero) - Original author");
        }
    }
}