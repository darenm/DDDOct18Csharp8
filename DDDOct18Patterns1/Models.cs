using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDOct18Patterns
{
    public class MeerMortal
    {
        public MeerMortal(string firstName, string lastName) => (FirstName, LastName) = (firstName, lastName);
        public void Deconstruct(out string firstName, out string lastName) => (firstName, lastName) = (FirstName, LastName);

        public string FirstName { get; }
        public string LastName { get; }
    }

    public class Developer : MeerMortal
    {
        public Developer(string firstName, string lastName, LeadDeveloper leadDeveloper)
            : base(firstName, lastName) => TechLead = leadDeveloper;

        public void Deconstruct(out string firstName, out string lastName, out LeadDeveloper techLead) => (firstName, lastName, techLead) = (FirstName, LastName, TechLead);

        public LeadDeveloper TechLead { get; set; }
    }

    public class LeadDeveloper : MeerMortal
    {
        public LeadDeveloper(string firstName, string lastName, string teamName)
            : base(firstName, lastName) => TeamName = teamName;
        public void Deconstruct(out string firstName, out string lastName, out string teamName) => (firstName, lastName, teamName) = (FirstName, LastName, TeamName);
        public string TeamName { get; set; }
    }

}
