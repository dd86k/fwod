using System.Collections.Generic;

/*
 * Manages people on screen except the player.
 */

namespace fwod
{
    class PeopleManager
    {
        List<List<Person>> PeopleList { get; }

        public PeopleManager()
        {
            PeopleList = new List<List<Person>>();
            PeopleList.Add(new List<Person>());
        }

        public void CreatePerson(int x, int y, short hp = 10,
            char c = 'P', string name = null, bool init = true,
            int level = -1)
        {
            Person p = new Person(x, y, hp, c, init)
            {
                Name = name
            };
            if (level == -1)
                PeopleList[Game.CurrentFloor].Add(p);
            else
                PeopleList[level].Add(p);
        }

        //TODO: TakeTurns(void);
        /*public void TakeTurns()
        {

        }*/

        public bool IsSomeoneAt(int x, int y)
        {
            return IsSomeoneAt(Game.CurrentFloor, x, y);
        }

        public bool IsSomeoneAt(int floor, int x, int y)
        {
            foreach (Person p in PeopleList[floor])
            {
                if (p.Y == y && p.X == x)
                    return true;
            }

            return false;
        }

        public Person GetPersonAt(int x, int y)
        {
            return GetPersonAt(Game.CurrentFloor, x, y);
        }

        public Person GetPersonAt(int floor, int x, int y)
        {
            foreach (Person P in PeopleList[floor])
            {
                if (P.X == x && P.Y == y)
                    return P;
            }

            return null;
        }

        public List<Person> this[int index]
        {
            get
            {
                return PeopleList[index];
            }
        }

        public Person this[string name]
        {
            get
            {
                foreach (Person item in PeopleList[Game.CurrentFloor])
                {
                    if (item.Name == name)
                        return item;
                }

                return null;
            }
        }

        public Person this[int floor, int index]
        {
            get
            {
                return PeopleList[floor][index];
            }
        }

        public Person this[int floor, string name]
        {
            get
            {
                foreach (Person item in PeopleList[floor])
                {
                    if (item.Name == name)
                        return item;
                }

                return null;
            }
        }
    }
}
