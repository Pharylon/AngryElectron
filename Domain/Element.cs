using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class Element : IEquatable<Element>, IComparable<Element>
    {
        public string Symbol { get; set; }
        public int AtomicNumber { get; set; }
        public double AtomicMass { get; set; }
        public string Name { get; set; }
        public Element()
        {
           
        }

        public new bool Equals(Element other)
        {
            return (this.AtomicNumber == other.AtomicNumber);
        }

        public int CompareTo(Element other)
        {
            if (this.AtomicNumber > other.AtomicNumber)
                return 1;
            else if (this.AtomicNumber == other.AtomicNumber)
                return 0;
            else
                return -1;
        }
    }
}
