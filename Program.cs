using System;

//TODO: Wall collision mechanics
//TODO: Enemy mechanics
//TODO: Attack mechanics

namespace Play
{
    class MainClass
    {
        const string Version = "0.2.3";
        const string Name = "Four Walls of Death"; // Four walls of death

        static readonly string nl = System.Environment.NewLine;

        static Player GamePlayer = new Player();
        static Enemy GameBoss = new Enemy();
        internal static int Main(string[] args)
        {
            string bosstext = string.Empty;
            char Pchar = '@';
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "/?":
                    case "--help":
                        ShowHelp();
                        //System.Environment.Exit(0);
                        //break;
                        return 0;
                    case "--version":
                        ShowVersion();
                        //System.Environment.Exit(0);
                        //break;
                        return 0;
                    case "--bosssays":
                        if (args[i + 1] != null)
                            bosstext = args[i + 1];
                        break;
                    case "--playerchar":
                        if (args[i + 1] != null)
                            Pchar = args[i + 1][0];
                        break;
                }
            }

            Console.Clear();
            Console.Title = Name + " " + Version;
            
            #if WINDOWS // Otherwise the scrollbar is still there
                Console.SetBufferSize(80, 25);
                Console.SetWindowSize(80, 25); // Window's default
            #endif

            // == Before the game ==

            ConsoleTools.WriteLineAndCenter("**********************************"); // temporary
            ConsoleTools.WriteLineAndCenter("* Welcome to " + Name + " *");
            ConsoleTools.WriteLineAndCenter("**********************************");
            Console.WriteLine();

            // Player information gathering
            Console.Write("Character's name (default=Player): ");
            string Pname = Console.ReadLine();
            Pname = (Pname.Length > 0 ? Pname : "Player");
            Pname = (Pname.Length > 25 ? Pname.Substring(0, 25) : Pname);

            Console.Clear();

            // == Game starts here ==

            GamePlayer.CharacterName = Pname;
            GamePlayer.CharacterChar = Pchar;
            GamePlayer.Initialize();
            GameBoss.Initialize();


            if (bosstext.Length > 0)
            {
                bosstext = bosstext.Replace("#P", Pname);
                GameBoss.EnemySays(bosstext); // Extra player defined text
            }
            else
                GameBoss.EnemySays("Die, " + Pname + "!");

            GamePlayer.PlayerSays("We'll see about that!");
            
            bool plays = true;
            do
            {
                plays = Entry();
            } while (plays);

            // == The player is leaving the game ==

            Console.Clear();

            return 0;
        }

        static internal bool Entry()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                    // Move up
                case ConsoleKey.UpArrow:
                    GamePlayer.MoveUp();
                    break;

                    // Move down
                case ConsoleKey.DownArrow:
                    GamePlayer.MoveDown();
                    break;

                    // Move left
                case ConsoleKey.LeftArrow:
                    GamePlayer.MoveLeft();
                    break;

                    // Move right
                case ConsoleKey.RightArrow:
                    GamePlayer.MoveRight();
                    break;

                    // Exit the game
                case ConsoleKey.Escape:
                    return false;
            }

            return true;
        }

        static void ShowHelp()
        {
            string Out = nl + "Usage:" + nl +
                #if WINDOWS
                " fwod-win32 [options]" + nl + nl +
                #elif LINUX
                " fwod-linux [options]" + nl + nl +
                #endif
                " --bosssays     Custom text from the Boss" + nl +
                "                #P=Player name" + nl +
                " --playerchar   Sets the player's character" + nl +
                nl +
                "  --help, /?    Shows this screen" + nl +
                "  --version     Shows version" + nl + nl +
                "Have fun!" + nl;
            Console.Write(Out);
        }

        static void ShowVersion()
        {
            Console.Write("Made by DD~!, version " + Version);
        }
    }
}