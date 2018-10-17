using System;
using System.Collections.Generic;
using System.Linq;
using DDDOct18.Library;


namespace DDDOct18Ranges
{
    class Program
    {
        static void Main(string[] args)
        {
            var people = DataFactory.GetPeople();

            Console.WriteLine("Existing");
            Console.WriteLine("**********************************");

            RenderPerson(people[1]);
            Console.WriteLine("**********************************");

            RenderPerson(people.First());
            Console.WriteLine("**********************************");

            RenderPerson(people.Last());
            Console.WriteLine("**********************************");


            // Show 4 and 5
            RenderPerson(people.Skip(3).Take(2));
            Console.WriteLine("**********************************");


            //Console.WriteLine("C# 8");
            //Console.WriteLine("**********************************");

            //// indexes and ranges

            //Index index = ^3;
            //Range range = 2..^2;


            //// 3rd from end
            //RenderPerson(people[index]);
            //RenderPerson(people[^3]);
            //Console.WriteLine("**********************************");

            //// 3 to 5
            //RenderPerson(people[3..5]);
            //Console.WriteLine("**********************************");

            //// first 5
            //RenderPerson(people[..5]);
            //Console.WriteLine("**********************************");

            //// 3rd to 3rd from End
            //RenderPerson(people[3..^3]);
            //Console.WriteLine("**********************************");

             Console.WriteLine("Press enter to exit");
            Console.ReadLine();
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
