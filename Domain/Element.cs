using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class Element : IEquatable<Element>
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
    }
}
