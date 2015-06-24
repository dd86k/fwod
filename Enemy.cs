using System;

/*
    This class is about anything related to the enemies.
*/

namespace FWoD
{
    public class Enemy
    {
        int _posx;
        /// <summary>
        /// X position (lower is higher).
        /// </summary>
        /// <value>Left position</value>
        int PosX
        {
            get { return _posx; }
            set
            {
                Console.SetCursorPosition(this._posx, this._posy);
                Console.Write(" ");
                this._posx = value;
                Console.SetCursorPosition(this._posx, this._posy);
                Console.Write(this.EnemyChar);
            }
        }

        int _posy;
        /// <summary>
        /// Y position (lower is closer to the left).
        /// </summary>
        /// <value>Top position</value>
        int PosY
        {
            get { return _posy; }
            set
            {
                Console.SetCursorPosition(this._posx, this._posy);
                Console.Write(" ");
                this._posy = value;
                Console.SetCursorPosition(this._posx, this._posy);
                Console.Write(this.EnemyChar);
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
            }
        }

        /// <summary>
        /// Gets or sets the name of the enemy.
        /// </summary>
        /// <value>The name of the character.</value>
        internal string CharacterName
        {
            get { return _characterName; }
            set
            {
                _characterName = value;
            }
        }

        /// <summary>
        /// The enemy's character
        /// </summary>
        internal char EnemyChar;

        internal Enemy()
        { // Defaults
            this.EnemyChar = '#';
            this.HP = 10;
        }

        /// <summary>
        /// Places the Enemy.
        /// </summary>
        internal void Initialize()
        {
            //TODO: Should be random (Except for bosses)
            this.PosX = (Console.BufferWidth / 2) - (Console.BufferWidth / 4);
            this.PosY = Console.BufferHeight / 2;

            Console.SetCursorPosition(this.PosX, this.PosY);
            Console.Write(this.EnemyChar);
        }

        /// <summary>
        /// Make the enemy say something.
        /// </summary>
        /// <param name="pText">Text.</param>
        internal void EnemySays(string pText)
        {
            // -- Make bubble --
            // determine the starting position of the bubble
            int StartX = (pText.Length > 2 ?
                (this.PosX - (pText.Length / 2)) - 2:
                (this.PosX - (pText.Length / 2)) - 1);
            int StartY = this.PosY - 4;

            Game.GenerateBox(Game.TypeOfLine.Single, StartX, StartY, pText.Length + 4, 3);

            // bubble chat "connector" (Over player)
            Console.SetCursorPosition(this.PosX, this.PosY - 2);
            Console.Write(Game.Graphics.Lines.SingleConnector[2]);

            // -- Insert Text --
            Console.SetCursorPosition(StartX + 2, StartY + 1);
            Console.Write(pText);

            // Waiting for keypress
            Console.SetCursorPosition(0, 0);
            Console.ReadKey(true);

            // Clear bubble
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