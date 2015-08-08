using System;
using System.Collections.Generic;

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
                if (value != _posx)
                {
                    char futrG = Core.GetCharAt(Core.Layer.Game, value, PosY);
                    char futrP = Core.GetCharAt(Core.Layer.People, value, PosY);
                    bool futrIsSolid = futrG.IsSolidObject();
                    bool futrIsSomeone = futrP.IsPersonObject();
                    if (!futrIsSolid)
                    {
                        if (futrIsSomeone)
                        {
                            Person futrPerson = Game.GetPersonObjectAt(value, PosY);

                            if (futrPerson != this && futrPerson is Enemy)
                            {
                                Attack(futrPerson);
                            }
                        }
                        else
                        {
                            Move(PosX, PosY, value, PosY);
                        }
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
                if (value != _posy)
                {
                    char futrG = Core.GetCharAt(Core.Layer.Game, PosX, value);
                    char futrP = Core.GetCharAt(Core.Layer.People, PosX, value);
                    bool futrIsSolid = futrG.IsSolidObject();
                    bool futrIsEnemy = futrP.IsPersonObject();
                    if (!futrIsSolid)
                    {
                        if (futrIsEnemy)
                        {
                            Person futrPerson = Game.GetPersonObjectAt(PosX, value);

                            if (futrPerson != this && futrPerson is Enemy)
                            {
                                Attack(futrPerson);
                            }
                        }
                        else
                        {
                            Move(PosX, PosY, PosX, value);
                        }
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
            Core.Write(Core.Layer.People, CharacterChar, newX, newY);
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
                if (value > _maxhp)
                    _hp = _maxhp;
                else
                    _hp = value;

                if (this is Player)
                {
                    Console.SetCursorPosition(29, 0);
                    Console.Write(new string(' ', 11));
                    Console.SetCursorPosition(29, 0); //HP: 000/000 | 
                    Console.Write(string.Format("HP: {0:000}/{1:000}", new object[] { _hp, _maxhp }));
                }

                if (_hp <= 0)
                    Destroy();
            }
        }

        int _maxhp;
        internal int MaxHP
        {
            get { return _maxhp; }
            set
            {
                if (value < _hp)
                    _maxhp = _hp;
                else
                    _maxhp = value;
            }
        }
        #endregion

        #region Name, appearance
        string _characterName;
        /// <summary>
        /// Gets or sets the name of the character.
        /// </summary>
        internal string CharacterName
        {
            get { return _characterName; }
            set
            {
                _characterName = value;

                if (this is Player)
                {
                    // Clear name and redraw it (in case of shorter name)
                    Console.SetCursorPosition(1, 0);
                    Console.Write(ConsoleTools.RepeatChar(' ', 25));
                    Console.SetCursorPosition(1, 0);
                    Console.Write(_characterName);
                }
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
            get { return _s1 ; }
            set
            {
                if (value > 10)
                    _s1 = 10;
                else if (value >= 0)
                    _s1 = value;
            }
        }

        int _s2;
        /// <summary>
        /// Dexterity
        /// </summary>
        internal int S2
        {
            get { return _s2; }
            set
            {
                if (value > 10)
                    _s2 = 10;
                else if (value >= 0)
                    _s2 = value;
            }
        }

        int _s3;
        /// <summary>
        /// Agility
        /// </summary>
        internal int S3
        {
            get { return _s3; }
            set
            {
                if (value > 10)
                    _s3 = 10;
                else if (value >= 0)
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
                if (value > 10)
                    _s4 = 10;
                else if (value >= 0)
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
                if (value > 10)
                    _s5 = 10;
                else if (value >= 0)
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
                if (value > 10)
                    _s6 = 10;
                else if (value >= 0)
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
                if (value > 10)
                    _s7 = 10;
                else if (value >= 0)
                    _s7 = value;
            }
        }
        #endregion

        #region Weapon
        /// <summary>
        /// Current EquipedWeapon in this Person.
        /// </summary>
        internal Weapon EquipedWeapon
        {
            get; set;
        }
        #endregion

        #region Armor
        /// <summary>
        /// Current EquipedArmor in this Person.
        /// </summary>
        internal Armor EquipedArmor
        {
            get; set;
        }
        #endregion

        #region Money
        int _money;
        /// <summary>
        /// Current sum of money in this Person.
        /// The sum is display via the inventory.
        /// </summary>
        internal int Money
        {
            get { return _money; }
            set
            {
                if (value < 1000000)
                    _money = value;

                if (this is Player)
                {
                    Console.SetCursorPosition(43, 0);
                    Console.Write(new string(' ', 8));
                    Console.SetCursorPosition(43, 0); //0'000'000$
                    Console.Write(string.Format("{0:0000000}$", _money));
                }
            }
        }
        #endregion

        #region Inventory
        List<Item> Inventory = new List<Item>();
        #endregion
        #endregion

        #region Construction
        internal Person()
            : this(ConsoleTools.BufferWidth / 2, ConsoleTools.BufferHeight / 2)
        {

        }

        internal Person(int pX, int pY)
            : this(pX, pY, 10)
        {

        }

        internal Person(int pX, int pY, int pHP)
        {
            _hp = pHP;
            _maxhp = _hp;
            _money = (int)(pHP * 0.5);
            _posx = pX;
            _posy = pY;
            _s1 = 1;
            _s2 = 1;
            _s3 = 1;
            _s4 = 1;
            _s5 = 1;
            _s6 = 1;
            _s7 = 1;
            CharacterChar = 'P';
            EquipedWeapon = new Weapon("None", 0);
            EquipedArmor = new Armor("None", 0);
        }
        #endregion

        #region Init
        /// <summary>
        /// Places the Person on screen.
        /// </summary>
        internal void Initialize()
        {
            Core.Write(Core.Layer.People, CharacterChar, _posx, _posy);
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
                _startX - BUBBLE_PADDING_X,
                _startY - BUBBLE_PADDING_Y,
                _lenW + (BUBBLE_PADDING_X * 2),
                _lenH + (BUBBLE_PADDING_Y * 2));

            // Bubble chat "connector"
            if (_startY < PosY) // Over Person
            {
                Console.SetCursorPosition(PosX, PosY - 2);
                Console.Write(Game.Graphics.Lines.SingleConnector[2]);
            }
            else // Under Person
            {
                Console.SetCursorPosition(PosX, PosY + 2);
                Console.Write(Game.Graphics.Lines.SingleConnector[1]);
            }

            // Prepare to insert text
            Console.SetCursorPosition(_startX + 1, _startY + 1);
        }

        void ClearBubble()
        {
            char c;
            for (int row = _startY; row < _lenH + _startY; row++)
            {
                for (int col = _startX; col < _lenW + _startX; col++)
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
        /// <param name="pText">Dialog</param>
        /// <param name="pWait">Wait for keydown</param>
        internal void Say(string pText, bool pWait)
        {
            string[] Lines = new string[] { pText }; // In case of multiline scenario

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
            
            Say(Lines, pWait);
        }

        internal void Say(string[] pLines, bool pWait)
        {
            // X/Left bubble starting position
            _startX = PosX - (pLines[0].Length / 2) - 1;
            // Re-places StartX if it goes further than the display buffer
            if (_startX + (pLines[0].Length + 2) > ConsoleTools.BufferWidth)
                _startX = ConsoleTools.BufferWidth - (pLines[0].Length + 2);
            else if (_startX < 0)
                _startX = 0;

            // Y/Top bubble starting position
            _startY = PosY - (pLines.Length) - 3;
            // Re-places StartY if it goes further than the display buffer
            if (_startY > ConsoleTools.BufferWidth)
                _startY = ConsoleTools.BufferWidth - (pLines[0].Length - 2);
            else if (_startY < 0)
                _startY = 3;

            _lenW = pLines[0].Length + BUBBLE_PADDING_X + 2;
            _lenH = pLines.Length + BUBBLE_PADDING_Y + 2;

            // Generate the bubble
            GenerateBubble();

            int TextStartX = _startX + 1;
            int TextStartY = _startY + 1;

            // Insert Text
            for (int i = 0; i < pLines.Length; i++)
            {
                Console.SetCursorPosition(TextStartX, TextStartY + i);
                Console.Write(pLines[i]);
            }

            if (pWait)
            {
                Console.ReadKey(true);
                ClearBubble();
            }
            else
            {
                // Prepare for text
                Console.SetCursorPosition(_startX + 1, _startY + 1);
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
            PosY--;
        }

        /// <summary>
        /// Makes the enemy move down one square
        /// </summary>
        internal void MoveDown()
        {
            PosY++;
        }

        /// <summary>
        /// Makes the enemy move left one square
        /// </summary>
        internal void MoveLeft()
        {
            PosX--;
        }

        /// <summary>
        /// Makes the enemy move right one square
        /// </summary>
        internal void MoveRight()
        {
            PosX++;
        }
        #endregion

        #region Attack
        internal void Attack(Person pPerson)
        {
            //TODO: Attack algorithm
            int AttackPoints = (int)((S1 + EquipedWeapon.BaseDamage) - EquipedArmor.ArmorPoints);

            pPerson.HP -= AttackPoints;

            string atk = " -> " + AttackPoints + " = " + pPerson.HP + "!" +
                (pPerson.HP <= 0 ? " +" + pPerson.Money + "$ *DEAD*" : string.Empty);

            if (pPerson is Enemy)
                Game.DisplayEvent(((Enemy)pPerson).eType + atk);
            else
                Game.DisplayEvent(pPerson.GetType() + atk);
        }
        #endregion

        #region Destroy
        /// <summary>
        /// Remove completely the Person from game.
        /// </summary>
        internal void Destroy()
        {
            Console.SetCursorPosition(PosX, PosY);
            Console.Write(' ');
            if (this is Enemy)
            {
                Game.MainPlayer.Money += this.Money;
                Game.EnemyList.Remove((Enemy)this);
            }
            else if (this is Player)
            { // Game over
                //TODO: Game over

            }
            else
            {
                Game.PeopleList.Remove(this);
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
            CharacterChar = '@';
        }
    }
    #endregion

    #region Enemy
    class Enemy : Person
    {
        internal Enemy(int X, int Y, EnemyType pEnemyType, int pHp)
            : base(X, Y)
        {
            HP = pHp;
            CharacterChar = 'E';
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