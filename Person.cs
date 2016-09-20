using System;

/*
    A Person.
    Can be a Player, Enemy, etc.
*/

//TODO: Make strength contribute to defensepoints (0.2 ratio)

namespace fwod
{
    public enum EnemyType
    {
        Rat, 
    }

    public enum EnemyModifier
    {
        Weak, Strong
    }

    static class EnemyTypeHelper
    {
        public static float GetHPModifier(this EnemyType t)
        {
            switch (t)
            {
                // Common enemies
                default: return 1.8f;
            }
        }

        public static float GetMoneyModifier(this EnemyType t)
        {
            switch (t)
            {
                default: return 0.2f;
            }
        }
    }

    #region Person
    class Person
    {
        #region Constants
        const int BUBBLE_PADDING_HORIZONTAL = 0;
        const int BUBBLE_TEXT_MAXLEN = 25;
        const int STAT_MAX = 10;
        #endregion

        #region Properties
        #region Position
        int _x;
        /// <summary>
        /// Set or get the Player position (Left).
        /// </summary>
        public int X
        {
            get { return _x; }
            set
            {
                if (value != _x)
                {
                    if (!MapManager.Map[_y, value].IsSolidObject())
                    {
                        if (Game.IsSomeonePresentAt(Game.CurrentFloor, value, _y))
                        {
                            Person futrPerson =
                                Game.GetPersonObjectAt(Game.CurrentFloor, value, _y);

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
        public int Y
        {
            get { return _y; }
            set
            {
                if (value != _y)
                {
                    if (!MapManager.Map[value, _x].IsSolidObject())
                    {
                        if (Game.IsSomeonePresentAt(Game.CurrentFloor, _x, value))
                        {
                            Person futrPerson =
                                Game.GetPersonObjectAt(Game.CurrentFloor, _x, value);

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

            // Move player
            Console.SetCursorPosition(_x = newX, _y = newY);
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
        public int Level
        {
            get; set;
        }

        ushort[] _s;
        /// <summary>
        /// Strength
        /// </summary>
        public ushort Strength
        {
            get { return _s[0] ; }
            // Check overflow.
            set { _s[0] = value > STAT_MAX ? _s[0] = 0xff : _s[0] = value; }
        }
        
        /// <summary>
        /// Dexterity
        /// </summary>
        public ushort Dexterity
        {
            get { return _s[1]; }
            // Check overflow.
            set { _s[1] = value > STAT_MAX ? _s[1] = 0xff : _s[1] = value; }
        }
        
        /// <summary>
        /// Agility
        /// </summary>
        public ushort Agility
        {
            get { return _s[2]; }
            // Check overflow.
            set { _s[2] = value > STAT_MAX ? _s[2] = 0xff : _s[2] = value; }
        }
        
        /// <summary>
        /// Sight
        /// </summary>
        public ushort Sight
        {
            get { return _s[3]; }
            // Check overflow.
            set { _s[3] = value > STAT_MAX ? _s[3] = 0xff : _s[3] = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public ushort S5
        {
            get { return _s[4]; }
            // Check overflow.
            set { _s[4] = value > STAT_MAX ? _s[4] = 0xff : _s[4] = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public ushort S6
        {
            get { return _s[5]; }
            // Check overflow.
            set { _s[5] = value > STAT_MAX ? _s[5] = 0xff : _s[5] = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public ushort S7
        {
            get { return _s[6]; }
            // Check overflow.
            set { _s[6] = value > STAT_MAX ? _s[6] = 0xff : _s[6] = value; }
        }
        #endregion

        #region Money
        int _money;
        /// <summary>
        /// Current sum of money in this Person.
        /// The sum is display via the inventory for Player.
        /// </summary>
        public int Money
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
        public InventoryManager Inventory { get; }
        #endregion
        #endregion

        #region Construction
        public Person(int x, int y,
            short hp = 10, char c = 'P', bool init = true)
        {
            _maxhp = _hp = hp;
            _x = x;
            _y = y;
            _s = new ushort[7];
            for (int i = 0; i < 7; ++_s[i++]);
            Char = c;
            //TODO: Left and right weapon?
            Inventory = new InventoryManager();
            Inventory.Armor = new Armor(ArmorType.Shirt);
            Inventory.Weapon = new Weapon(WeaponType.Fist);

            //Game.PeopleList[Game.CurrentFloor].Add(this);

            if (init)
                Initialize();
        }
        #endregion

        #region Init
        /// <summary>
        /// Place the Person on screen.
        /// </summary>
        public void Initialize()
        {
            Console.SetCursorPosition(_x, _y);
            Console.Write(Char);
        }
        #endregion

        #region Bubble
        /// <summary>
        /// Generates a bubble for this Person.
        /// </summary>
        /// <param name="x">Top starting position</param>
        /// <param name="y">Left starting position</param>
        /// <param name="width">Bubble width</param>
        /// <param name="height">Bubble height</param>
        void GenerateBubble(int x, int y, int width, int height)
        {
            Utils.GenerateBox(x, y, width, height);

            // Bubble chat "connector"
            if (y < _y) // Over Person
            {
                Console.SetCursorPosition(_x, _y - 1);
                Console.Write('┬');
            }
            else // Under Person
            {
                Console.SetCursorPosition(_x, _y + 1);
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
                X - (length / 2) - (BUBBLE_PADDING_HORIZONTAL * 2) - 1,
                Y - (length / BUBBLE_TEXT_MAXLEN) - 3,
                length + (BUBBLE_PADDING_HORIZONTAL * 2) + 2,
                (length / (BUBBLE_TEXT_MAXLEN + 1)) + 4
            );
        }

        /// <summary>
        /// Clear the past bubble and reprint chars from game layer.
        /// </summary>
        /// <param name="x">Top starting position</param>
        /// <param name="y">Left starting position</param>
        /// <param name="width">Bubble width</param>
        /// <param name="height">Bubble height</param>
        unsafe void ClearBubble(int x, int y, int width, int height,
            bool map = true)
        {
            if (map)
                MapManager.RedrawMap(x, y, width, height);
            else
            {
                int yl = y + height;
                string l = new string(' ', width);
                for (; y < yl; y++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(l);
                }
            }
        }
        #endregion

        #region Conversation
        /// <summary>
        /// Makes the Person talk.
        /// </summary>
        /// <param name="text">Dialog</param>
        /// <param name="wait">Wait for keydown</param>
        /// <param name="redraw">Redraw map.</param>
        public void Say(string text,
            bool wait = true, bool redraw = true)
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
        /// <param name="redraw">Redraw map.</param>
        public void Say(string[] lines,
            bool wait = true, bool redraw = true)
        {
            int arrlen = lines.Length;
            int strlen = arrlen > 1 ?
                lines.GetLonguestStringLength() : lines[0].Length;
            int width = strlen + (BUBBLE_PADDING_HORIZONTAL * 2) + 2;
            int height = arrlen + 2;
            int startX = X - (width / 2);
            int startY = Y - (height);

            // Re-locate startX if it goes further than the display buffer
            if (startX + width > Utils.WindowWidth)
                startX = Utils.WindowWidth - width;
            else if (startX < 0)
                startX = 0;

            // Re-locate startY if it goes further than the display buffer
            if (startY > Utils.WindowWidth)
                startY = Utils.WindowWidth - (width - 2);
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

            // Fill the rest of the bubble
            if (lines.Length > 1)
                Console.Write(
                    new string(' ', strlen - lines[arrlen - 1].Length)
                );

            if (wait)
            {
                Console.ReadKey(true);
                ClearBubble(startX, startY, width, height, false);
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
        public string GetAnswer()
        {
            return GetAnswer(BUBBLE_TEXT_MAXLEN);
        }

        /// <summary>
        /// Get input from the Person.
        /// </summary>
        /// <param name="limit">Limit in characters.</param>
        /// <returns>Answer</returns>
        public string GetAnswer(int limit = 25)
        {
            Say(new string(' ', limit), false);

            // Read input from this Person
            string t = Utils.ReadLine(limit);

            // Clear bubble
            ClearBubble(limit);

            return t;
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
            //TODO: Accuracy algorithm

            int dam = Inventory.Weapon.Damage;
            int def = person.Inventory.Armor.ArmorPoints;
            
            // Attack points
            int ap =
                Inventory.Weapon.Type.IsGun() ?
                dam - def :
                ((Strength * dam) + dam) - (def);

            string atkstr = $": {person.HP} HP - {ap} = {person.HP -= ap} HP";

            if (person.HP <= 0) // If killed
                atkstr += $" -- Dead! You earn {person.Money}$!";

            Game.Statistics.DamageDealt += (uint)ap;

            if (person is Enemy)
                Game.Log(((Enemy)person).Race + atkstr);
            else
                Game.Log(person.GetType() + atkstr);
        }
        #endregion

        #region Destroy
        /// <summary>
        /// Remove completely the Person from the game.
        /// </summary>
        public void Destroy()
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
            : base(X, Y, 10, '@')
        {
            Name = null;
        }
    }
    #endregion

    #region Enemy
    class Enemy : Person
    {
        public Enemy(int x, int y, EnemyType type, int level, bool initialize = true)
            : base(x, y, (short)(level * type.GetHPModifier()), '&')
        {
            Money = (int)(HP * 0.5);
        }
        
        public EnemyType Race { get; }
    }
    #endregion
}