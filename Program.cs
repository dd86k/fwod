using System;
using System.Reflection;

/*
    This is the entry point of the program.
*/

#if DEBUG
[assembly: AssemblyVersion("0.3.5.*")]
#else
[assembly: AssemblyVersion("0.3.5.0")]
#endif

namespace fwod
{
    class MainClass
    {
        #region Constants
        const string ProjectName = "Four Walls Of Death";
        #endregion

        #region Properties
        static readonly string nl = System.Environment.NewLine;
        static readonly string ProjectVersion =
                Assembly.GetExecutingAssembly().GetName().Version.ToString();
        static internal bool isPlaying = true;
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

                    case "--bosssays":
                    case "-B":
                        if (args[i + 1] != null)
                            bosstext = args[i + 1];
                        break;

                    case "--playerchar":
                    case "-C":
                        if (args[i + 1] != null)
                            Pchar = args[i + 1][0];
                        break;

                    case "--skipintro":
                    case "-S":
                        SkipIntro = true;
                        break;

                    #if DEBUG
                    case "--showmeme":
                        Misc.ShowMeme(); // *shrugs*
                        return 0;

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

            // -- Before the game --

            Console.Clear();
            Console.Title = ProjectName + " " + ProjectVersion;

            string BannerText = "* Welcome to " + ProjectName + " *";
            string BannerOutline = ConsoleTools.RepeatChar('*', BannerText.Length);

            ConsoleTools.WriteLineAndCenter(BannerOutline);
            ConsoleTools.WriteLineAndCenter(BannerText);
            ConsoleTools.WriteLineAndCenter(BannerOutline);
            Console.WriteLine();

            #if DEBUG
                Console.WriteLine("This is a development build, so expect bugs and crashes!");
            #endif

            Console.WriteLine();
            Console.Write("Press a key to start!");

            Console.ReadKey(true);
            Console.Clear();

            // -- Game starts here --

            // Add player and first enemy in game
            Game.MainPlayer = new Player((ConsoleTools.BufferWidth / 4) + (ConsoleTools.BufferWidth / 2), 
                ConsoleTools.BufferHeight / 2);
            Game.EnemyList.Add(new Player(ConsoleTools.BufferWidth / 4,
                ConsoleTools.BufferHeight / 2));

            // Generate the 'main' box
            Game.GenerateBox(Core.Layer.Game, Game.TypeOfLine.Double, 1, 1, ConsoleTools.BufferWidth - 2, ConsoleTools.BufferHeight - 3);

            // Set player stuff
            Game.MainPlayer.CharacterName = Pname;
            Game.MainPlayer.CharacterChar = Pchar;
            Game.MainPlayer.Initialize();
            Game.EnemyList[0].CharacterChar = '#';
            Game.EnemyList[0].Initialize();

            Game.UpdateEvent("You wake up in a strange place");

            if (!SkipIntro)
            {
                Game.MainPlayer.Say("Ah! Where am I?");

                Game.EnemyList[0].Say("Oh, you're awake...");
                Game.EnemyList[0].Say("What is your name?");

                string tmp_name = string.Empty;
                do
                {
                    tmp_name = Game.MainPlayer.GetAnswer();

                    // If the player entered at least something and not too long
                    if (tmp_name.Length == 0)
                        Game.EnemyList[0].Say("Say something!");

                } while (tmp_name.Length == 0);

                Game.MainPlayer.CharacterName = tmp_name;
                Game.MainPlayer.Say("It's " + Game.MainPlayer.CharacterName + ".");

                Game.EnemyList[0].Say("So, welcome to The Dungeon.");

                Game.MainPlayer.Say("The what now?");

                Game.EnemyList[0].Say("We'll meet on the last floor.");

                Game.MainPlayer.Say("Um.. Okay?");

                if (bosstext.Length > 0)
                    Game.EnemyList[0].Say(bosstext);
                else
                    Game.EnemyList[0].Say("I'll be back for you, " + Game.MainPlayer.CharacterName + "!");

                //TODO: Make enemy walk to next floor and disapear

            }

            do
            { // User is playing the game
                Entry();
            } while (isPlaying);

            // -- The user is leaving the game --

            Console.Clear();

            return 0;
        }

        static internal void Entry()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
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
                    Menu.Show();
                    break;
            }

            // Steps a "turn" since this is turn based.. ish
            //Game.TakeTurn();
        }

        static void ShowHelp()
        {
            string Out = "Usage:" + nl +
                    " fwod[.exe] [options]" + nl + nl +
                " --bosssays, -B     Custom text from the Boss" + nl +
                " --playerchar, -C   Sets the player's character" + nl +
                " --skipintro, -S    Skip intro and use defaults" + nl +
                nl +
                "  --help, /?      Shows this screen" + nl +
                "  --version       Shows version" + nl +
                nl +
                "Have fun!";
            
            Console.Write(Out);
        }

        static void ShowVersion()
        {
            string Out = ProjectName + " " + ProjectVersion + nl +
                "Copyright (c) 2015 DD~!/guitarxhero" + nl +
                "License: MIT License <http://opensource.org/licenses/MIT>" + nl +
                nl +
                " -- Credits --" + nl +
                "DD~! (guitarxhero) - Original author";
            
            Console.Write(Out);
        }
    }
}