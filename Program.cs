﻿using System;

/*
    This is the entry point of the program.
*/

#if DEBUG
[assembly: System.Reflection.AssemblyVersion("0.3.6.*")]
#else
[assembly: System.Reflection.AssemblyVersion("0.3.6.0")]
#endif

namespace fwod
{
    class MainClass
    {
        #region Constants
        const string ProjectName = "Four Walls Of Death";
        #endregion

        #region Properties
        static readonly string ProjectVersion =
                string.Format("{0}",
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version);
        static internal bool isPlaying = true;
        #endregion

        internal static int Main(string[] args)
        {
            // Default values
            string bosstext = string.Empty;
            char Pchar = '@';
            string Pname = "Player ";
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

                    case "-B":
                    case "--bosssays":
                        if (args[i + 1] != null)
                            bosstext = args[i + 1];
                        break;

                    case "-C":
                    case "--playerchar":
                        if (args[i + 1] != null)
                            Pchar = args[i + 1][0];
                        break;

                    case "-S":
                    case "--skipintro":
                        SkipIntro = true;
                        break;

#if DEBUG
                    case "--showmeme":
                        Misc.ShowMeme(); // :^)
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

            if (!SkipIntro)
            {
                Console.Clear();
                Console.Title = ProjectName + " " + ProjectVersion;

                string BannerText = "* Welcome to " + ProjectName + " *";
                string BannerOutline = ConsoleTools.RepeatChar('*', BannerText.Length);

                ConsoleTools.WriteLineAndCenter(BannerOutline);
                ConsoleTools.WriteLineAndCenter(BannerText);
                ConsoleTools.WriteLineAndCenter(BannerOutline);
                Console.WriteLine();
                Console.WriteLine("Watch your keystrokes!");

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
            Game.MainPlayer = new Person((ConsoleTools.BufferWidth / 4) + (ConsoleTools.BufferWidth / 2),
                ConsoleTools.BufferHeight / 2);
            Game.EnemyList.Add(new Person(ConsoleTools.BufferWidth / 4, ConsoleTools.BufferHeight / 2));

            // Generate the 'main' box
            Game.GenerateBox(Core.Layer.Game, Game.TypeOfLine.Double, 1, 1,
                ConsoleTools.BufferWidth - 2, ConsoleTools.BufferHeight - 3);

            Console.SetCursorPosition(27, 0);
            Console.Write("|");

            // Set player stuff
            Game.MainPlayer.CharacterName = Pname;
            Game.MainPlayer.CharacterChar = Pchar;
            Game.MainPlayer.PersonType = Person.Type.Player;
            Game.MainPlayer.Initialize();

            Game.EnemyList[0].CharacterChar = '#';
            Game.EnemyList[0].HP = 1;
            Game.EnemyList[0].PersonType = Person.Type.MysteriousStranger;
            Game.EnemyList[0].Initialize();

            #region Intro
            if (!SkipIntro)
            {
                Game.MainPlayer.Say("Ah! Where am I?");

                Game.EnemyList[0].Say("Oh, you're awake... What is your name?");

                if (Pname == "Player ")
                {
                    string tmp_name = null;

                    while (tmp_name == null)
                    {
                        tmp_name = Game.MainPlayer.GetAnswer();

                        Game.EnemyList[0].Say("Say something!");
                    }

                    Game.MainPlayer.CharacterName = tmp_name;
                }

                Game.MainPlayer.Say("It's " + Game.MainPlayer.CharacterName + ".");

                Game.EnemyList[0].Say("So, welcome to " + ProjectName + ".");
                Game.MainPlayer.HP = 10;
                Game.EnemyList[0].Say("Here's your HP meter.");

                Game.MainPlayer.Say("My HP?");

                Game.EnemyList[0].Say("Health Points, yo.");

                Game.MainPlayer.Say("Um.. Okay?");

                if (bosstext.Length > 0)
                    Game.EnemyList[0].Say(bosstext);
                else
                    Game.EnemyList[0].Say("I'll be back for you anyway, " + Game.MainPlayer.CharacterName + ".");

                Game.MainPlayer.Say("Wait!");

                //TODO: Make enemy walk to next floor and disapear (AI Path?)

                //Game.MainPlayer.Say("Oh, alright.. Time to get out.");
            }
            #endregion

            do
            {
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
            Console.WriteLine(" Usage:");
            Console.WriteLine("  fwod [options]");
            Console.WriteLine();
            Console.WriteLine("  -B --bosssays     Custom text from the Boss");
            Console.WriteLine("  -C --playerchar   Sets the player's character");
            Console.WriteLine("  -S --skipintro    Skip intro and use defaults");
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