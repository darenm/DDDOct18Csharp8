using System;
using System.Collections.Generic;

namespace DDDOct18Patterns
{
    class Program
    {
        static void Main(string[] args)
        {
            var people = DataFactory.GetPeople();

            DisplayPeople(people);

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        static void DisplayPeople(IEnumerable<Person> people)
        {
            foreach (var person in people)
            {
                Console.WriteLine(Render(person));
            }
        }

        static string Render(Person person)
        {
            // before patterns
            if (person is Quarterback)
            {
                return string.Format("{0} {1} is Quarterback for the {2}", person.FirstName, person.LastName, ((Quarterback)person).TeamName);
            }
            else if (person is Player)
            {
                return string.Format("{0} {1} plays for the {2}", person.FirstName, person.LastName, ((Player)person).Quarterback.TeamName);
            }
            else if (person.LastName == "May")
            {
                return string.Format("{0} {1} prefers **real** football anyway", person.FirstName, person.LastName);
            }
            else
            {
                return string.Format("{0} {1} has no hope of playing football", person.FirstName, person.LastName);
            }

            // patterns 
            //switch (person)
            //{
            //    case Quarterback q:
            //        return $"{q.FirstName} {q.LastName} is Quarterback for the {q.TeamName}";
            //    case Player p:
            //        return $"{p.FirstName} {p.LastName} is plays for the {p.Quarterback.TeamName}";
            //    case Person p when p.LastName == "May":
            //        return $"{p.FirstName} {p.LastName} prefers **real** football anyway";
            //    default:
            //        return $"{person.FirstName} {person.LastName} has no hope of playing football";
            //}

            // switch as an expression with advanced patterns
            //return person switch
            //{
            //    Quarterback q => return $"{q.FirstName} {q.LastName} is Quarterback for the {q.TeamName}",
            //    Player p=> $"{p.FirstName} {p.LastName} is plays for the {p.Quarterback.TeamName}",
            //    Person { LastName: "May" } p => $"{p.FirstName} {p.LastName} prefers **real** football anyway",
            //    _ => $"{person.FirstName} {person.LastName} has no hope of playing football"
            //};

        }
    }
}
