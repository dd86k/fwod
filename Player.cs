using System;

namespace Play
{
    internal class Player
    {
        int _posx;
        int PosX
        {
            get { return _posx; }
            set
            {
                Console.SetCursorPosition(this._posx, this.PosY);
                Console.Write(" "); //TODO: Put old char back/remember char
                _posx = value;
                Console.SetCursorPosition(this._posx, this.PosY);
                Console.Write(this.CharacterChar);
            }
        }

        int _posy;
        int PosY
        {
            get { return _posy; }
            set
            {
                Console.SetCursorPosition(this.PosX, _posy);
                Console.Write(" ");
                _posy = value;
                Console.SetCursorPosition(this.PosX, _posy);
                Console.Write(this.CharacterChar);
            }
        }

        string _characterName;

        int _hp;
        /// <summary>
        /// Gets or sets the HP.
        /// </summary>
        /// <value>The HP.</value>
        int HP
        {
            get { return _hp; }
            set
            {
                _hp = value;
                Console.SetCursorPosition(1, Console.BufferHeight - 1);
                Console.Write("HP: " + _hp);
            }
        }

        /// <summary>
        /// Gets or sets the name of the character.
        /// </summary>
        /// <value>The name of the character.</value>
        internal string CharacterName
        {
            get { return _characterName; }
            set
            {
                _characterName = value;
                Console.SetCursorPosition(1, 0);
                Console.Write(_characterName);
            }
        }

        /// <summary>
        /// The character's char.
        /// </summary>
        internal char CharacterChar;
        internal Player()
        { // Defaults
            this.CharacterChar = '@';
        }

        /// <summary>
        /// Places the Player.
        /// </summary>
        internal void Initialize()
        {
            this.PosX = (Console.BufferWidth / 4) + (Console.BufferWidth / 2);
            this.PosY = Console.BufferHeight / 2;
            this.HP = 10;

            Console.SetCursorPosition(this.PosX, this.PosY);
            Console.Write(this.CharacterChar);
            Game.GenerateMasterRoom();
        }

        /// <summary>
        /// Make the player talk!
        /// </summary>
        /// <param name="pText">Text.</param>
        internal void PlayerSays(string pText)
        {
            // -- Make bubble --
            // determine the starting position of the bubble
            int StartX = (pText.Length > 2 ?
                (this.PosX - (pText.Length / 2)) - 2:
                (this.PosX - (pText.Length / 2)) - 1);
            int StartY = this.PosY - 4;

            //TODO: Multiline chat bubble
            // If lenght is higher than 34, split it 
            //

            // top
            Console.SetCursorPosition(StartX, StartY);
            Console.Write(Game.Graphics.Walls.Thin[2]);
            ConsoleTools.GenerateHorizontalLine(Game.Graphics.Walls.Thin[1], pText.Length + 2);
            Console.Write(Game.Graphics.Walls.Thin[3]);

            // left
            Console.SetCursorPosition(StartX, StartY + 1);
            Console.Write(Game.Graphics.Walls.Thin[0]);

            // right
            Console.SetCursorPosition(StartX + pText.Length + 3, StartY + 1);
            Console.Write(Game.Graphics.Walls.Thin[0]);

            // bottom
            Console.SetCursorPosition(StartX, StartY + 2);
            Console.Write(Game.Graphics.Walls.Thin[5]);
            ConsoleTools.GenerateHorizontalLine(Game.Graphics.Walls.Thin[1], pText.Length + 2);
            Console.Write(Game.Graphics.Walls.Thin[4]);

            // bubble chat "connector"
            Console.SetCursorPosition(this.PosX, StartY + 2);
            Console.Write(Game.Graphics.Walls.Thin[8]);

            // -- Insert Text --
            Console.SetCursorPosition(StartX + 2, StartY + 1);
            Console.Write(pText);

            // Waiting for keypress
            Console.SetCursorPosition(0, 0);
            Console.ReadKey(true);

            // Clear bubble
            //TODO: Put older chars back
            Console.SetCursorPosition(StartX, StartY);
            int len = pText.Length + 4;
            for (int i = StartY; i < this.PosY; i++)
            {
                ConsoleTools.GenerateHorizontalLine(' ', len);
                Console.SetCursorPosition(StartX, i);
            }
            Console.SetCursorPosition(0, 0);
        }

        /// <summary>
        /// Makes the enemy move up one square
        /// </summary>
        internal void MoveUp()
        {
            this.PosY--;
        }

        /// <summary>
        /// Makes the enemy move down one square
        /// </summary>
        internal void MoveDown()
        {
            this.PosY++;
        }

        /// <summary>
        /// Makes the enemy move left one square
        /// </summary>
        internal void MoveLeft()
        {
            this.PosX--;
        }

        /// <summary>
        /// Makes the enemy move right one square
        /// </summary>
        internal void MoveRight()
        {
            this.PosX++;
        }
    }
}