using System;

namespace Play
{
    public class Enemy
    {
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
                Console.Write(this.EnemyChar);
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
                Console.Write(this.EnemyChar);
            }
        }

        string _characterName;

        int _hp;
        int HP
        {
            get { return _hp; }
            set
            {
                _hp = value;
            }
        }
        internal string CharacterName
        {
            get { return _characterName; }
            set
            {
                _characterName = value;
            }
        }

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
            this.PosX = (Console.BufferWidth / 2) - (Console.BufferWidth / 4);
            this.PosY = Console.BufferHeight / 2;
            this.HP = 9999;

            Console.SetCursorPosition(this.PosX, this.PosY);
            Console.Write(this.EnemyChar);
        }

        internal void EnemySays(string pText)
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

        internal void MoveUp()
        {
            this.PosY--;
        }

        internal void MoveDown()
        {
            this.PosY++;
        }

        internal void MoveLeft()
        {
            this.PosX--;
        }

        internal void MoveRight()
        {
            this.PosX++;
        }
    }
}