//#define DEBUGGER
using System;
using System.IO;
using System.Reflection;

/*
    This is the entry point of the program.
*/

//TODO: F12 for screenshot (Dump Game buffer to file?)

#if DEBUG
[assembly: AssemblyVersion("0.3.4.*")]
#else
[assembly: AssemblyVersion("0.3.4.0")]
#endif

namespace fwod
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
        static Player GameBoss = new Player(); //TODO: Dictionary for enemies in map/floor
        #endregion

        internal static int Main(string[] args)
        {
            // Default values
            string bosstext = string.Empty;
            char Pchar = '@';
            string Pname = "Player";
            bool SkipIntro = false;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;

            #if DEBUGGER
                args = new string[] { "--debug" };
            #endif

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
                    case "-B":
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
                    #if DEBUG
                    case "--debug":
                        int returnint = 0;
                        Debug.StartTests(ref returnint);
                        return returnint;
                    case "--debugsay":
                        if (args[i + 1] != null)
                        {
                            Debug.TalkTest(args[i + 1]);
                            return 0;
                        }
                        else return 1;
                    #endif
                }
            }

            //Console.Clear();
            Console.Title = ProjectName + " " + ProjectVersion;

            int w = (ConsoleTools.BufferWidth / 4) + (ConsoleTools.BufferWidth / 2);
            int h = ConsoleTools.BufferHeight / 2;

            GamePlayer = new Player((ConsoleTools.BufferWidth / 4) + (ConsoleTools.BufferWidth / 2), 
                ConsoleTools.BufferHeight / 2);
            GameBoss = new Player(ConsoleTools.BufferWidth / 4,
                ConsoleTools.BufferHeight / 2);

            // == Before the game ==

            string BannerText = "* Welcome to " + ProjectName + " *";
            string BannerOutline = ConsoleTools.RepeatChar('*', BannerText.Length);

            ConsoleTools.WriteLineAndCenter(Core.Layer.Game, BannerOutline);
            ConsoleTools.WriteLineAndCenter(Core.Layer.Game, BannerText);
            ConsoleTools.WriteLineAndCenter(Core.Layer.Game, BannerOutline);
            Console.WriteLine();

            #if DEBUG
                Console.WriteLine("This is a development build, so expect bugs and crashes!");
            #endif

            Console.WriteLine();
            Console.Write("Press a key to start!");

            Console.ReadKey(true);
            Console.Clear();

            // == Game starts here ==

            Core.FillScreen(Core.Layer.Game, ' ');
            Game.GenerateBox(Core.Layer.Game, Game.TypeOfLine.Double, 1, 1, ConsoleTools.BufferWidth - 2, ConsoleTools.BufferHeight - 3);

            GamePlayer.CharacterName = Pname;
            GamePlayer.CharacterChar = Pchar;
            GamePlayer.Initialize();
            GameBoss.CharacterChar = '#';
            GameBoss.Initialize();

            Game.UpdateEvent("You wake up after being defeated in battle");

            if (!SkipIntro)
            {
                GamePlayer.Say("Arrgh! Where am I?");

                GameBoss.Say("Oh, you're awake...");
                GameBoss.Say("What is your name?");

                string tmp_name = string.Empty;
                do
                {
                    tmp_name = GamePlayer.GetAnswer();
                    // If the player entered at least something and not too long
                    if (tmp_name.Length == 0)
                        GameBoss.Say("Say something!");
                    else if (tmp_name.Length > 25)
                        GameBoss.Say("Hey, that's way too long.");
                } while (tmp_name.Length == 0 || tmp_name.Length > 25);

                GamePlayer.CharacterName = tmp_name;
                GamePlayer.Say("It's " + GamePlayer.CharacterName + ".");

                GameBoss.Say("Well, it's your unlucky day.");

                GamePlayer.Say("Why?");

                GameBoss.Say("Because I will kill you.");

                GamePlayer.Say("WHAT!?!?!");

                if (bosstext.Length > 0)
                    GameBoss.Say(bosstext);
                else
                    GameBoss.Say("I'll be back for you, " + GamePlayer.CharacterName + "!");
            }

            bool isPlaying = true;
            do
            { // User is playing the game
                isPlaying = Entry();
            } while (isPlaying);

            // == The user is leaving the game ==

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

                    // Menu button
                case ConsoleKey.Escape:
                    Menu.Show();
                    break;
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
                " -bosssays, -B     Custom text from the Boss" + nl +
                "                   #P=Player name" + nl +
                " -playerchar, -C   Sets the player's character" + nl +
                " --skipintro       Skip intro and use defaults" + nl +
                nl +
                "  --help, /?    Shows this screen" + nl +
                "  --version     Shows version" + nl +
                nl +
                "Have fun!" + nl;
            
            Console.Write(Out);
        }

        static void ShowVersion()
        {
            // Should the credits be here or somewhere else?
            string Out = nl + "Version " + ProjectVersion + nl +
                nl +
                " -- Credits --" + nl +
                "DD~! (guitarxhero) - Original author" + nl +
                nl +
                "I'd like to thank the authors of Rogue, NetHack, and Pixel Dungeon for their awesome game and the many hours of fun I had in them." + nl;
            
            Console.Write(Out);
        }
    }
}