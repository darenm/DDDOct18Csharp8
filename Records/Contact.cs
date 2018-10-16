using System;

namespace Records
{
    public class Contact(string FirstName, string LastName);

    

    public class Contact : IEquatable<Contact>
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Contact(string FirstName, string LastName)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
        }
        public bool Equals(Contact other) // for IEquatable<Contact>
        {
            return Equals(FirstName, other.FirstName) && Equals(LastName, other.LastName);
        }
        public override bool Equals(object other)
        {
            return (other as Contact)?.Equals(this) == true;
        }
        public override int GetHashCode()
        {
            return (FirstName?.GetHashCode() * 17 + LastName?.GetHashCode()).GetValueOrDefault();
        }

        // Relies on using expressions
        //public Contact With(string FirstName = this.FirstName, string LastName = this.LastName) => new Contact(FirstName, LastName);
        //public void Deconstruct(out object FirstName, out object LastName)
        //{
        //    FirstName = self.FirstName;
        //    LastName = self.LastName;
        //}
    }
}
