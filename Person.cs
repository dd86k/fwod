﻿using System;
using System.Collections.Generic;

/*
    A Person.
    Can be a Player, Enemy, etc.
*/

namespace fwod
{
    public enum EnemyType
    {
        Rat, 
    }

    static class EnemyTypeHelper
    {
        public static float GetModifier(this EnemyType t)
        {
            switch (t)
            {
                // Common enemies
                default: return 1.8f;
            }
        }
    }

    #region Person
    class Person
    {
        #region Constants
        const int BUBBLE_PADDING_HORIZONTAL = 0;
        const int BUBBLE_TEXT_MAXLEN = 25;
        #endregion

        #region Object properties
        #region Position
        int _x;
        /// <summary>
        /// Set or get the Player position (Left).
        /// </summary>
        internal int X
        {
            get { return _x; }
            set
            {
                if (value != _x)
                {
                    char g = MapManager.Map[_y, value];
                    bool solid = g.IsSolidObject();
                    bool someone = Game.IsSomeonePresentAt(Game.CurrentFloor, value, _y);
                    if (!solid)
                    {
                        if (someone)
                        {
                            Person futrPerson =
                                Game.GetPersonObjectAt(Game.CurrentFloor, value, Y);

                            if (futrPerson != this && futrPerson is Enemy)
                            {
                                Attack(futrPerson);
                            }
                        }
                        else
                        {
                            Move(_x, _y, value, _y);
                        }
                    }
                }
            }
        }

        int _y;
        /// <summary>
        /// Set or get the Player position (Top).
        /// </summary>
        internal int Y
        {
            get { return _y; }
            set
            {
                if (value != _y)
                {
                    char g = MapManager.Map[value, _x];
                    bool solid = g.IsSolidObject();
                    bool someone = Game.IsSomeonePresentAt(Game.CurrentFloor, _x, value);
                    if (!solid)
                    {
                        if (someone)
                        {
                            Person futrPerson =
                                Game.GetPersonObjectAt(Game.CurrentFloor, X, value);

                            if (futrPerson != this && futrPerson is Enemy)
                            {
                                Attack(futrPerson);
                            }
                        }
                        else
                        {
                            Move(_x, _y, _x, value);
                        }
                    }
                }
            }
        }

        void Move(int pastX, int pastY, int newX, int newY)
        {
            // Get old char
            char pastchar = MapManager.Map[pastY, pastX];

            // Place old char
            Console.SetCursorPosition(pastX, pastY);
            Console.Write(pastchar == '\0' ? ' ' : pastchar);

            // Update values
            _y = newY;
            _x = newX;

            // Move player
            Console.SetCursorPosition(newX, newY);
            Console.Write(Char);

            if (this is Player)
                Game.Statistics.StepsTaken++;
        }
        #endregion

        #region Health
        int _hp;
        /// <summary>
        /// Get or set the HP.
        /// </summary>
        public int HP
        {
            get { return _hp; }
            set
            {
                if (value > _maxhp)
                    _hp = _maxhp;
                else
                {
                    Game.Statistics.DamageReceived += (uint)(_hp - value);

                    _hp = value;

                    if (value <= 0)
                        Destroy();
                }

                if (this is Player)
                {
                    Console.SetCursorPosition(29, 0);
                    Console.Write(new string(' ', 13));
                    Console.SetCursorPosition(29, 0); //HP: 0000/0000
                    Console.Write($"HP: {_hp:D4}/{_maxhp:D4}");
                }

            }
        }

        int _maxhp;
        public int MaxHP
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
        string _name;
        /// <summary>
        /// Get or set the name of the character.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;

                if (this is Player)
                {
                    // Clear name and redraw it (in case of shorter name)
                    Console.SetCursorPosition(1, 0);
                    Console.Write(new string(' ', 25));
                    Console.SetCursorPosition(1, 0);
                    Console.Write(_name);
                }
            }
        }

        /// <summary>
        /// Get or set the character displayed on screen.
        /// </summary>
        public char Char { get; set; }
        #endregion

        #region Stats
        internal int Level
        {
            get; set;
        }

        byte[] _s;
        /// <summary>
        /// Strength
        /// </summary>
        public byte S1
        {
            get { return _s[0] ; }
            // Check overflow.
            set { _s[0] = value + 1 == 0 ? _s[0] = 0xff : _s[0] = value; }
        }
        
        /// <summary>
        /// Dexterity
        /// </summary>
        public byte S2
        {
            get { return _s[1]; }
            // Check overflow.
            set { _s[1] = value + 1 == 0 ? _s[1] = 0xff : _s[1] = value; }
        }
        
        /// <summary>
        /// Agility
        /// </summary>
        public byte S3
        {
            get { return _s[2]; }
            // Check overflow.
            set { _s[2] = value + 1 == 0 ? _s[2] = 0xff : _s[2] = value; }
        }
        
        /// <summary>
        /// Sight
        /// </summary>
        public byte S4
        {
            get { return _s[3]; }
            // Check overflow.
            set { _s[3] = value + 1 == 0 ? _s[3] = 0xff : _s[3] = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public byte S5
        {
            get { return _s[4]; }
            // Check overflow.
            set { _s[4] = value + 1 == 0 ? _s[4] = 0xff : _s[4] = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public byte S6
        {
            get { return _s[5]; }
            // Check overflow.
            set { _s[5] = value + 1 == 0 ? _s[5] = 0xff : _s[5] = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public byte S7
        {
            get { return _s[6]; }
            // Check overflow.
            set { _s[6] = value + 1 == 0 ? _s[6] = 0xff : _s[6] = value; }
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
        /// The sum is display via the inventory for Player.
        /// </summary>
        internal int Money
        {
            get { return _money; }
            set
            {
                if (value <= 1000000) // 1'000'000
                    _money = value;
                
                if (this is Player)
                {
                    Console.SetCursorPosition(45, 0);
                    Console.Write(new string(' ', 8));
                    Console.SetCursorPosition(45, 0); //0'000'000$
                    Console.Write($"{_money:0'000'000}$");
                }
            }
        }
        #endregion

        #region Inventory
        public List<Item> Inventory { get; }
        #endregion
        #endregion

        #region Construction
        public Person(int pX, int pY)
            : this(pX, pY, 10)
        {

        }

        public Person(int x, int y, short hp, bool initialize = true)
        {
            _hp = hp;
            _maxhp = _hp;
            _money = (int)(hp * 0.5);
            _x = x;
            _y = y;
            _s = new byte[7];
            for (int i = 0; i < 7; _s[i++]++);
            Char = 'P';
            //TODO: Left and right weapon?
            EquipedWeapon = new Weapon("Fists", 0);
            EquipedArmor = new Armor("Shirt", 0);
            Inventory = new List<Item>();

            if (initialize)
                Initialize();
        }
        #endregion

        #region Init
        /// <summary>
        /// Place the Person on screen.
        /// </summary>
        internal void Initialize()
        {
            Console.SetCursorPosition(_x, _y);
            Console.Write(Char);
        }
        #endregion

        #region Bubble
        /// <summary>
        /// Generates a bubble for this Person.
        /// </summary>
        /// <param name="pStartX">Top starting position</param>
        /// <param name="pStartY">Left starting position</param>
        /// <param name="pWidth">Bubble width</param>
        /// <param name="pHeight">Bubble height</param>
        void GenerateBubble(int pStartX, int pStartY, int pWidth, int pHeight)
        {
            Utils.GenerateBox(pStartX, pStartY, pWidth, pHeight);

            // Bubble chat "connector"
            if (pStartY < Y) // Over Person
            {
                Console.SetCursorPosition(X, Y - 2);
                Console.Write('┬');
            }
            else // Under Person
            {
                Console.SetCursorPosition(X, Y + 2);
                Console.Write('┴');
            }
        }

        /// <summary>
        /// Clear the past bubble and reprint chars from game layer.
        /// </summary>
        /// <param name="length">Length of the string</param>
        void ClearBubble(int length)
        {
            MapManager.RedrawMap(
                X - (length / 2) + (BUBBLE_PADDING_HORIZONTAL * 2),
                Y - (length / BUBBLE_TEXT_MAXLEN) - 3,
                length + (BUBBLE_PADDING_HORIZONTAL * 2) + 2,
                (length / (BUBBLE_TEXT_MAXLEN + 1)) + 3
            );
        }

        /// <summary>
        /// Clear the past bubble and reprint chars from game layer.
        /// </summary>
        /// <param name="x">Top starting position</param>
        /// <param name="y">Left starting position</param>
        /// <param name="width">Bubble width</param>
        /// <param name="height">Bubble height</param>
        unsafe void ClearBubble(int x, int y, int width, int height)
        {
            MapManager.RedrawMap(x, y, width, height);
        }
        #endregion

        #region Conversation
        /// <summary>
        /// Makes the Person talk.
        /// </summary>
        /// <param name="text">Dialog</param>
        /// <param name="wait">Wait for keydown</param>
        internal void Say(string text, bool wait = true)
        {
            string[] lines = new string[] { text };

            if (text.Length > BUBBLE_TEXT_MAXLEN)
            {
                int ci = 0; // Multiline scenario row index
                int start = 0; // Multiline cutting index
                lines = new string[(text.Length / (BUBBLE_TEXT_MAXLEN + 1)) + 1];

                // This block seperates the input into BUBBLE_MAXLEN characters each lines equally.
                do
                {
                    if (start + BUBBLE_TEXT_MAXLEN > text.Length)
                        lines[ci] = text.Substring(start, text.Length - start);
                    else
                        lines[ci] = text.Substring(start, BUBBLE_TEXT_MAXLEN);
                    ci++;
                    start += BUBBLE_TEXT_MAXLEN;
                } while (start < text.Length);
            }
            
            Say(lines, wait);
        }

        /// <summary>
        /// Makes the Person say a few lines.
        /// </summary>
        /// <param name="lines">Lines of dialog</param>
        /// <param name="wait">Wait for keydown</param>
        internal void Say(string[] lines, bool wait)
        {
            //TODO: Clean this clutter.
            int arrlen = lines.Length;
            int strlen = arrlen > 1 ?
                lines.GetLonguestStringLength() : lines[0].Length;
            int width = strlen + (BUBBLE_PADDING_HORIZONTAL * 2) + 2;
            int height = arrlen + 2;
            int startX = X - (strlen / 2);
            int startY = Y - (arrlen) - 3;

            // Re-locate startX if it goes further than the display buffer
            if (startX + width > Utils.WindowWidth)
                startX = Utils.WindowWidth - width;
            else if (startX < 0)
                startX = 0;

            // Re-locate startY if it goes further than the display buffer
            if (startY > Utils.WindowWidth)
                startY = Utils.WindowWidth - (arrlen - 2);
            else if (startY < 3)
                startY = 3;

            // Generate the bubble
            GenerateBubble(startX, startY, width, height);

            int textStartX = startX + 1;
            int textStartY = startY + 1;

            // Insert Text
            for (int i = 0; i < arrlen; i++)
            {
                Console.SetCursorPosition(textStartX, textStartY + i);
                Console.Write(lines[i]);
            }

            if (wait)
            {
                Console.ReadKey(true);
                ClearBubble(startX, startY, width, height);
            }
            else
            {
                // Prepare for text
                Console.SetCursorPosition(textStartX, textStartY);
            }
        }

        /// <summary>
        /// Get input from the Person.
        /// </summary>
        /// <returns>Answer</returns>
        internal string GetAnswer()
        {
            return GetAnswer(BUBBLE_TEXT_MAXLEN);
        }

        /// <summary>
        /// Get input from the Person.
        /// </summary>
        /// <param name="limit">Limit in characters.</param>
        /// <returns>Answer</returns>
        internal string GetAnswer(int limit)
        {
            Say(new string(' ', limit), false);

            // Read input from this Person
            string Out = Utils.ReadLine(limit);

            // Clear bubble
            ClearBubble(limit);

            return Out;
        }
        #endregion

        #region Movement
        /// <summary>
        /// Makes the enemy move up one square
        /// </summary>
        public void MoveUp()
        {
            Y--;
        }

        /// <summary>
        /// Makes the enemy move down one square
        /// </summary>
        public void MoveDown()
        {
            Y++;
        }

        /// <summary>
        /// Makes the enemy move left one square
        /// </summary>
        public void MoveLeft()
        {
            X--;
        }

        /// <summary>
        /// Makes the enemy move right one square
        /// </summary>
        public void MoveRight()
        {
            X++;
        }
        #endregion

        #region Attack
        public void Attack(Person person)
        {
            //TODO: Attack algorithm
            int AttackPoints = ((S1 * 2) + EquipedWeapon.Damage) - EquipedArmor.ArmorPoints;

            string atk = $": {person.HP} HP - {AttackPoints} = {person.HP -= AttackPoints} HP";

            if (person.HP <= 0)
                atk += $" -- Dead! You earn {person.Money}$!";

            Game.Statistics.DamageDealt += (uint)AttackPoints;

            if (person is Enemy)
                Game.Log(((Enemy)person).Race + atk);
            else
                Game.Log(person.GetType() + atk);
        }
        #endregion

        #region Destroy
        /// <summary>
        /// Remove completely the Person from the game.
        /// </summary>
        internal void Destroy()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write(' ');

            Game.MainPlayer.Money += Money;
            Game.Statistics.MoneyGained += (uint)Money;
            Game.PeopleList[Game.CurrentFloor].Remove(this);

            if (this is Enemy)
            {
                Game.Statistics.EnemiesKilled++;
            }
            else if (this is Player)
            { //TODO: Game over

            }
        }
        #endregion
    }
    #endregion

    #region Player
    class Player : Person
    {
        public Player()
            : this(Utils.WindowWidth / 2, Utils.WindowHeight / 2) {}

        public Player(int X, int Y)
            : base(X, Y)
        {
            Char = '@';
            Name = null;
        }

        //TODO: ShowInventory(void)
        public void ShowInventory()
        {

        }
    }
    #endregion

    #region Enemy
    class Enemy : Person
    {
        public Enemy(int x, int y, EnemyType type, int level)
            : base(x, y)
        {
            HP = MaxHP = (ushort)(level * type.GetModifier());
            Char = 'E';
        }
        
        public EnemyType Race { get; }
    }
    #endregion
}