using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngryElectron.Domain
{
    public class Element : IEquatable<Element>, IComparable<Element>, IParsableSymbols
    {
        public string Symbol { get; set; }
        public int AtomicNumber { get; set; }
        public double AtomicMass { get; set; }
        public string Name { get; set; }
        public Element()
        {
           
        }

        public bool Equals(Element other)
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

        public IEnumerable<string> ParsableSymbols
        {
            get { return new List<string>() { Symbol }; }
        }

        public override string ToString()
        {
            return Symbol;
        }

        public string ToHTML()
        {
            return Symbol;
        }


        public List<string> ListOfElements
        {
            get { return new List<string>() { Symbol}; }
        }

        public int GetSubscriptCount(string key)
        {
            return 1;
        }
    }
}
