using System;
using System.Collections.Generic;
using System.Text;

namespace DDDOct18Csharp8
{
    public class Contact
    {
        public string FirstName { get;  }
        public string MiddleName { get;  }
        public string LastName { get;  }
        public int Age { get;  }

        public Contact(string firstName, string middleName, string lastName, int age)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Age = age;
        }
    }
}
