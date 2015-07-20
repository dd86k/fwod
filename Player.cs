using System;

/*
    This class is about the Player.
    Reason is an enemy uses this class is simply
    due to lazyness. I could of just used an interface.
*/

namespace fwod
{
    internal class Player
    {
        #region Properties
        int _posx;
        /// <summary>
        /// Sets or gets the Player position (Left).
        /// </summary>
        internal int PosX
        {
            get { return _posx; }
            set
            {
                char futrG = Core.GetCharAt(Core.Layer.Game, value, this.PosY);
                char futrP = Core.GetCharAt(Core.Layer.Player, value, this.PosY);
                if (!futrG.IsSolidObject() && !futrP.IsEnemyObject())
                {
                    // Note: if future coord is enemy, attack enemy instead

                    // Get old char
                    char pastchar = Core.GetCharAt(Core.Layer.Game, this.PosX, this.PosY);
                    
                    // Place old char
                    Console.SetCursorPosition(this.PosX, this.PosY);
                    Console.Write(pastchar);

                    // Update event
                    Game.UpdateEvent("Player moved " + (this._posx > value ? "left" : "right"));
                    
                    // Update value
                    this._posx = value;
                    
                    // Move player
                    Core.Write(Core.Layer.Player, this.CharacterChar, value, this.PosY);
                }
            }
        }

        int _posy;
        /// <summary>
        /// Sets or gets the Player position (Top).
        /// </summary>
        internal int PosY
        {
            get { return _posy; }
            set
            {
                char futrG = Core.GetCharAt(Core.Layer.Game, this.PosX, value);
                char futrP = Core.GetCharAt(Core.Layer.Player, this.PosX, value);
                if (!futrG.IsSolidObject() && !futrP.IsEnemyObject())
                {
                    // Note: if future coord is enemy, attack enemy instead

                    // Get old char
                    char pastchar = Core.GetCharAt(Core.Layer.Game, this.PosX, this.PosY);
                    
                    // Place old char
                    Console.SetCursorPosition(this.PosX, this.PosY);
                    Console.Write(pastchar);

                    // Update event
                    Game.UpdateEvent("Player moved " + (this._posy > value ? "up" : "down"));
                    
                    // Update value
                    this._posy = value;
                    
                    // Move player
                    Core.Write(Core.Layer.Player, this.CharacterChar, this.PosX, value);
                }
            }
        }

        int _hp;
        /// <summary>
        /// Gets or sets the HP.
        /// </summary>
        int HP
        {
            get { return _hp; }
            set
            {
                _hp = value;
                Console.SetCursorPosition(ConsoleTools.BufferWidth / 2, 0);
                Console.Write("HP: " + _hp);
            }
        }

        /// <summary>
        /// Gets or sets the name of the character.
        /// </summary>
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

        /// <summary>
        /// The Player's name.
        /// </summary>
        string _characterName;
        #endregion

        #region Construction
        /// <summary>
        /// Creates a new player.
        /// </summary>
        internal Player()
        { // Defaults
            this.CharacterChar = '@';
            this._posx = ConsoleTools.BufferWidth / 2;
            this._posy = ConsoleTools.BufferHeight / 2;
            this._hp = 10;
        }

        /// <summary>
        /// Creates a new player with specific coords.
        /// </summary>
        /// <param name="X">Left value.</param>
        /// <param name="Y">Top value.</param>
        internal Player(int X, int Y)
        {
            this.CharacterChar = '@';
            this._posx = X;
            this._posy = Y;
            this._hp = 10;
        }
        #endregion

        #region Init
        /// <summary>
        /// Places the Player on screen.
        /// </summary>
        internal void Initialize()
        {
            this.PosX = this._posx;
            this.PosY = this._posy;
        }
        #endregion

        #region Conversation
        /// <summary>
        /// Makes the character talk.
        /// </summary>
        /// <param name="pText">Text!</param>
        internal void Say(string pText)
        {
            Say(pText, true);
        }

        /// <summary>
        /// Makes the character talk.
        /// </summary>
        /// <param name="pText">Text!</param>
        /// <param name="pWait">Wait for keydown?</param>
        internal void Say(string pText, bool pWait)
        {
            string[] Lines = new string[] { pText }; // In case of multiline scenario
            int ci = 0; // Multiline scenario row index
            int start = 0; // Multiline cutting index

            // This block seperates the input into 25 characters each lines equaly.
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
            // Minimum text so the bubble doesn't look too thin
            else Lines = new string[] { " " };

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

            // Define the position of the text
            int TextStartX = StartX + 1;
            int TextStartY = StartY + 1;

            // Generate the bubble
            GenerateBubble(Lines[0].Length, Lines.Length, StartX, StartY);

            // Insert Text
            for (int i = 0; i < Lines.Length; i++)
                Core.Write(Core.Layer.Bubble, Lines[i], TextStartX, TextStartY + i); 

            // Waiting for keypress
            if (pWait) Console.ReadKey(true);

            // Clear bubble
            int lenH = StartX + Lines[0].Length + 2;
            int lenV = StartY + Lines.Length + 2;
            for (int row = StartY; row < lenV; row++)
            {
                for (int col = StartX; col < lenH; col++)
                {
                    // Write back what was at Game layer before
                    Core.Write(Core.Layer.Game, Core.GetCharAt(Core.Layer.Game, col, row), col, row);
                }
            }
        }

        /// <summary>
        /// Get input from Player.
        /// </summary>
        /// <returns>Answer</returns>
        internal string GetAnswer()
        {
            // Generates temporary text for spacer
            string tmp = ConsoleTools.RepeatChar(' ', 25);

            // Determine the starting position of the bubble
            int StartX = this.PosX - (tmp.Length / 2) - 1;
            int StartY = this.PosY - 4;

            // Generate the bubble
            GenerateBubble(tmp.Length, 1, StartX, StartY);

            // Read input from player
            string Out = ConsoleTools.ReadLine(25);

            // Clear bubble
            int lenH = StartX + tmp.Length + 2;
            int lenV = StartY + 3;
            for (int row = StartY; row < lenV; row++)
            {
                for (int col = StartX; col < lenH; col++)
                {
                    // Write back what was at Game layer before
                    Core.Write(Core.Layer.Game, Core.GetCharAt(Core.Layer.Game, col, row), col, row);
                }
            }

            return Out;
        }
        #endregion

        #region Movement
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

        #region Bubble
        /// <summary>
        /// Generates the bubble for a player.
        /// </summary>
        /// <param name="pPlayer">Player</param>
        /// <param name="pTextLength">Lenght of the text (Width)</param>
        /// <param name="pLines">Length of the text (Height)</param>
        /// <param name="pPosX">Top position</param>
        /// <param name="pPosY">Left position</param>
        void GenerateBubble(int pTextLength, int pLines, int pPosX, int pPosY)
        {
            Game.GenerateBox(Core.Layer.Bubble, Game.TypeOfLine.Single, pPosX, pPosY, pTextLength + 2, pLines + 2);

            // Bubble chat "connector"
            if (pPosY < this.PosY) // Over player
            {
                Console.SetCursorPosition(this.PosX, this.PosY - 2);
                Core.Write(Core.Layer.Bubble, Game.Graphics.Lines.SingleConnector[2]);
            }
            else // Under player
            {
                Console.SetCursorPosition(this.PosX, this.PosY + 2);
                Core.Write(Core.Layer.Bubble, Game.Graphics.Lines.SingleConnector[1]);
            }

            // Prepare to insert text
            Console.SetCursorPosition(pPosX + 1, pPosY + 1);
        }
        #endregion
    }
}