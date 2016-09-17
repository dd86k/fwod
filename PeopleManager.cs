using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Person p = new Person(x, y, hp, c, init);
            p.Name = name;

            if (level == -1)
                PeopleList[Game.CurrentFloor].Add(p);
            else
                PeopleList[level].Add(p);
        }

        //TODO: TakeTurns(void);
        /*public void TakeTurns()
        {

        }*/

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

        public Person this[int level, int index]
        {
            get
            {
                return PeopleList[level][index];
            }
        }

        public Person this[int level, string name]
        {
            get
            {
                foreach (Person item in PeopleList[level])
                {
                    if (item.Name == name)
                        return item;
                }

                return null;
            }
        }
    }
}
