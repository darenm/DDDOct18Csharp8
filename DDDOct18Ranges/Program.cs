using System;
using System.Collections.Generic;
using System.Linq;
using DDDOct18Patterns;

namespace DDDOct18Ranges
{
    class Program
    {
        static void Main(string[] args)
        {
            var testString = "This is a test string";


            var people = DataFactory.GetPeople();

            RenderPerson(people[1]);

            RenderPerson(people.First());

            RenderPerson(people.Last());

            // Show 4 and 5
            RenderPerson(people.Skip(3).Take(2));

            // indexes and ranges

            Index index = ^3;
            Range range = 2..^2;


            // 3rd from end
            // RenderPerson(people[^3]);

            // 3 to 5
            // RenderPerson(people[3..5]);

            // first 5
            // RenderPerson(people[..5]);

            // 3rd to 3rd from End
            // RenderPerson(people[3..^3]);
        }

        static void RenderPerson(Person p) => Console.WriteLine($"{p.FirstName} {p.LastName}");

        static void RenderPerson(IEnumerable<Person> peeps)
        {
            foreach (var p in peeps)
            {
                RenderPerson(p);
            }
        }

    }
}
