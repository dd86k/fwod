
/*
 * Abilities, skills.
 */

namespace fwod
{
    class AbilityManager
    {
        ushort STAT_MAX = 10;
        ushort LEVEL_MAX = 99;

        public AbilityManager()
        {
            _s = new ushort[7];
            for (int i = 0; i < 7; ++_s[i++]);
            _level = 1;
        }

        ushort _level;
        public ushort Level
        {
            get { return _level; }
            private set
            {
                if (value <= LEVEL_MAX)
                    _level = value;
            }
        }
        long MaximumExperience => (long)(Level * 1.2f);
        long _experience;
        public long Experience
        {
            get { return _experience; }
            set
            {
                if (value >= MaximumExperience)
                {
                    _experience = 0;
                    _level++;
                }
                else
                    _experience = value;
            }
        }

        ushort[] _s;
        /// <summary>
        /// Strength
        /// </summary>
        public ushort Strength
        {
            get { return _s[0] ; }
            // Check overflow.
            set { _s[0] = value >= STAT_MAX ? _s[0] = 0xff : _s[0] = value; }
        }
        
        /// <summary>
        /// Dexterity
        /// </summary>
        public ushort Dexterity
        {
            get { return _s[1]; }
            // Check overflow.
            set { _s[1] = value >= STAT_MAX ? _s[1] = 0xff : _s[1] = value; }
        }
        
        /// <summary>
        /// Agility
        /// </summary>
        public ushort Agility
        {
            get { return _s[2]; }
            // Check overflow.
            set { _s[2] = value >= STAT_MAX ? _s[2] = 0xff : _s[2] = value; }
        }
        
        /// <summary>
        /// Sight
        /// </summary>
        public ushort Sight
        {
            get { return _s[3]; }
            // Check overflow.
            set { _s[3] = value >= STAT_MAX ? _s[3] = 0xff : _s[3] = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public ushort S5
        {
            get { return _s[4]; }
            // Check overflow.
            set { _s[4] = value >= STAT_MAX ? _s[4] = 0xff : _s[4] = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public ushort S6
        {
            get { return _s[5]; }
            // Check overflow.
            set { _s[5] = value >= STAT_MAX ? _s[5] = 0xff : _s[5] = value; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        public ushort S7
        {
            get { return _s[6]; }
            // Check overflow.
            set { _s[6] = value >= STAT_MAX ? _s[6] = 0xff : _s[6] = value; }
        }
    }
}
