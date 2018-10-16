using System;
using System.Collections.Generic;
using System.Text;

namespace DDDOct18Csharp8
{
    public class RenderContact
    {
        public string Render(Contact contact)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Name: {contact.FirstName} {contact.MiddleName[0]}. {contact.LastName}");
            sb.AppendLine($"Age: {contact.Age}");

            return sb.ToString();
        }
    }
}
