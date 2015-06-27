using System;
using System.Reflection;

/*
    This is the entry point of the program.
*/

//TODO: Collision mechanics
//TODO: Enemy mechanics
//TODO: Attack mechanics

//TODO: Do menu (Options, Savegames, Quit, etc.) - Needs Buffer first!

//TODO: F12 for screenshot (Dump display buffer to file)

#if DEBUG
[assembly: AssemblyVersion("0.3.3.*")]
#else
[assembly: AssemblyVersion("0.3.3.0")]
#endif

namespace FWoD
{
    class MainClass
    {
        #region Consts
        const string ProjectName = "Four Walls of Death";
        #endregion

        #region Properties
        static readonly string nl = System.Environment.NewLine;
        static string ProjectVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        static Player GamePlayer = new Player();
        static Enemy GameBoss = new Enemy(); //TODO: 1D Array for enemies [in Game]? (!!!)
        static bool isPlaying = true; // Is the player playing?
        static bool inMenu = false;
        #endregion

        internal static int Main(string[] args)
        {
            // Default values
            string bosstext = string.Empty;
            char Pchar = '@';
            string Pname = "Player";
            bool SkipIntro = false;

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
                    case "-bosssays":
                    case "-B": //TODO: Find spot for custom text
                        if (args[i + 1] != null)
                            bosstext = args[i + 1];
                        break;
                    case "-playerchar":
                    case "-C":
                        if (args[i + 1] != null)
                            Pchar = args[i + 1][0];
                        break;
                    case "--skipintro":
                        SkipIntro = true;
                        break;
                    case "--showmeme":
                        Misc.ShowMeme(); // *shrugs*
                        return 0;
                    case "--debug":
                        int returnint = 0;
                        Debug.StartTests(ref returnint);
                        return returnint;
                }
            }

            Console.Clear();
            Console.Title = ProjectName + " " + ProjectVersion;
            Console.CancelKeyPress += Console_CancelKeyPress;
            
            #if WINDOWS
                Console.SetBufferSize(80, 25); // Window's default
                Console.SetWindowSize(80, 25); // Remove scrollbar for maximum yes
            #elif WINDOWS10
                // Crashes on SetBufferSize (because it's lower, thx ms)
                
            #elif LINUX
            //TODO: [LINUX] Find a way to set the Window or buffersize (no libs pls)
            //"dude terminal doesn't have a 'buffer' its a real terminal rofl!!11"

            #endif

            GamePlayer = new Player((ConsoleTools.BufferWidth / 4) + (ConsoleTools.BufferWidth / 2), 
                ConsoleTools.BufferHeight / 2);

            // == Before the game ==

            string BannerText = "* Welcome to " + ProjectName + " *";
            string BannerOutline = ConsoleTools.RepeatChar('*', BannerText.Length);

            ConsoleTools.WriteLineAndCenter(BannerOutline);
            ConsoleTools.WriteLineAndCenter(BannerText);
            ConsoleTools.WriteLineAndCenter(BannerOutline);
            Console.WriteLine();
            /* Some kind of lore/context/pre-story going on or something. */
            /*
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            */

            #if DEBUG
                Console.WriteLine("This is a development build, so expect bugs and crashes!");
            #endif

            Console.WriteLine();
            Console.Write("Press a key to start!");

            Console.ReadKey(true);
            Console.Clear();

            // == Game starts here ==

            Game.GenerateBox(Game.TypeOfLine.Double, 1, 1, ConsoleTools.BufferWidth - 2, ConsoleTools.BufferHeight - 2);

            GamePlayer.CharacterName = Pname;
            GamePlayer.CharacterChar = Pchar;
            GamePlayer.Initialize();
            GameBoss.Initialize();

            //TODO: Place scenary here


            if (!SkipIntro)
            {
                GamePlayer.PlayerSays("Arrgh! Where am I?");

                GameBoss.EnemySays("Oh, you're awake...");
                GameBoss.EnemySays("What is your name?");

                string tmp_name = string.Empty;
                do
                {
                    tmp_name = GamePlayer.PlayerAnswer();
                    // If the player entered at least something and not too long
                    if (tmp_name.Length == 0)
                        GameBoss.EnemySays("Say something!");
                    else if (tmp_name.Length > 25)
                        GameBoss.EnemySays("Hey, that's way too long.");
                } while (tmp_name.Length == 0 || tmp_name.Length > 25);

                GamePlayer.CharacterName = tmp_name;
                GamePlayer.PlayerSays("It's " + GamePlayer.CharacterName + ".");

                GameBoss.EnemySays("Well, it's your unlucky day.");

                GamePlayer.PlayerSays("Why?");

                GameBoss.EnemySays("Because I will kill you.");

                GamePlayer.PlayerSays("WHAT!?!?!");

                if (bosstext.Length > 0)
                    GameBoss.EnemySays(bosstext);
                else
                    GameBoss.EnemySays("I'll be back for you, " + GamePlayer.CharacterName + "!");
            }

            do
            {
                Entry();
            } while (isPlaying);

            // == The player is leaving the game ==

            Console.Clear();

            return 0;
        }

        static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Console.Clear();
        }

        static internal void Entry()
        { // Make a bool like isInMenu
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

                    // Menu button
                case ConsoleKey.Escape:
                    if (inMenu)
                    {
                        Menu.Hide();
                        inMenu = false;
                    }
                    else
                    {
                        Menu.Show();
                        inMenu = true;
                    }
                    break;
            }
        }

        static void ShowHelp()
        {
            string Out = nl + "Usage:" + nl +
                #if WINDOWS
                    " fwod-win32 [options]" + nl + nl +
                #elif LINUX
                    " fwod-linux [options]" + nl + nl +
                #endif
                " -bosssays, -B     Custom text from the Boss" + nl +
                "                   #P=Player name" + nl +
                " -playerchar, -C   Sets the player's character" + nl +
                " --skipintro       Skip intro and use defaults" + nl +
                nl +
                "  --help, /?    Shows this screen" + nl +
                "  --version     Shows version" + nl + nl +
                "Have fun!" + nl;
            
            Console.Write(Out);
        }

        static void ShowVersion()
        {
            // Should the credits be here or somewhere else?
            string Out = nl + "Version " + ProjectVersion + nl + nl +
                " -- Credits --" + nl +
                "DD~! (guitarxhero) - Original author" + nl + nl +
                "I'd like to thank the authors of Rogue, NetHack, and Pixel Dungeon for their awesome game and the many hours of fun I had in them." + nl;
            
            Console.Write(Out);
        }
    }
}