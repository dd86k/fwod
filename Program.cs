using System;

/*
    Entry point of the program.
*/

#if DEBUG
[assembly: System.Reflection.AssemblyVersion("0.3.6.*")]
#else
[assembly: System.Reflection.AssemblyVersion("0.3.6.0")]
#endif

namespace fwod
{
    /// <summary>
    /// Program entry point
    /// </summary>
    class MainClass
    {
        #region Constants
        const string ProjectName = "Four Walls Of Death";
        #endregion

        #region Properties
        static readonly string ProjectVersion =
                string.Format("{0}",
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
        #endregion

        internal static int Main(string[] args)
        {
            // Default values
            string bosstext = string.Empty;
            char Pchar = '@';
            string Pname = "Player ";
            bool SkipIntro = false;

            // Applying CMD-like colors so it won't look weird later.
            Console.ForegroundColor = ConsoleTools.OriginalForegroundColor;
            Console.BackgroundColor = ConsoleTools.OriginalBackgroundColor;

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

                    case "-B":
                    case "--bosssays":
                        if (args[i + 1] != null)
                            bosstext = args[i + 1];
                        break;

                    case "-Pc":
                    case "--playerchar":
                        if (args[i + 1] != null)
                            Pchar = args[i + 1][0];
                        break;

                    case "-Pn":
                    case "--playername":
                        if (args[i + 1] != null)
                            Pname = args[i + 1];
                        break;

                    case "-S":
                    case "--skipintro":
                        SkipIntro = true;
                        break;

#if DEBUG
                    case "--showmeme":
                        Misc.ShowMeme(); // :^)
                        return 0;

                    case "--runtests":
                        int returnint = 0;
                        Debug.StartTests(ref returnint);
                        return returnint;

                    case "--say":
                        if (args[i + 1] != null)
                        {
                            Debug.TalkTest(args[i + 1]);
                            return 0;
                        }
                        else return 1;

                    case "--speedtalktest":
                        Debug.SpeedTalkTest();
                        return 0;
#endif
                }
            }

            // -- Before the game --
            Console.Clear();
            Console.Title = "fwod " + ProjectVersion;

            if (!SkipIntro)
            {
                string BannerText = "* Welcome to " + ProjectName + " *";
                string BannerOutline = new string('*', BannerText.Length);

                ConsoleTools.WriteLineAndCenter(BannerOutline);
                ConsoleTools.WriteLineAndCenter(BannerText);
                ConsoleTools.WriteLineAndCenter(BannerOutline);
                Console.WriteLine();

                Console.WriteLine("[Insert shitty lore here]");

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

            // -- Game starts here --

            // Add player and first enemy in game
            Game.MainPlayer = new Player((ConsoleTools.WindowWidth / 4) + (ConsoleTools.WindowWidth / 2),
                ConsoleTools.WindowHeight / 2);
            Person Stranger = new Person(ConsoleTools.WindowWidth / 4, ConsoleTools.WindowHeight / 2);
            Game.PeopleList.Add(Stranger);

            // Generate the 'main' box
            Game.GenerateBox(Core.Layer.Game, Game.TypeOfLine.Double, 1, 1,
                ConsoleTools.WindowWidth - 2, ConsoleTools.WindowHeight - 3);

            // Set player stuff
            Game.MainPlayer.CharacterName = Pname;
            Game.MainPlayer.CharacterChar = Pchar;
            Game.MainPlayer.Initialize();

            Stranger.CharacterChar = 'S';
            Stranger.Initialize();

#warning Test
            Enemy TestRat = new Enemy(5, 5, Enemy.EnemyType.Rat, 24);
            Game.EnemyList.Add(TestRat);
            TestRat.Initialize();

            #region Intro
            if (!SkipIntro)
            {
                Game.DisplayEvent("Dialog...");

                Game.MainPlayer.Say("Ah! Where am I?");

                Stranger.Say("Oh, you're awake... What is your name?");

                if (Pname == "Player ")
                {
                    string tmp_name = null;

                    do
                    {
                        tmp_name = Game.MainPlayer.GetAnswer();

                        Stranger.Say("Say something!");
                    } while (tmp_name == null);

                    Game.MainPlayer.CharacterName = tmp_name;
                }

                Game.MainPlayer.Say("It's " + Game.MainPlayer.CharacterName + ". Jeez, calm down.");

                Stranger.Say("So, welcome to " + ProjectName + ".");

                Console.SetCursorPosition(27, 0);
                Console.Write("|");
                Game.MainPlayer.HP = 10;

                Stranger.Say("Here's your HP meter.");

                Game.MainPlayer.Say("My HP?");

                Stranger.Say("Health Points, yo.");

                Game.MainPlayer.Say("Um.. Okay?");

                Console.SetCursorPosition(41, 0);
                Console.Write("|");
                Game.MainPlayer.Money = 12;

                Stranger.Say("And your money.");

                Game.MainPlayer.Say("Oh yeah, I forgot I have that much.");

                if (bosstext.Length > 0)
                    Stranger.Say(bosstext);
                else
                    Stranger.Say("I'll be back for you anyway, " + Game.MainPlayer.CharacterName + ".");

                Game.MainPlayer.Say("Wait!");

                Stranger.Say("Time for me to go!");
                Stranger.Say("[POOF]");
                Stranger.Destroy();

                Game.MainPlayer.Say("I guess there's no helping him... Better get moving.");
            }
            else
            {
                Stranger.Destroy();
                Game.QuickSetup();
            }
            #endregion

            do
            {
                Entry();
            } while (Game.isPlaying);

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
                    DisplayMenu.Show(DisplayMenu.MainMenuItems);
                    break;
            }

            // Steps a "turn" since this is turn based.. ish
            //Game.TakeTurn();
        }

        static void ShowHelp()
        {
            Console.WriteLine(" Usage:");
            Console.WriteLine("  fwod [options]");
            Console.WriteLine();
            Console.WriteLine("  -B,  --bosssays     Custom text from the Boss.");
            Console.WriteLine("  -Pc, --playerchar   Sets the player's character.");
            Console.WriteLine("  -Pn, --playername   Sets the player's name.");
            Console.WriteLine("  -S,  --skipintro    Skip intro and use defaults.");
#if DEBUG
            Console.WriteLine("  --runtests          Run debugging tests.");
            Console.WriteLine("  -Pn, --playername   Sets the player's name.");
            Console.WriteLine("  -S,  --skipintro    Skip intro and use defaults.");
#endif
            Console.WriteLine();
            Console.WriteLine("  --help, /?      Shows this screen");
            Console.WriteLine("  --version       Shows version");
            Console.WriteLine();
            Console.WriteLine("Have fun!");
        }

        static void ShowVersion()
        {
            Console.WriteLine(ProjectName + " - " + ProjectVersion);
            Console.WriteLine("Copyright (c) 2015 DD~!/guitarxhero");
            Console.WriteLine("License: MIT License <http://opensource.org/licenses/MIT>");
            Console.WriteLine("Project page: <https://github.com/guitarxhero/fwod>");
            Console.WriteLine();
            Console.WriteLine(" -- Credits --");
            Console.WriteLine("DD~! (guitarxhero) - Original author");
        }
    }
}