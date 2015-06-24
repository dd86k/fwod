using System;

/*
    This class is about the Player.
*/

namespace FWoD
{
    internal class Player
    {
        #region Properties
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
                Console.SetCursorPosition(this._posx, this._posy);
                Console.Write(" ");
                _posy = value;
                Console.SetCursorPosition(this._posx, this._posy);
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
                // Clear name and redraw it (in case of shorter name)
                Console.SetCursorPosition(1, 0);
                Console.Write(ConsoleTools.RepeatChar(' ', Console.BufferWidth / 2));
                Console.SetCursorPosition(1, 0);
                Console.Write(_characterName);
            }
        }

        /// <summary>
        /// The character's char.
        /// </summary>
        internal char CharacterChar;
        #endregion

        #region Construction
        /// <summary>
        /// Creates a few player. Only one can be made!
        /// </summary>
        internal Player()
        { // Defaults
            this.CharacterChar = '@';
        }
        #endregion

        #region Methods
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
        }

        /// <summary>
        /// Generates the bubble and readys the cursor
        /// </summary>
        /// <param name="pText">P text.</param>
        /// <param name="pPosX">P position x.</param>
        /// <param name="pPosY">P position y.</param>
        void GenerateBubble(string pText, int pPosX, int pPosY)
        { // pText is to determine the size of the bubble!

            //TODO: NOW Multiline chat bubble DO IT DO IT DO IT DO IT DO IT 
            // If lenght is higher than 25, split it 

            //TODO: GenerateBubble -> Game class with (Player pPlayer, bool Yelling, [...])

            Game.GenerateBox(Game.TypeOfLine.Single, pPosX, pPosY, pText.Length + 4, 3);

            // bubble chat "connector" (Over player)
            Console.SetCursorPosition(this.PosX, pPosY + 2);
            Console.Write(Game.Graphics.Lines.SingleConnector[2]);

            Console.SetCursorPosition(pPosX + 2, pPosY + 1); // Prepare to insert text
        }

        internal void PlayerSays(string pText)
        {
            // determine the starting position of the bubble
            int StartX = (pText.Length > 2 ?
                (this.PosX - (pText.Length / 2)) - 2:
                (this.PosX - (pText.Length / 2)) - 1);
            int StartY = this.PosY - 4;

            GenerateBubble(pText, StartX, StartY);

            // -- Insert Text --
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
        }

        internal string PlayerAnswer()
        {
            string tmp = ConsoleTools.RepeatChar(' ', 25);

            // determine the starting position of the bubble
            // lol copy paste 
            int StartX = (tmp.Length > 2 ?
                (this.PosX - (tmp.Length / 2)) - 2:
                (this.PosX - (tmp.Length / 2)) - 1);
            int StartY = this.PosY - 4;

            GenerateBubble(tmp, StartX, StartY);

            string Out = Console.ReadLine();

            // Clear bubble
            //TODO: Put older chars back
            Console.SetCursorPosition(StartX, StartY);
            int len = tmp.Length + 4;
            for (int i = StartY; i < this.PosY; i++)
            {
                ConsoleTools.GenerateHorizontalLine(' ', len);
                Console.SetCursorPosition(StartX, i);
            }

            return Out;
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
        #endregion
    }
}