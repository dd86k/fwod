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
                Console.Write(" ");
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
                Console.SetCursorPosition(1, ConsoleTools.BufferHeight - 1);
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
                Console.Write(ConsoleTools.RepeatChar(' ', (ConsoleTools.BufferWidth / 2) - 1));
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
        /// Creates a new player. Only one can be made!
        /// </summary>
        internal Player()
        { // Defaults
            this.CharacterChar = '@';
            this._posx = ConsoleTools.BufferWidth / 2;
            this._posy = ConsoleTools.BufferHeight / 2;
            this._hp = 10;
        }


        internal Player(int X, int Y)
        { // Defaults
            this.CharacterChar = '@';
            this._posx = X;
            this._posy = Y;
            this._hp = 10;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Places the Player.
        /// </summary>
        internal void Initialize()
        {
            this.PosX = this._posx;
            this.PosY = this._posy;
        }

        /// <summary>
        /// Generates the bubble and readys the cursor
        /// </summary>
        /// <param name="pText">Text</param>
        /// <param name="pPosX">Left position</param>
        /// <param name="pPosY">Top position</param>
        void GenerateBubble(int pTextLength, int pLines, int pPosX, int pPosY)
        {
            Game.GenerateBox(Game.TypeOfLine.Single, pPosX, pPosY, pTextLength + 2, pLines + 2);

            // bubble chat "connector"
            if (pPosY < this.PosY) // Over player
            {
                Console.SetCursorPosition(this.PosX, this.PosY - 2);
                Console.Write(Game.Graphics.Lines.SingleConnector[2]);
            }
            else // Under player
            {
                Console.SetCursorPosition(this.PosX, this.PosY + 2);
                Console.Write(Game.Graphics.Lines.SingleConnector[1]);
            }

            // Prepare to insert text
            Console.SetCursorPosition(pPosX + 1, pPosY + 1);
        }

        internal void PlayerSays(string pText)
        {
            string[] Lines = new string[] { pText };
            int ci = 0;
            int start = 0;
            // Seperates the text in blocks of 25 letters in case
            if (pText.Length != 0)
            {
                if (pText.Length > 25)
                {
                    Lines = new string[(pText.Length / 26) + 1];
                    do
                    {
                        if (start + 25 > pText.Length)
                            Lines[ci] = pText.Substring(start, pText.Length - start);
                        else
                            Lines[ci] = pText.Substring(start, 25);
                        ci++;
                        start += 25;
                    } while (start < pText.Length);
                }
            }
            else Lines = new string[] { " " }; // At least the bubble won't look squished out

            // X/Left bubble starting position
            int StartX = this.PosX - (Lines[0].Length / 2) - 1;
            // Re-places StartX if it goes further than the display buffer
            if (StartX + (Lines[0].Length + 2) > ConsoleTools.BufferWidth)
            {
                StartX = ConsoleTools.BufferWidth - (Lines[0].Length + 2);
            }

            if (StartX < 0)
            {
                StartX = 0;
            }

            // Y/Top bubble starting position
            int StartY = this.PosY - (Lines.Length) - 3;
            // Re-places StartY if it goes further than the display buffer
            if (StartY > ConsoleTools.BufferWidth)
            {
                StartY = ConsoleTools.BufferWidth - (Lines[0].Length - 2);
            }

            if (StartY < 0)
            {
                StartY = 3;
            }

            int TextStartX = StartX + 1;
            int TextStartY = StartY + 1;

            GenerateBubble(Lines[0].Length,
                Lines.Length,
                StartX,
                StartY);

            // Insert Text
            for (int i = 0; i < Lines.Length; i++)
            {
                Console.SetCursorPosition(TextStartX, TextStartY + i);
                Console.Write(Lines[i]);
            }

            // Waiting for keypress
            Console.SetCursorPosition(0, 0);
            Console.ReadKey(true);

            // Clear bubble
            //TODO: Put older chars back
            Console.SetCursorPosition(StartX, StartY);
            int len = Lines[0].Length + 4;
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
            int StartX = this.PosX - (tmp.Length / 2) - 1;
            int StartY = this.PosY - 4;

            GenerateBubble(tmp.Length, 1, StartX, StartY);

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