using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDOct18.Library
{
    public class Person
    {
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        public void Deconstruct(out string firstName, out string lastName)
        {
            firstName = FirstName;
            lastName = LastName;
        }

        public string FirstName { get; }
        public string LastName { get; }
    }

    public class Player : Person
    {
        public Player(string firstName, string lastName, Quarterback quarterback)
            : base(firstName, lastName)
        {
            Quarterback = quarterback;
        }

        public void Deconstruct(out string firstName, out string lastName, out Quarterback quarterback) 
        {
            firstName = FirstName;
            lastName = LastName;
            quarterback = Quarterback;
        }

        public Quarterback Quarterback { get; set; }
    }

    public class Quarterback : Person
    {
        public Quarterback(string firstName, string lastName, string teamName)
            : base(firstName, lastName)
        {
            TeamName = teamName;
        }

        public void Deconstruct(out string firstName, out string lastName, out string teamName) 
        {
            firstName = FirstName;
            lastName = LastName;
            teamName = TeamName;
        }
        public string TeamName { get; set; }
    }

    public static class DataFactory
    {
        public static RangedList<Person> GetPeople()
        {
            var broncosQb = new Quarterback("Case", "Keenum", "Broncos");
            var giantsQb = new Quarterback("Eli", "Manning", "Giants");
            var patriotsQb = new Quarterback("Tom", "Brady", "Patriots");

            return new RangedList<Person> {
                broncosQb,
                giantsQb,
                patriotsQb,
                new Person("Daren", "May"),
                new Person("Jerry", "Nixon"),
                new Player("Philip", "Lindsay", broncosQb),
                new Player("Emmanuel", "Sanders", broncosQb),
                new Player("Demaryius", "Thomas", broncosQb),
                new Player("Saquon", "Barkley", giantsQb),
                new Player("Odell", "Beckham Jr", giantsQb),
                new Player("Sterling", "Shepherd", giantsQb),
                new Player("James", "White", patriotsQb),
                new Player("Chris", "Hogan", patriotsQb),
                new Player("Josh", "Gordan", patriotsQb)
            };

        }
    }
}
