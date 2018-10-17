using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DDDOct18.Library
{
    public class RangedList<T>: List<T>
    {
        public T this[Index i] => base[DeterminePosition(i)];

        public IEnumerable<T> this[Range r] {
            get
            {
                var startPos = DeterminePosition(r.Start);
                var endPos = DeterminePosition(r.End);
                var numberOfItems = endPos - startPos;
                return this.Skip(startPos).Take(numberOfItems);
            }
        }

        private int DeterminePosition(Index i)
        {
            var candidate = i.FromEnd ? this.Count - i.Value : i.Value;

            switch(candidate)
            {
                case int c when c < 0:
                    return 0;
                case int c when c >= this.Count:
                    return this.Count - 1;
                default:
                    return candidate;
            }
        }
    }
}
