using System;
using System.Collections.Generic;
using System.Text;

namespace DDDOct18Csharp8
{
    class ContactService
    {
        public Contact Get()
        {
            return new Contact("Daren", null, lastName: "May", age: 49);
        }
    }
}
