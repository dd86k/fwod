using System;

/*
    A Person, whenever it be a player, enemy, etc.
*/

namespace fwod
{
    internal class Person
    {
        #region Consts
        const int BUBBLE_PADDING_X = 0;
        const int BUBBLE_PADDING_Y = 0; // Not ready, but also no thanks
        #endregion

        #region Object properties
        #region Position
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
                char futrP = Core.GetCharAt(Core.Layer.Person, value, this.PosY);
                bool futrIsSolid = futrG.IsSolidObject();
                bool futrIsEnemy = futrP.IsEnemyObject();
                if (!futrIsSolid)
                {
                    if (futrIsEnemy)
                    {
                        Person Enemy = Game.GetEnemyObjectAt(value, this.PosY);

                        if (Enemy != null && Enemy != this)
                        {
                            Enemy.HP--;
                        }
                    }
                    else
                    {
                        // Get old char
                        char pastchar = Core.GetCharAt(Core.Layer.Game, this.PosX, this.PosY);

                        // Place old char
                        Console.SetCursorPosition(this.PosX, this.PosY);
                        Console.Write(pastchar == '\0' ? ' ' : pastchar);

                        // Update event
                        if (this._posx != value)
                            Game.DisplayEvent("Player moved " + (this._posx > value ? "left" : "right"));

                        // Update value
                        this._posx = value;

                        // Move player
                        Core.Write(Core.Layer.Person, this.CharacterChar, value, this.PosY);
                    }
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
                char futrP = Core.GetCharAt(Core.Layer.Person, this.PosX, value);
                bool futrIsSolid = futrG.IsSolidObject();
                bool futrIsEnemy = futrP.IsEnemyObject();
                if (!futrIsSolid)
                {
                    if (futrIsEnemy)
                    {
                        Person Enemy = Game.GetEnemyObjectAt(this.PosX, value);

                        if (Enemy != null && Enemy != this)
                        {
                            Enemy.HP--;
                        }
                    }
                    else
                    {
                        // Get old char
                        char pastchar = Core.GetCharAt(Core.Layer.Game, this.PosX, this.PosY);

                        // Place old char
                        Console.SetCursorPosition(this.PosX, this.PosY);
                        Console.Write(pastchar == '\0' ? ' ' : pastchar);

                        // Update event
                        if (this._posy != value)
                            Game.DisplayEvent("Player moved " + (this._posy > value ? "up" : "down"));

                        // Update value
                        this._posy = value;

                        // Move player
                        Core.Write(Core.Layer.Person, this.CharacterChar, this.PosX, value);
                    }
                }
            }
        }
        #endregion

        #region Health
        int _hp;
        /// <summary>
        /// Gets or sets the HP.
        /// </summary>
        internal int HP
        {
            get { return _hp; }
            set
            {
                _hp = value;

                if (Game.MainPlayer == this)
                {
                    Console.SetCursorPosition(29, 0);
                    Console.Write(new string(' ', 7));
                    Console.SetCursorPosition(29, 0);
                    Console.Write(string.Format("HP: {0:000}", this.HP));
                }

                if (value <= 0)
                    this.Destroy();
            }
        }
        #endregion

        #region Name, appearance
        string _characterName;
        /// <summary>
        /// Gets or sets the name of the character.
        /// Not to use with an enemy!
        /// </summary>
        internal string CharacterName
        {
            get { return _characterName; }
            set
            {
                _characterName = value;
                // Clear name and redraw it (in case of shorter name)
                Console.SetCursorPosition(1, 0);
                Console.Write(ConsoleTools.RepeatChar(' ', 25));
                Console.SetCursorPosition(1, 0);
                Console.Write(_characterName);
            }
        }

        /// <summary>
        /// The Player's literal character.
        /// </summary>
        internal char CharacterChar;
        #endregion

        #region Type
        /// <summary>
        /// Types of Person
        /// </summary>
        internal enum Type
        {
            // Player
            Player,
            // Neutral
            MysteriousStranger,
            // Enemies
            Rat,
            // NPCs
            Seller,
            // Misc
            Dummy
        }

        Type _type;
        /// <summary>
        /// Gets or sets the type of the Person
        /// </summary>
        internal Type PersonType
        {
            get { return this._type; }
            set { this._type = value; }
        }
        #endregion
        #endregion

        #region Construction
        /// <summary>
        /// Creates a new Person.
        /// </summary>
        internal Person()
            : this(ConsoleTools.BufferWidth / 2, ConsoleTools.BufferHeight / 2, Type.Dummy)
        {
            this.CharacterChar = '@';
            this._hp = 10;
        }

        /// <summary>
        /// Creates a new Person with specific coords.
        /// </summary>
        /// <param name="X">Left value.</param>
        /// <param name="Y">Top value.</param>
        internal Person(int X, int Y)
            : this(X, Y, Type.Dummy)
        {
            this.CharacterChar = '@';
            this._hp = 10;
        }

        /// <summary>
        /// Creates a new Person with specific coords and type.
        /// </summary>
        /// <param name="X">Left value.</param>
        /// <param name="Y">Top value.</param>
        /// <param name="pPersonType">Type of the person.</param>
        internal Person(int X, int Y, Type pPersonType)
        {
            this.CharacterChar = '@';
            this._hp = 10;
            this._posx = X;
            this._posy = Y;
            this._type = pPersonType;
        }
        #endregion

        #region Init
        /// <summary>
        /// Places the Person on screen.
        /// </summary>
        internal void Initialize()
        {
            this.PosX = this._posx;
            this.PosY = this._posy;
        }
        #endregion

        #region Bubble
        // Avoiding heavy usage of parameters
        int _startX;
        int _startY;
        int _lenW;
        int _lenH;

        void GenerateBubble()
        {
            Game.GenerateBox(Core.Layer.None,
                Game.TypeOfLine.Single,
                this._startX - BUBBLE_PADDING_X,
                this._startY - BUBBLE_PADDING_Y,
                this._lenW + (BUBBLE_PADDING_X * 2),
                this._lenH + (BUBBLE_PADDING_Y * 2));

            // Bubble chat "connector"
            if (_startY < this.PosY) // Over Person
            {
                Console.SetCursorPosition(this.PosX, this.PosY - 2);
                Console.Write(Game.Graphics.Lines.SingleConnector[2]);
            }
            else // Under Person
            {
                Console.SetCursorPosition(this.PosX, this.PosY + 2);
                Console.Write(Game.Graphics.Lines.SingleConnector[1]);
            }

            // Prepare to insert text
            Console.SetCursorPosition(this._startX + 1, this._startY + 1);
        }

        void ClearBubble()
        {
            char c;
            for (int row = this._startY; row < this._lenH + this._startY; row++)
            {
                for (int col = this._startX; col < this._lenW + this._startX; col++)
                {
                    Console.SetCursorPosition(col, row);
                    c = Core.GetCharAt(Core.Layer.Game, col, row);
                    Console.Write(c == '\0' ? ' ' : c);
                }
            }
        }
        #endregion

        #region Conversation
        /// <summary>
        /// Makes the character talk.
        /// </summary>
        /// <param name="pText">Dialog</param>
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

            if (pText.Length > 0)
            {
                if (pText.Length > 25)
                {
                    int ci = 0; // Multiline scenario row index
                    int start = 0; // Multiline cutting index
                    Lines = new string[(pText.Length / 26) + 1];

                    // This block seperates the input into 25 characters each lines equally.
                    do
                    {
                        if (start + 25 > pText.Length)
                            Lines[ci] = pText.Substring(start, pText.Length - start).Trim();
                        else
                            Lines[ci] = pText.Substring(start, 25).Trim();
                        ci++;
                        start += 25;
                    } while (start < pText.Length);
                }
            }
            else Lines = new string[] { "..." };

            // X/Left bubble starting position
            this._startX = this.PosX - (Lines[0].Length / 2) - 1;
            // Re-places StartX if it goes further than the display buffer
            if (_startX + (Lines[0].Length + 2) > ConsoleTools.BufferWidth)
            {
                _startX = ConsoleTools.BufferWidth - (Lines[0].Length + 2);
            }
            else if (_startX < 0)
            {
                _startX = 0;
            }

            // Y/Top bubble starting position
            this._startY = this.PosY - (Lines.Length) - 3;
            // Re-places StartY if it goes further than the display buffer
            if (_startY > ConsoleTools.BufferWidth)
            {
                _startY = ConsoleTools.BufferWidth - (Lines[0].Length - 2);
            }
            else if (_startY < 0)
            {
                _startY = 3;
            }

            this._lenW = Lines[0].Length + BUBBLE_PADDING_X + 2;
            this._lenH = Lines.Length + BUBBLE_PADDING_Y + 2;

            // Generate the bubble
            GenerateBubble();

            // Insert Text
            for (int i = 0; i < Lines.Length; i++)
            {
                Console.SetCursorPosition(this._startX + 1, this._startY + i + 1);
                Console.Write(Lines[i]);
            }

            if (pWait)
            {
                Console.ReadKey(true);
                ClearBubble();
            }
            else
            {
                // Prepare for text
                Console.SetCursorPosition(this._startX + 1, this._startY + 1);
            }
        }

        /// <summary>
        /// Get input from the Person.
        /// </summary>
        /// <returns>Answer</returns>
        internal string GetAnswer()
        {
            return GetAnswer(25);
        }

        /// <summary>
        /// Get input from the Person.
        /// </summary>
        /// <param name="pLimit">Limit in characters.</param>
        /// <returns>Answer</returns>
        internal string GetAnswer(int pLimit)
        {
            Say(new string(' ', pLimit), false);

            // Read input from this Person
            string Out = ConsoleTools.ReadLine(pLimit);

            // Clear bubble
            ClearBubble();

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

        #region Destroy
        /// <summary>
        /// Remove completely the Person from game.
        /// </summary>
        internal void Destroy()
        {
            if (this == Game.MainPlayer)
            {
                //TODO: GAME OVER MANG

            }
            else
            {
                Console.SetCursorPosition(this.PosX, this.PosY);
                Console.Write(' ');
                Game.DisplayEvent(Game.MainPlayer.CharacterName + " killed " + this.PersonType + "!");
                Game.EnemyList.Remove(this);
            }
        }
        #endregion
    }
}