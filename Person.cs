using System;

/*
    A Person.
    Can be a Player, Enemy, etc.
*/

namespace fwod
{
    #region Person
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
                char futrP = Core.GetCharAt(Core.Layer.People, value, this.PosY);
                bool futrIsSolid = futrG.IsSolidObject();
                bool futrIsSomeone = futrP.IsPersonObject();
                if (!futrIsSolid)
                {
                    if (futrIsSomeone)
                    {
                        Person futrPerson = Game.GetPersonObjectAt(value, this.PosY);

                        if (futrPerson != null && futrPerson != this && futrPerson is Enemy)
                        {
                            this.Attack(futrPerson);
                        }
                    }
                    else
                    {
                        Move(this.PosX, this.PosY, value, this.PosY);
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
                char futrP = Core.GetCharAt(Core.Layer.People, this.PosX, value);
                bool futrIsSolid = futrG.IsSolidObject();
                bool futrIsEnemy = futrP.IsPersonObject();
                if (!futrIsSolid)
                {
                    if (futrIsEnemy)
                    {
                        Person Enemy = Game.GetPersonObjectAt(this.PosX, value);

                        if (Enemy != null && Enemy != this)
                        {
                            this.Attack(Enemy);
                        }
                    }
                    else
                    {
                        Move(this.PosX, this.PosY, this.PosX, value);
                    }
                }
            }
        }

        void Move(int pastX, int pastY, int newX, int newY)
        {
            // Get old char
            char pastchar = Core.GetCharAt(Core.Layer.Game, pastX, pastY);

            // Place old char
            Console.SetCursorPosition(pastX, pastY);
            Console.Write(pastchar == '\0' ? ' ' : pastchar);

            // Update values
            _posy = newY;
            _posx = newX;

            // Move player
            Core.Write(Core.Layer.People, this.CharacterChar, newX, newY);
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

                if (this is Player)
                {
                    Console.SetCursorPosition(29, 0);
                    Console.Write(new string(' ', 7));
                    Console.SetCursorPosition(29, 0);
                    Console.Write(string.Format("HP: {0:000}", this.HP));
                }

                if (_hp <= 0)
                    Destroy();
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

        #region Stats
        int _s1;
        /// <summary>
        /// Strength
        /// </summary>
        internal int S1
        {
            get { return _s1; }
            set
            {
                if (value <= 10 && value >= 0)
                    _s1 = value;
            }
        }

        int _s2;
        /// <summary>
        /// 
        /// </summary>
        internal int S2
        {
            get { return _s2; }
            set
            {
                if (value <= 10 && value >= 0)
                    _s2 = value;
            }
        }

        int _s3;
        /// <summary>
        /// 
        /// </summary>
        internal int S3
        {
            get { return _s3; }
            set
            {
                if (value <= 10 && value >= 0)
                    _s3 = value;
            }
        }

        int _s4;
        /// <summary>
        /// 
        /// </summary>
        internal int S4
        {
            get { return _s4; }
            set
            {
                if (value <= 10 && value >= 0)
                    _s4 = value;
            }
            
        }

        int _s5;
        /// <summary>
        /// 
        /// </summary>
        internal int S5
        {
            get { return _s5; }
            set
            {
                if (value <= 10 && value >= 0)
                    _s5 = value;
            }
        }

        int _s6;
        /// <summary>
        /// 
        /// </summary>
        internal int S6
        {
            get { return _s6; }
            set
            {
                if (value <= 10 && value >= 0)
                    _s6 = value;
            }
        }

        int _s7;
        /// <summary>
        /// 
        /// </summary>
        internal int S7
        {
            get { return _s7; }
            set
            {
                if (value <= 10 && value >= 0)
                    _s7 = value;
            }
        }
        #endregion
        #endregion

        #region Construction
        internal Person()
            : this(ConsoleTools.BufferWidth / 2, ConsoleTools.BufferHeight / 2)
        {

        }

        internal Person(int X, int Y)
        {
            CharacterChar = 'P';
            _hp = 10;
            _posx = X;
            _posy = Y;
            _s1 = 5;
            _s2 = 5;
            _s3 = 5;
            _s4 = 5;
            _s5 = 5;
            _s6 = 5;
            _s7 = 5;
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
                            Lines[ci] = pText.Substring(start, pText.Length - start);
                        else
                            Lines[ci] = pText.Substring(start, 25);
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

        #region Attack
        internal void Attack(Person pPerson)
        {
            //TODO: Attack algorithm
            int AttackPoints = this.S1 /* * this.Weapon.BaseDamage*/;

            pPerson.HP -= AttackPoints;

            if (pPerson is Enemy)
                Game.DisplayEvent(((Enemy)pPerson).eType + " -= " + AttackPoints + " HP! (HP:" + pPerson.HP + ")");
            else
                Game.DisplayEvent(pPerson.GetType() + " -= " + AttackPoints + " HP! (HP:" + pPerson.HP + ")");
        }
        #endregion

        #region Destroy
        /// <summary>
        /// Remove completely the Person from game.
        /// </summary>
        internal void Destroy()
        {
            Console.SetCursorPosition(this.PosX, this.PosY);
            Console.Write(' ');
            if (this is Enemy)
            {
                Enemy e = (Enemy)this;
                Game.EnemyList.Remove(e);
                Game.DisplayEvent(Game.MainPlayer.CharacterName + " killed " + e.eType + "!");
            }
            else if (this is Player)
            { // Game over
                //TODO: Game over

            }
            else
            {
                Game.PeopleList.Remove(this);
                Game.DisplayEvent(Game.MainPlayer.CharacterName + " killed " + this.GetType() + "!");
            }
        }
        #endregion
    }
    #endregion

    #region Player
    class Player : Person
    {
        internal Player()
            : base(ConsoleTools.BufferWidth / 2, ConsoleTools.BufferHeight / 2)
        {

        }

        internal Player(int X, int Y)
            : base(X, Y)
        {
            this.CharacterChar = '@';
        }
    }
    #endregion

    #region Enemy
    class Enemy : Person
    {
        internal Enemy(int X, int Y, EnemyType pEnemyType, int pHp)
            : base(X, Y)
        {
            this.HP = pHp;
            this.CharacterChar = 'E';
        }

        internal enum EnemyType
        {
            Rat
        }

        EnemyType _type;
        internal EnemyType eType
        {
            get { return _type; }
            set { _type = value; }
        }
    }
    #endregion
}