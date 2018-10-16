using System;
using System.Collections.Generic;
using System.Text;

namespace ContactTests
{
    public class CrazyClass
    {
        public string CrazyProperty => DateTime.Now.Second % 2 > 0 ? "A string" : null;

        public string NotSoCrazy => "Not so crazy";

        public override bool Equals(object obj)
        {
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.CrazyProperty, this.NotSoCrazy);
        }

        public static bool operator ==(CrazyClass a, CrazyClass b)
        {
            return false;
        }


        public static bool operator !=(CrazyClass a, CrazyClass b)
        {
            return true;
        }

    }
}
